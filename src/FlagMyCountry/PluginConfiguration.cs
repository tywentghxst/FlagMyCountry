using MediaBrowser.Model.Plugins;
using System.Collections.Generic;

namespace FlagMyCountry
{
    public class LanguageMappingItem
    {
        public string Language { get; set; }
        public string LanguageCode { get; set; }
        public string CountryCode { get; set; }
    }

    public enum FlagPosition
    {
        TopLeft,
        TopRight,
        BottomLeft,
        BottomRight
    }

    public class PluginConfiguration : BasePluginConfiguration
    {
        public int FlagSize { get; set; }
        public FlagPosition Position { get; set; }
        public List<LanguageMappingItem> LanguageMappings { get; set; }

        public PluginConfiguration()
        {
            FlagSize = 48; // Default size
            Position = FlagPosition.TopRight; // Default position
            LanguageMappings = new List<LanguageMappingItem>
            {
                // Add some common default mappings
                new LanguageMappingItem { Language = "English", LanguageCode = "en", CountryCode = "US" },
                new LanguageMappingItem { Language = "Spanish", LanguageCode = "es", CountryCode = "ES" },
                new LanguageMappingItem { Language = "French", LanguageCode = "fr", CountryCode = "FR" },
                new LanguageMappingItem { Language = "German", LanguageCode = "de", CountryCode = "DE" },
                new LanguageMappingItem { Language = "Japanese", LanguageCode = "ja", CountryCode = "JP" },
                new LanguageMappingItem { Language = "Brazilian Portuguese", LanguageCode = "pt-BR", CountryCode = "BR" }
            };
        }
    }
}
