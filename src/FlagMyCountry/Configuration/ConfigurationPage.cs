using MediaBrowser.Controller.Plugins; // For IPlugin
using MediaBrowser.Model.Plugins;     // For IPluginConfigurationPage, PluginPageInfo, ConfigurationPageType
using System;
using System.IO;

namespace FlagMyCountry.Configuration
{
    public class ConfigurationPage : IPluginConfigurationPage
    {
        /// <summary>
        /// Gets the name of the configuration page.
        /// This is displayed in the Emby server dashboard.
        /// </summary>
        public string Name => "Flag My Country Settings"; // Can be more specific than plugin name

        /// <summary>
        /// Gets the HTML stream for the configuration page.
        /// </summary>
        /// <returns>A stream containing the HTML content.</returns>
        public Stream GetHtmlStream()
        {
            var type = GetType();
            // The resource path should match the location of FlagMyCountryConfig.html
            // If Plugin.cs is in FlagMyCountry namespace, and this class is in FlagMyCountry.Configuration,
            // and FlagMyCountryConfig.html is in the Configuration folder, this should be correct.
            return type.Assembly.GetManifestResourceStream(type.Namespace + ".FlagMyCountryConfig.html");
        }

        /// <summary>
        /// Gets or sets the plugin this configuration page is for.
        /// This property is set by the Emby server.
        /// </summary>
        public IPlugin Plugin { get; set; }

        /// <summary>
        /// Gets the type of the configuration page.
        /// </summary>
        public ConfigurationPageType ConfigurationPageType => ConfigurationPageType.PluginConfiguration;

        /// <summary>
        /// Gets the unique ID of the plugin this page belongs to.
        /// </summary>
        public string PluginId => FlagMyCountry.Plugin.PluginGuid.ToString();
    }
}
