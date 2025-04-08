# FlagMyCountry Plugin for Emby

FlagMyCountry is an Emby Server plugin for versions 4.8 and up that adds an overlay flag from the country of the selected language onto any content selected to be easily identified by what language the movie or show is in.

## Features

- Overlay national flags on media content based on the language
- Server-wide flag display (when one user adds a flag, all users will see it)
- Configurable flag size
- Configurable flag position (top-left, top-right, bottom-left, bottom-right)
- Add custom language-to-country mappings
- Language detection from media metadata or audio streams
- Built-in support for many languages, including Brazilian Portuguese
- Right-click context menu integration for easy flag application

## Installation

1. Download the latest release (.dll file) from the Releases section
2. Copy the .dll file to your Emby Server's plugins directory:
   - Windows: `%AppData%\Emby-Server\Plugins`
   - Linux: `/var/lib/emby/plugins` or `~/.emby/plugins`
   - Mac: `~/Library/Application Support/Emby-Server/Plugins`
3. Restart your Emby Server
4. The plugin should appear in your Emby Server's Plugins section

## Usage

### Adding a flag to content

1. Right-click on any movie or show poster content item
2. Select "Mark Language Flag" from the context menu
3. Choose the language from the list
4. The flag will be overlaid on the poster

### Configuration

1. Go to Emby Server Dashboard
2. Navigate to Plugins
3. Click on FlagMyCountry
4. Configure the following options:
   - Flag size (in pixels)
   - Flag position on posters
   - Add or remove language-to-country mappings

## Building from Source

This plugin is built using .NET Standard 2.0. To build it from source:

1. Clone this repository
2. Open the solution in Visual Studio 2019 or later
3. Build the solution
4. The compiled .dll will be in the bin/Release/netstandard2.0 directory

## Flag Images

Flag images are sourced from [flagsapi.com](https://flagsapi.com) and are embedded within the plugin.

## License

This plugin is licensed under the MIT License. See the LICENSE file for details.

## Acknowledgements

- [Emby](https://emby.media/) for their excellent media server
- [FlagsAPI](https://flagsapi.com) for providing the flag images

## Support

If you encounter any issues with this plugin, please create an issue on the GitHub repository.
