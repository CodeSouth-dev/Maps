using System;
using System.Collections.Generic;

namespace Maps
{
    public class FilterProfile
    {
        public string Name { get; set; }
        public int MinTier { get; set; }
        public int MaxTier { get; set; }
        public int MinQuantity { get; set; }
        public int MinRarity { get; set; }
        public int MinPackSize { get; set; }
        public string GoodMods { get; set; }
        public string BadMods { get; set; }

        public FilterProfile(string name = "Custom")
        {
            Name = name;
            MinTier = 1;
            MaxTier = 17;
            MinQuantity = 0;
            MinRarity = 0;
            MinPackSize = 0;
            GoodMods = "";
            BadMods = "";
        }
    }

    public static class ProfilePresets
    {
        public static Dictionary<string, FilterProfile> GetPresets()
        {
            return new Dictionary<string, FilterProfile>
            {
                ["Custom"] = new FilterProfile("Custom"),

                ["Juicing"] = new FilterProfile("Juicing")
                {
                    MinTier = 14,
                    MaxTier = 17,
                    MinQuantity = 80,
                    MinRarity = 0,
                    MinPackSize = 20,
                    GoodMods = "beyond,breach,harbinger,legion,ritual,delirium,abyss,strongbox,shrine",
                    BadMods = "reflect,no regen,less recovery"
                },

                ["Boss Rush"] = new FilterProfile("Boss Rush")
                {
                    MinTier = 14,
                    MaxTier = 17,
                    MinQuantity = 60,
                    MinRarity = 0,
                    MinPackSize = 0,
                    GoodMods = "boss,unique,guardian,conqueror,elder,shaper",
                    BadMods = "reflect,no regen,reduced quantity,reduced rarity"
                },

                ["Safe Farming"] = new FilterProfile("Safe Farming")
                {
                    MinTier = 1,
                    MaxTier = 17,
                    MinQuantity = 40,
                    MinRarity = 0,
                    MinPackSize = 0,
                    GoodMods = "breach,harbinger,essence,metamorph",
                    BadMods = "reflect,no regen,temporal chains,cannot leech,reduced recovery,players are cursed,monsters cannot be stunned,monsters cannot be leeched from"
                },

                ["MF Farming"] = new FilterProfile("MF Farming")
                {
                    MinTier = 1,
                    MaxTier = 17,
                    MinQuantity = 60,
                    MinRarity = 40,
                    MinPackSize = 15,
                    GoodMods = "rarity,quantity,pack,breach,legion,harbinger",
                    BadMods = "reflect,no regen,reduced quantity,reduced rarity"
                },

                ["Delirium"] = new FilterProfile("Delirium")
                {
                    MinTier = 14,
                    MaxTier = 17,
                    MinQuantity = 70,
                    MinRarity = 0,
                    MinPackSize = 25,
                    GoodMods = "beyond,breach,legion,abyss,harbinger,strongbox,additional monster packs",
                    BadMods = "players cannot regenerate,reduced recovery,temporal chains"
                },

                ["Speedrun"] = new FilterProfile("Speedrun")
                {
                    MinTier = 14,
                    MaxTier = 17,
                    MinQuantity = 50,
                    MinRarity = 0,
                    MinPackSize = 10,
                    GoodMods = "quantity,pack,breach,legion",
                    BadMods = "temporal chains,chilled ground,reduced movement,players are cursed,monsters cannot be stunned,reflect"
                }
            };
        }

        public static FilterProfile LoadPreset(string presetName)
        {
            var presets = GetPresets();
            return presets.ContainsKey(presetName) ? presets[presetName] : presets["Custom"];
        }
    }
}
