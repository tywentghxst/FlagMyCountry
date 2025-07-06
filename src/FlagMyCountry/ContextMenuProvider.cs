using MediaBrowser.Controller.Entities;
using MediaBrowser.Controller.Plugins; // For IHasContextMenuItem (though often it's directly in Model.Services)
using MediaBrowser.Model.Services;    // For IHasContextMenuItem, ContextMenuItem
using MediaBrowser.Model.Entities;    // For BaseItem, User, Movie, Series, Episode, Video
using MediaBrowser.Model.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlagMyCountry
{
    public class ContextMenuProvider : IHasContextMenuItem
    {
        private readonly ILogger _logger;
        private readonly PluginConfiguration _configuration;

        public ContextMenuProvider(ILogManager logManager, PluginConfiguration configuration)
        {
            _logger = logManager.GetLogger(Plugin.PluginGuid.ToString());
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        public Task<IEnumerable<ContextMenuItem>> GetContextMenuItems(BaseItem item, User user)
        {
            var menuItems = new List<ContextMenuItem>();

            // Only provide context menu items for supported media types
            if (item is Movie || item is Series || item is Episode || item is Video)
            {
                var languageSubItems = new List<ContextMenuItem>();
                if (_configuration.LanguageMappings != null && _configuration.LanguageMappings.Any())
                {
                    foreach (var mapping in _configuration.LanguageMappings.OrderBy(m => m.Language))
                    {
                        languageSubItems.Add(new ContextMenuItem
                        {
                            Name = mapping.Language, // e.g., "English"
                            Caption = mapping.CountryCode.ToUpper(), // e.g., "US" - short additional info
                            // This will trigger a call to FlagApiController
                            ApiCommand = $"/FlagMyCountry/SetLanguage?ItemId={item.Id}&LanguageCode={mapping.LanguageCode}"
                        });
                    }
                }

                if (languageSubItems.Any())
                {
                     // A non-clickable header to group the language options
                     // Using Name for the header text. IsHeader might be supported by some themes.
                     menuItems.Add(new ContextMenuItem { Name = "Mark Language Flag", IsEnabled = false });
                     menuItems.AddRange(languageSubItems);
                }

                // Add an option to clear the currently set language flag
                // This should appear even if there are no language mappings (to clear a previously set one)
                menuItems.Add(new ContextMenuItem
                {
                    Name = "Clear Language Flag",
                    // Caption = "Remove Flag", // Caption can be omitted if Name is clear
                    ApiCommand = $"/FlagMyCountry/SetLanguage?ItemId={item.Id}&LanguageCode=" // Empty LanguageCode to signify clearing
                });
            }

            return Task.FromResult<IEnumerable<ContextMenuItem>>(menuItems);
        }
    }
}
