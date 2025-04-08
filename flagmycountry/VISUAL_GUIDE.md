# FlagMyCountry Visual Guide

This guide shows how the FlagMyCountry plugin works with visual examples of flag positions and configurations.

## Plugin Configuration Page

When you navigate to the plugin settings in your Emby dashboard, you'll see a configuration page similar to this:

```
+-----------------------------------------------------------------------+
|                                                                       |
|  Flag My Country                                                      |
|  Overlay national flags on media content based on language.           |
|                                                                       |
|  Flag Display Settings                                                |
|  ------------------------                                             |
|                                                                       |
|  Flag Position:  [Top Right ▼]                                        |
|                                                                       |
|  Flag Size (pixels): [========●=======]  48                           |
|                                                                       |
|  Language Mappings                                                    |
|  ----------------                                                     |
|  Map language codes to country flags.                                 |
|                                                                       |
|  +-------------------------------------------------------+            |
|  | Language | Lang Code | Country Code | Preview | Actions |           |
|  |----------|-----------|--------------|---------|---------|           |
|  | English  | en        | us           | 🇺🇸      | [DEL]   |           |
|  | French   | fr        | fr           | 🇫🇷      | [DEL]   |           |
|  | Spanish  | es        | es           | 🇪🇸      | [DEL]   |           |
|  | German   | de        | de           | 🇩🇪      | [DEL]   |           |
|  | Brazilian| pt-br     | br           | 🇧🇷      | [DEL]   |           |
|  | Portuguese|          |              |         |         |           |
|  +-------------------------------------------------------+            |
|                                                                       |
|  [+ Add Language/Flag]                                                |
|                                                                       |
|  [Save]                                                               |
|                                                                       |
+-----------------------------------------------------------------------+
```

## Flag Position Options

The plugin allows you to position the flag in four different corners of the poster:

### Top Left
```
+-------------------+
| 🇫🇷                |
|                   |
|                   |
|                   |
|      MOVIE        |
|      TITLE        |
|                   |
|                   |
|                   |
+-------------------+
```

### Top Right (Default)
```
+-------------------+
|                 🇺🇸|
|                   |
|                   |
|                   |
|      MOVIE        |
|      TITLE        |
|                   |
|                   |
|                   |
+-------------------+
```

### Bottom Left
```
+-------------------+
|                   |
|                   |
|                   |
|                   |
|      MOVIE        |
|      TITLE        |
|                   |
|                   |
| 🇩🇪                |
+-------------------+
```

### Bottom Right
```
+-------------------+
|                   |
|                   |
|                   |
|                   |
|      MOVIE        |
|      TITLE        |
|                   |
|                   |
|                 🇪🇸|
+-------------------+
```

## Flag Size Examples

The plugin allows you to adjust the flag size from 16px to 128px:

### Small (16px)
```
+-------------------+
|                🇧🇷 |
|                   |
|                   |
|                   |
|      MOVIE        |
|      TITLE        |
|                   |
|                   |
|                   |
+-------------------+
```

### Medium (48px - Default)
```
+-------------------+
|              🇧🇷   |
|                   |
|                   |
|                   |
|      MOVIE        |
|      TITLE        |
|                   |
|                   |
|                   |
+-------------------+
```

### Large (96px)
```
+-------------------+
|          🇧🇷       |
|                   |
|                   |
|                   |
|      MOVIE        |
|      TITLE        |
|                   |
|                   |
|                   |
+-------------------+
```

## Adding a New Language

When you click the "Add Language/Flag" button, a dialog appears:

```
+-------------------------------------+
|   Add Language                      |
|-------------------------------------+
|                                     |
|  Language Name: [Swedish          ] |
|                                     |
|  Language Code: [sv               ] |
|                                     |
|  Country Code:  [se               ] |
|                                     |
|  Flag Preview:  🇸🇪                  |
|                                     |
|  [Cancel]               [Add]       |
+-------------------------------------+
```

## Context Menu Integration

When you right-click on a movie or show poster, you'll see:

```
+-------------------------------------+
|   Play                              |
|   Add to Queue                      |
|   Instant Mix                       |
|   -----------------------------     |
|   Mark Favorite                     |
|   Mark Played                       |
|   -----------------------------     |
|   Mark Language Flag                | <-- The plugin adds this option
|   -----------------------------     |
|   Edit Metadata                     |
+-------------------------------------+
```

## Before and After Examples

### Before Applying Flag
```
+-------------------+
|                   |
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

### After Applying Brazilian Portuguese Flag
```
+-------------------+
|               🇧🇷  |
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

## Server-Wide Visibility

Once one user applies a flag to content, all users on the server will see the flag overlay. This makes it easy to identify the language of content at a glance throughout your media library.

## Real-World Example

Here's how your media library might look after applying flags to different language content:

```
+------------------+ +------------------+ +------------------+
|              🇺🇸  | |              🇫🇷  | |              🇩🇪  |
|                  | |                  | |                  |
|                  | |                  | |                  |
|    INCEPTION     | |     AMÉLIE       | |     RUN LOLA     |
|                  | |                  | |       RUN        |
|                  | |                  | |                  |
+------------------+ +------------------+ +------------------+

+------------------+ +------------------+ +------------------+
|              🇮🇹  | |              🇧🇷  | |              🇯🇵  |
|                  | |                  | |                  |
|                  | |                  | |                  |
|   LIFE IS        | |   CITY OF GOD    | |   SEVEN         |
|   BEAUTIFUL      | |                  | |   SAMURAI       |
|                  | |                  | |                  |
+------------------+ +------------------+ +------------------+
```

This visual guide demonstrates how the FlagMyCountry plugin transforms your Emby library into a more informative visual experience, making it easy to identify content languages at a glance.
