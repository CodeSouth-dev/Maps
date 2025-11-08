# Maps - ExileAPI Plugin

A powerful map analysis plugin for Path of Exile using ExileAPI. This plugin helps you identify valuable maps based on your custom criteria including tier, quantity, rarity, pack size, and specific mods.

## Features

- **Real-time Map Analysis**: Automatically analyzes maps in your inventory and stash
- **Visual Highlighting**: Color-coded borders around maps (green=good, red=bad mods, white=meets criteria)
- **Detailed Overlays**: Shows tier, quantity, rarity, and pack size at a glance
- **Customizable Filters**: Set minimum requirements for tier, quantity, rarity, and pack size
- **Mod Filtering**: Define good and bad mod keywords to automatically evaluate maps
- **Scoring System**: Automatically calculates a score for each map based on your preferences
- **Interactive Tooltips**: Hover over maps to see full details and all mods
- **Hotkey Support**: Quick toggle for overlays and filter reload

## Installation

### Prerequisites
- ExileAPI installed and working
- Visual Studio 2019+ (for compilation)
- .NET Framework 4.8 Developer Pack

### Setup Steps

1. **Clone/Download this repository** to your ExileAPI plugin source folder:
   ```
   HUD\ExileApi\Plugins\Source\Maps\
   ```

2. **Open your ExileAPI solution** in Visual Studio

3. **Add this project to your solution**:
   - Right-click solution → Add → Existing Project
   - Navigate to `Maps.csproj` and add it

4. **Update the project references**:
   - Open `Maps.csproj` in a text editor
   - Update `YOUR_POE_PATH` to your actual Path of Exile installation path
   - Update the ExileApi project reference GUID if needed
   - Save the file

5. **Build the solution**:
   - Build → Build Solution (or press F6)
   - The compiled plugin will be in: `HUD\PoeHelper\Plugins\Compiled\Maps\`

6. **Launch ExileAPI** and enable the plugin from the menu (F12)

## Configuration

Press **F12** in-game to open the ExileAPI menu and navigate to the Maps plugin settings.

### General Settings
- **Enable Map Highlighting**: Show colored borders around maps
- **Enable Overlay Display**: Show stat overlays above maps
- **Show Notifications**: Display alerts for exceptional maps

### Filter Criteria
- **Minimum/Maximum Map Tier**: Set tier range (1-17)
- **Minimum Item Quantity %**: Filter maps below this quantity
- **Minimum Item Rarity %**: Filter maps below this rarity
- **Minimum Pack Size %**: Filter maps below this pack size

### Desired Mods
- **Good Mod Keywords**: Comma-separated list of desirable mod keywords
  - Example: `beyond,breach,harbinger,legion,ritual,delirium`
- **Bad Mod Keywords**: Comma-separated list of mods to avoid
  - Example: `reflect,no regen,cannot leech,reduced recovery`

### Visual Settings
- **Good Map Border Color**: Color for maps with good mods (default: green)
- **Bad Map Border Color**: Color for maps with bad mods (default: red)
- **Neutral Map Border Color**: Color for maps meeting criteria (default: white)
- **Border Thickness**: Width of the highlight border (1-10)
- **Text Size**: Size of overlay text (10-30)
- **Overlay Background Opacity**: Transparency of text background (0-255)

### Hotkeys
- **Toggle Overlay** (default F9): Show/hide the stat overlays
- **Reload Filter** (default F10): Reload mod keyword filters without restarting

## Usage

1. **Open your inventory or stash** containing maps
2. **Maps will automatically be analyzed** and highlighted based on your criteria
3. **Green border** = Map meets criteria AND has good mods
4. **Red border** = Map has bad/dangerous mods
5. **White border** = Map meets your minimum criteria
6. **No highlight** = Map doesn't meet your requirements

### Reading the Overlay

The overlay shows: `T{tier} | Q:{quantity}% R:{rarity}% P:{packsize}%`

Example: `T16 | Q:85% R:42% P:25%`
- Tier 16 map
- 85% increased item quantity
- 42% increased item rarity
- 25% increased pack size

### Interactive Tooltips

**Hover your mouse** over any map to see:
- Full map name
- All map statistics
- Complete list of mods (color-coded)
  - Green text = Good mods (from your keywords)
  - Red text = Bad mods (from your keywords)
  - White text = Neutral mods
- Overall map score

## How the Scoring System Works

Maps are scored based on:
- **+1 point per 1% quantity**
- **+0.5 points per 1% rarity**
- **+2 points per 1% pack size**
- **+50 points** for each good mod keyword matched
- **-100 points** for each bad mod keyword matched

Higher scores = better maps!

## Customization Tips

### For Juicing Maps
```
Minimum Quantity: 80
Minimum Pack Size: 20
Good Mods: beyond,breach,harbinger,legion,delirium,ritual
```

### For Safe Farming
```
Bad Mods: reflect,no regen,temporal chains,cannot leech,reduced recovery
```

### For Boss Rushing
```
Minimum Tier: 14
Good Mods: boss
Bad Mods: reduced quantity,reduced pack
```

### For MF Characters
```
Minimum Quantity: 60
Minimum Rarity: 40
Good Mods: rarity,quantity
```

## Troubleshooting

### Plugin doesn't appear in menu
- Make sure the plugin compiled successfully
- Check that it's in `HUD\PoeHelper\Plugins\Compiled\Maps\`
- Restart ExileAPI

### Maps not being detected
- Make sure your inventory/stash is open
- Check that maps are identified (not white/unidentified)
- Verify the plugin is enabled in settings

### Highlights not showing
- Enable "Enable Map Highlighting" in settings
- Check that maps meet your minimum criteria
- Try lowering your filter requirements temporarily

### Overlays not visible
- Press F9 to toggle overlays
- Check "Enable Overlay Display" setting
- Increase text size if text is too small

## Future Features (Planned)

- Stash tab scanning without opening
- Map base type filtering
- Export/import filter profiles
- Statistics tracking
- Alert sounds for exceptional maps
- Atlas region filtering

## Credits

Built for ExileAPI by CodeSouth-dev
Based on ExileAPI framework by Qvin0000

## License

This project is open source. Use at your own risk.

## Disclaimer

This is a helper/overlay tool that reads game memory. While it doesn't automate gameplay, use at your own discretion. The developer is not responsible for any consequences of using this plugin.