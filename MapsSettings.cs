using ExileCore.Shared.Interfaces;
using ExileCore.Shared.Nodes;
using ExileCore.Shared.Attributes;
using System.Windows.Forms;

namespace Maps
{
    public class MapsSettings : ISettings
    {
        public ToggleNode Enable { get; set; } = new ToggleNode(true);

        [Menu("General Settings")]
        public EmptyNode GeneralSettings { get; set; }

        [Menu("Enable Map Highlighting", parentIndex = 0)]
        public ToggleNode EnableHighlighting { get; set; } = new ToggleNode(true);

        [Menu("Enable Overlay Display", parentIndex = 0)]
        public ToggleNode EnableOverlay { get; set; } = new ToggleNode(true);

        [Menu("Show Notifications", parentIndex = 0)]
        public ToggleNode ShowNotifications { get; set; } = new ToggleNode(true);

        [Menu("Highlight in Vendor Windows", "Also highlight maps when buying from merchants/vendors", parentIndex = 0)]
        public ToggleNode HighlightInVendor { get; set; } = new ToggleNode(true);

        [Menu("Use Regex for Mod Matching", "Enable regex patterns instead of simple keyword matching", parentIndex = 0)]
        public ToggleNode UseRegex { get; set; } = new ToggleNode(false);

        [Menu("Filter Criteria")]
        public EmptyNode FilterCriteria { get; set; }

        [Menu("Minimum Map Tier", parentIndex = 1)]
        public RangeNode<int> MinimumTier { get; set; } = new RangeNode<int>(1, 1, 17);

        [Menu("Maximum Map Tier", parentIndex = 1)]
        public RangeNode<int> MaximumTier { get; set; } = new RangeNode<int>(17, 1, 17);

        [Menu("Minimum Item Quantity %", parentIndex = 1)]
        public RangeNode<int> MinimumQuantity { get; set; } = new RangeNode<int>(0, 0, 200);

        [Menu("Minimum Item Rarity %", parentIndex = 1)]
        public RangeNode<int> MinimumRarity { get; set; } = new RangeNode<int>(0, 0, 200);

        [Menu("Minimum Pack Size %", parentIndex = 1)]
        public RangeNode<int> MinimumPackSize { get; set; } = new RangeNode<int>(0, 0, 100);

        [Menu("Desired Mods")]
        public EmptyNode DesiredMods { get; set; }

        [Menu("Good Mod Keywords (comma separated)", "Enter keywords like: beyond, breach, harbinger", parentIndex = 2)]
        public TextNode GoodModKeywords { get; set; } = new TextNode("beyond,breach,harbinger,legion,ritual");

        [Menu("Bad Mod Keywords (comma separated)", "Enter keywords to avoid like: reflect, no regen", parentIndex = 2)]
        public TextNode BadModKeywords { get; set; } = new TextNode("reflect,no regen,cannot leech");

        [Menu("Highlight good mods in green", parentIndex = 2)]
        public ToggleNode HighlightGoodMods { get; set; } = new ToggleNode(true);

        [Menu("Highlight bad mods in red", parentIndex = 2)]
        public ToggleNode HighlightBadMods { get; set; } = new ToggleNode(true);

        [Menu("Visual Settings")]
        public EmptyNode VisualSettings { get; set; }

        [Menu("Good Map Border Color", parentIndex = 3)]
        public ColorNode GoodMapColor { get; set; } = new ColorNode(System.Drawing.Color.FromArgb(200, 0, 255, 0));

        [Menu("Bad Map Border Color", parentIndex = 3)]
        public ColorNode BadMapColor { get; set; } = new ColorNode(System.Drawing.Color.FromArgb(200, 255, 0, 0));

        [Menu("Neutral Map Border Color", parentIndex = 3)]
        public ColorNode NeutralMapColor { get; set; } = new ColorNode(System.Drawing.Color.FromArgb(150, 255, 255, 255));

        [Menu("Border Thickness", parentIndex = 3)]
        public RangeNode<int> BorderThickness { get; set; } = new RangeNode<int>(2, 1, 10);

        [Menu("Text Size", parentIndex = 3)]
        public RangeNode<int> TextSize { get; set; } = new RangeNode<int>(16, 10, 30);

        [Menu("Overlay Background Opacity", parentIndex = 3)]
        public RangeNode<int> BackgroundOpacity { get; set; } = new RangeNode<int>(200, 0, 255);

        [Menu("Filter Profiles")]
        public EmptyNode FilterProfiles { get; set; }

        [Menu("Active Profile", "Select which filter profile to use", parentIndex = 4)]
        public ListNode ActiveProfile { get; set; } = new ListNode
        {
            Values = new System.Collections.Generic.List<string> { "Custom", "Juicing", "Boss Rush", "Safe Farming", "MF Farming", "Delirium", "Speedrun" },
            Value = "Custom"
        };

        [Menu("Load Profile", "Apply the selected profile settings", parentIndex = 4)]
        public ButtonNode LoadProfile { get; set; } = new ButtonNode();

        [Menu("Save Current as Custom", "Save current settings to Custom profile", parentIndex = 4)]
        public ButtonNode SaveCustomProfile { get; set; } = new ButtonNode();

        [Menu("Hotkeys")]
        public EmptyNode Hotkeys { get; set; }

        [Menu("Toggle Overlay", parentIndex = 5)]
        public HotkeyNode ToggleOverlayHotkey { get; set; } = new HotkeyNode(Keys.F9);

        [Menu("Reload Filter", parentIndex = 5)]
        public HotkeyNode ReloadFilterHotkey { get; set; } = new HotkeyNode(Keys.F10);

        [Menu("Cycle Profiles", "Quickly switch between profiles", parentIndex = 5)]
        public HotkeyNode CycleProfilesHotkey { get; set; } = new HotkeyNode(Keys.F11);
    }
}
