using System;
using System.Collections.Generic;
using System.IO;
using MediaBrowser.Common.Configuration;
using MediaBrowser.Controller.Plugins;
using MediaBrowser.Model.Drawing;
using MediaBrowser.Model.Plugins;
using MediaBrowser.Model.Serialization;
using FlagMyCountry.Configuration; // Namespace for PluginConfiguration

namespace FlagMyCountry
{
    public class Plugin : BasePlugin<PluginConfiguration>, IHasThumbImage, IHasWebPages
    {
        public override string Name => "Flag My Country";
        public override string Description => "Overlay national flags on media content based on the selected language.";

        // A fixed GUID for the plugin
        public static Guid PluginGuid = new Guid("524287f7-1801-4da8-ac37-05ad528894b0");
        public override Guid Id => PluginGuid;

        // Explicitly define plugin version, can also be derived from assembly
        public override Version Version => new Version("1.0.0.0");

        private readonly IJsonSerializer _jsonSerializer; // May not be used here but part of constructor pattern

        public Plugin(IApplicationPaths applicationPaths, IXmlSerializer xmlSerializer, IJsonSerializer jsonSerializer)
            : base(applicationPaths, xmlSerializer)
        {
            _jsonSerializer = jsonSerializer;
            Instance = this; // Make instance available statically if needed by other parts
        }

        // Static instance for easy access to configuration (use with caution, prefer DI)
        public static Plugin Instance { get; private set; }


        public IEnumerable<PluginPageInfo> GetPages()
        {
            return new[]
            {
                new PluginPageInfo
                {
                    Name = "FlagMyCountryConfigPage", // Internal name for the configuration page
                    EmbeddedResourcePath = GetType().Namespace + ".Configuration.FlagMyCountryConfig.html",
                    DisplayName = "Flag My Country", // Display name in Emby settings
                    MenuSection = "Plugins", // Appears in the "Plugins" section of Emby Server Dashboard
                    MenuIcon = "flag", // Uses a built-in Emby icon
                    PluginId = Id // Associates this page with this plugin
                }
            };
        }

        public Stream GetThumbImage()
        {
            var type = GetType();
            // Assumes thumb.png is in an "Images" folder marked as an embedded resource
            return type.Assembly.GetManifestResourceStream(type.Namespace + ".Images.thumb.png");
        }

        public ImageFormat ThumbImageFormat => ImageFormat.Png;
    }
}
