# FlagMyCountry Visual Guide

This guide demonstrates how the FlagMyCountry plugin works with visual examples.

## Configuration Page

When you navigate to the plugin settings in your Emby dashboard, you'll see a configuration page that allows you to:

- Set the flag position (Top Left, Top Right, Bottom Left, Bottom Right)
- Adjust the flag size (16px to 128px)
- Manage language mappings

## Flag Positions

The plugin supports four flag positions:

```
Top Left                Top Right (Default)
+---+----------+       +----------+---+
|🇫🇷 |          |       |          |🇺🇸 |
|   |          |       |          |   |
|   |  MOVIE   |       |  MOVIE   |   |
|   |  TITLE   |       |  TITLE   |   |
|   |          |       |          |   |
+---+----------+       +----------+---+

Bottom Left             Bottom Right
+----------+---+       +---+----------+
|          |   |       |   |          |
|          |   |       |   |          |
|  MOVIE   |   |       |   |  MOVIE   |
|  TITLE   |   |       |   |  TITLE   |
|          |🇩🇪 |       |🇪🇸 |          |
+----------+---+       +---+----------+
```

## Before & After Examples

### Movie Poster Before Flag
```
+-------------------+
|                   |
|                   |
|                   |
|      CITY OF      |
|       GOD         |
|                   |
|                   |
|                   |
+-------------------+
```

### After Adding Brazilian Portuguese Flag
```
+-------------------+
|               🇧🇷  |
|                   |
|                   |
|      CITY OF      |
|       GOD         |
|                   |
|                   |
|                   |
+-------------------+
```

## Adding Flags via Context Menu

1. Right-click on any movie or show poster
2. Select "Mark Language Flag"
3. Choose the language
4. The flag appears on the poster immediately

## Media Library with Flags

After applying flags to content in different languages, your library will look like this:

```
+------------------+ +------------------+ +------------------+
|              🇺🇸  | |              🇫🇷  | |              🇩🇪  |
|                  | |                  | |                  |
|    INCEPTION     | |     AMÉLIE       | |     RUN LOLA     |
|                  | |                  | |       RUN        |
+------------------+ +------------------+ +------------------+

+------------------+ +------------------+ +------------------+
|              🇮🇹  | |              🇧🇷  | |              🇯🇵  |
|                  | |                  | |                  |
|   LIFE IS        | |   CITY OF GOD    | |     SEVEN        |
|   BEAUTIFUL      | |                  | |     SAMURAI      |
+------------------+ +------------------+ +------------------+
```

## Adding New Languages

Using the configuration page, you can add new language mappings:

1. Click "Add Language/Flag"
2. Enter language name (e.g., "Swedish")
3. Enter language code (e.g., "sv")
4. Enter country code (e.g., "se")
5. You'll see a flag preview automatically
6. Click "Add"

The new language will be available immediately for marking content.
