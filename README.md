# Maps - ExileAPI Plugin

A powerful map analysis plugin for Path of Exile using ExileAPI. This plugin helps you identify valuable maps based on your custom criteria including tier, quantity, rarity, pack size, and specific mods.

## Features

- **Real-time Map Analysis**: Automatically analyzes maps in your inventory, stash, and vendor windows
- **Visual Highlighting**: Color-coded borders around maps (green=good, red=bad mods, white=meets criteria)
- **Detailed Overlays**: Shows tier, quantity, rarity, and pack size at a glance
- **Customizable Filters**: Set minimum requirements for tier, quantity, rarity, and pack size
- **Mod Filtering**: Define good and bad mod keywords to automatically evaluate maps
- **Regex Support**: Use advanced regex patterns for precise mod matching
- **Profile System**: 6 preset profiles + custom profile for different playstyles
- **Vendor Support**: Highlights maps when buying from merchants or trading with players
- **Scoring System**: Automatically calculates a score for each map based on your preferences
- **Interactive Tooltips**: Hover over maps to see full details and all mods
- **Hotkey Support**: Quick toggle for overlays, filter reload, and profile cycling

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
- **Highlight in Vendor Windows**: Also highlight maps when buying from NPCs or trading
- **Use Regex for Mod Matching**: Enable regex patterns instead of simple keywords

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

### Filter Profiles
- **Active Profile**: Select from 7 preset profiles or use Custom
  - **Custom**: Your personalized settings
  - **Juicing**: High quantity/pack size for maximum loot (T14-T17, 80+ quant, 20+ pack)
  - **Boss Rush**: Focused on boss content (T14-T17, 60+ quant, boss-related mods)
  - **Safe Farming**: Avoids dangerous mods while maintaining decent returns
  - **MF Farming**: Magic find optimized (60+ quant, 40+ rarity)
  - **Delirium**: High pack size for delirium content (70+ quant, 25+ pack)
  - **Speedrun**: Fast clear maps without slow mods (T14-T17, 50+ quant)
- **Load Profile**: Apply the selected profile's settings
- **Save Current as Custom**: Save your current settings to Custom profile

### Hotkeys
- **Toggle Overlay** (default F9): Show/hide the stat overlays
- **Reload Filter** (default F10): Reload mod keyword filters without restarting
- **Cycle Profiles** (default F11): Quickly switch between filter profiles

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

## Using Filter Profiles

The plugin comes with 6 preset profiles optimized for different playstyles:

### Quick Start
1. Press **F12** to open the menu
2. Navigate to **Maps Analyzer → Filter Profiles**
3. Select a profile from the **Active Profile** dropdown
4. Click **Load Profile** button
5. Start mapping!

### Profile Details

**Juicing** (Maximum Loot)
- Tier 14-17 only
- 80%+ quantity, 20%+ pack size
- Good mods: beyond, breach, harbinger, legion, ritual, delirium, abyss, strongbox, shrine
- Perfect for high-investment mapping

**Boss Rush** (Guardian/Conqueror Farming)
- Tier 14-17 only
- 60%+ quantity
- Good mods: boss, unique, guardian, conqueror, elder, shaper
- Avoids quantity/rarity reduction mods

**Safe Farming** (HC Viable)
- All tiers
- 40%+ quantity minimum
- Filters dangerous mods: reflect, no regen, temporal chains, cannot leech, reduced recovery, player curses
- Good for magic finding on tanky builds

**MF Farming** (Magic Find)
- All tiers
- 60%+ quantity, 40%+ rarity, 15%+ pack
- Prioritizes rarity/quantity mods
- Great for item farming with MF gear

**Delirium** (Simulacrum/Delirium Orbs)
- Tier 14-17 only
- 70%+ quantity, 25%+ pack size
- Emphasizes pack density mods
- Avoids slow/recovery mods

**Speedrun** (Fast Clears)
- Tier 14-17 only
- 50%+ quantity, 10%+ pack
- Avoids temporal chains, chill, reduced movement, stun immunity
- Optimized for zoom builds

### Creating Custom Filters

1. Adjust settings to your preferences
2. Click **Save Current as Custom**
3. Your settings are now saved to the Custom profile
4. Select "Custom" and click "Load Profile" anytime to restore

### Quick Profile Switching

Press **F11** (default) to cycle through all profiles without opening the menu. Great for switching between farming strategies on the fly!

## Advanced: Regex Filtering

Enable **Use Regex for Mod Matching** to use regular expressions instead of simple keywords.

### Example Regex Patterns

**Match multiple words with OR:**
```
beyond|breach|legion
```

**Match "increased" but not "reduced":**
```
increased.*quantity
```

**Match pack size exactly:**
```
\d+%?\s*(increased|additional)\s*pack\s*size
```

**Match reflect mods:**
```
reflect.*damage|damage.*reflect
```

**Exclude specific mods:**
```
^(?!.*reduced).*quantity
```

### Regex Tips
- Use `|` for OR conditions
- Use `.*` to match anything between words
- Use `\d+` to match numbers
- Use `^` and `$` for start/end of line
- Invalid regex automatically falls back to keyword matching

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

- Stash tab scanning without opening (currently requires stash to be visible)
- Map base type filtering (e.g., only show Strand, Cemetery, etc.)
- Export/import filter profiles to JSON files for sharing
- Statistics tracking (maps run, loot gained, time spent)
- Alert sounds for exceptional maps
- Atlas region filtering
- Map favorite/blacklist system
- Integration with trading APIs

## Credits

Built for ExileAPI by CodeSouth-dev
Based on ExileAPI framework by Qvin0000

## License

This project is open source. Use at your own risk.

## Disclaimer

This is a helper/overlay tool that reads game memory. While it doesn't automate gameplay, use at your own discretion. The developer is not responsible for any consequences of using this plugin.