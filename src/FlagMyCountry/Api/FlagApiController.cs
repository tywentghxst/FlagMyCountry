using MediaBrowser.Controller.Entities;
using MediaBrowser.Controller.Library;
using MediaBrowser.Controller.Net; // For IRestfulService, [Route]
using MediaBrowser.Model.Logging;
using MediaBrowser.Model.Services; // For IReturnVoid, [ApiMember]
using System;
using System.Threading;
using MediaBrowser.Controller.Providers; // For MetadataRefreshOptions
using MediaBrowser.Controller.IO;       // For DirectoryService (used by MetadataRefreshOptions)

namespace FlagMyCountry.Api
{
    [Route("/FlagMyCountry/SetLanguage", "POST", Summary = "Sets or clears the language flag for a media item.")]
    public class SetLanguageRequestDto : IReturnVoid // DTO suffix is a common convention
    {
        [ApiMember(Name = "ItemId", Description = "The ID of the media item.", IsRequired = true, DataType = "string", ParameterType = "query", Verb = "POST")]
        public string ItemId { get; set; }

        [ApiMember(Name = "LanguageCode", Description = "The language code (e.g., 'en', 'fr'). Empty string to clear.", IsRequired = false, DataType = "string", ParameterType = "query", Verb = "POST")]
        public string LanguageCode { get; set; }
    }

    public class FlagApiController : IRestfulService
    {
        private readonly ILogger _logger;
        private readonly ILibraryManager _libraryManager;
        private readonly IDirectoryService _directoryService; // Needed for MetadataRefreshOptions constructor

        public FlagApiController(ILogManager logManager, ILibraryManager libraryManager, IDirectoryService directoryService)
        {
            _logger = logManager.GetLogger(Plugin.PluginGuid.ToString());
            _libraryManager = libraryManager ?? throw new ArgumentNullException(nameof(libraryManager));
            _directoryService = directoryService ?? throw new ArgumentNullException(nameof(directoryService)); // Inject IDirectoryService
            _logger.Info("FlagApiController initialized.");
        }

        // Handles POST requests to /FlagMyCountry/SetLanguage
        public void Post(SetLanguageRequestDto request)
        {
            if (string.IsNullOrEmpty(request.ItemId))
            {
                _logger.Warn("SetLanguageRequest: ItemId is null or empty. Request ignored.");
                // In a real service, you might throw new ArgumentException or return an error response.
                return;
            }
            // request.LanguageCode can be null or empty (for clearing), which is valid.

            Guid itemIdGuid;
            try
            {
                itemIdGuid = Guid.Parse(request.ItemId);
            }
            catch (FormatException ex)
            {
                _logger.Error($"SetLanguageRequest: Invalid ItemId GUID format '{request.ItemId}'. Exception: {ex.Message}");
                // Consider throwing new ServiceStack.Common.Web.HttpError(System.Net.HttpStatusCode.BadRequest, "Invalid ItemId format.");
                return;
            }

            var item = _libraryManager.GetItemById(itemIdGuid);
            if (item == null)
            {
                _logger.Warn($"SetLanguageRequest: Item with ID '{request.ItemId}' not found.");
                // Consider throwing new ServiceStack.Common.Web.HttpError(System.Net.HttpStatusCode.NotFound, "Item not found.");
                return;
            }

            _logger.Info($"Processing SetLanguage request for item '{item.Name}' (ID: {request.ItemId}). LanguageCode: '{request.LanguageCode ?? "CLEAR"}'.");

            if (string.IsNullOrEmpty(request.LanguageCode))
            {
                // Clear the language flag data
                item.RemovePluginData(Plugin.PluginGuid, FlagOverlayManager.UserDataLanguageKey);
                _logger.Info($"Cleared language flag data for item '{item.Name}'.");
            }
            else
            {
                // Set the language flag data
                item.SavePluginData(Plugin.PluginGuid, FlagOverlayManager.UserDataLanguageKey, request.LanguageCode);
                _logger.Info($"Set language flag data for item '{item.Name}' to '{request.LanguageCode}'.");
            }

            // Persist changes for the item. This is crucial.
            // UpdateItem alone might not be enough to trigger IImageEnhancer reliably for existing cached images.
            // A full metadata refresh for the item is more robust.
            var refreshOptions = new MetadataRefreshOptions(_directoryService)
            {
                ImageRefreshMode = MediaBrowser.Model.Configuration.MetadataRefreshMode.Full,
                MetadataRefreshMode = MediaBrowser.Model.Configuration.MetadataRefreshMode.Full // Refresh all metadata too if needed
            };

            // Run the refresh asynchronously (fire and forget) as it can take time.
            // Don't await it here to keep the API call responsive.
            // Ensure error handling within the Task if necessary.
            Task.Run(() => item.RefreshMetadata(refreshOptions, CancellationToken.None))
                .ContinueWith(t => {
                    if (t.IsFaulted) {
                        _logger.Error($"Error during background metadata refresh for item {item.Id}: {t.Exception?.InnerException?.Message}");
                    } else {
                        _logger.Info($"Background metadata refresh initiated for item '{item.Name}' after language flag change.");
                    }
                });
        }
    }
}
