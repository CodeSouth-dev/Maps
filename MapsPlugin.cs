using ExileCore;
using ExileCore.PoEMemory.Components;
using ExileCore.PoEMemory.MemoryObjects;
using ExileCore.Shared.Enums;
using ExileCore.Shared.Cache;
using SharpDX;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Maps
{
    public class MapsPlugin : BaseSettingsPlugin<MapsSettings>
    {
        private IngameState _ingameState;
        private CachedValue<List<MapData>> _cachedMapData;
        private List<string> _goodModKeywords;
        private List<string> _badModKeywords;

        public override bool Initialise()
        {
            Name = "Maps Analyzer";
            _ingameState = GameController.Game.IngameState;

            // Cache map data for 500ms to avoid constant recalculation
            _cachedMapData = new CachedValue<List<MapData>>(() => AnalyzeMaps(), 500);

            UpdateModKeywords();

            LogMessage("Maps plugin initialized successfully!", 3);
            return true;
        }

        public override void Render()
        {
            try
            {
                if (!Settings.Enable.Value) return;
                if (_ingameState == null) return;

                // Handle hotkeys
                if (Settings.ToggleOverlayHotkey.PressedOnce())
                {
                    Settings.EnableOverlay.Value = !Settings.EnableOverlay.Value;
                }

                if (Settings.ReloadFilterHotkey.PressedOnce())
                {
                    UpdateModKeywords();
                    _cachedMapData.ForceUpdate();
                    LogMessage("Filter settings reloaded!", 2);
                }

                // Get map data
                var mapDataList = _cachedMapData.Value;

                if (mapDataList == null || mapDataList.Count == 0) return;

                // Render highlights and overlays for each map
                foreach (var mapData in mapDataList)
                {
                    RenderMapHighlight(mapData);

                    if (Settings.EnableOverlay.Value)
                    {
                        RenderMapOverlay(mapData);
                    }
                }
            }
            catch (Exception ex)
            {
                LogError($"Error in Render: {ex.Message}");
            }
        }

        private List<MapData> AnalyzeMaps()
        {
            var mapDataList = new List<MapData>();

            try
            {
                // Check if inventory is open
                var inventoryPanel = _ingameState?.IngameUi?.InventoryPanel;
                if (inventoryPanel == null || !inventoryPanel.IsVisible) return mapDataList;

                // Get all visible items in inventory
                var items = inventoryPanel[InventoryIndex.PlayerInventory]?.VisibleInventoryItems;
                if (items == null) return mapDataList;

                foreach (var item in items)
                {
                    if (item?.Item == null) continue;

                    var mapData = AnalyzeMapItem(item);
                    if (mapData != null)
                    {
                        mapDataList.Add(mapData);
                    }
                }

                // Also check stash if it's open
                var stashPanel = _ingameState?.IngameUi?.StashElement;
                if (stashPanel != null && stashPanel.IsVisible)
                {
                    var stashItems = stashPanel?.VisibleStash?.VisibleInventoryItems;
                    if (stashItems != null)
                    {
                        foreach (var item in stashItems)
                        {
                            if (item?.Item == null) continue;

                            var mapData = AnalyzeMapItem(item);
                            if (mapData != null)
                            {
                                mapDataList.Add(mapData);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogError($"Error analyzing maps: {ex.Message}");
            }

            return mapDataList;
        }

        private MapData AnalyzeMapItem(NormalInventoryItem inventoryItem)
        {
            try
            {
                var item = inventoryItem.Item;
                var baseComponent = item.GetComponent<Base>();

                // Check if item is a map
                if (baseComponent == null || !baseComponent.Name.Contains("Map"))
                    return null;

                var mods = item.GetComponent<Mods>();
                if (mods == null) return null;

                var mapData = new MapData
                {
                    InventoryItem = inventoryItem,
                    ItemRarity = mods.ItemRarity,
                    Tier = GetMapTier(item),
                    Quantity = GetStatValue(mods, "IncreasedQuantity"),
                    Rarity = GetStatValue(mods, "IncreasedRarity"),
                    PackSize = GetStatValue(mods, "MonsterPackSize"),
                    Mods = GetMapMods(mods),
                    Name = baseComponent.Name
                };

                // Analyze if map meets criteria
                mapData.MeetsCriteria = EvaluateMapCriteria(mapData);
                mapData.Score = CalculateMapScore(mapData);

                return mapData;
            }
            catch (Exception ex)
            {
                LogError($"Error analyzing map item: {ex.Message}");
                return null;
            }
        }

        private int GetMapTier(Entity item)
        {
            try
            {
                var mods = item.GetComponent<Mods>();
                if (mods?.ItemMods == null) return 0;

                // Try to find tier from implicit mods
                var tierMod = mods.ItemMods.FirstOrDefault(m => m.Name.Contains("MapTier"));
                if (tierMod != null && tierMod.Value1 > 0)
                    return tierMod.Value1;

                // Fallback: parse from item name if needed
                return 0;
            }
            catch
            {
                return 0;
            }
        }

        private int GetStatValue(Mods mods, string statName)
        {
            try
            {
                if (mods?.ItemMods == null) return 0;

                var stat = mods.ItemMods.FirstOrDefault(m => m.Name.Contains(statName));
                return stat?.Value1 ?? 0;
            }
            catch
            {
                return 0;
            }
        }

        private List<string> GetMapMods(Mods mods)
        {
            var modList = new List<string>();

            try
            {
                if (mods?.ItemMods == null) return modList;

                foreach (var mod in mods.ItemMods)
                {
                    if (!string.IsNullOrEmpty(mod.DisplayName))
                    {
                        modList.Add(mod.DisplayName);
                    }
                }
            }
            catch (Exception ex)
            {
                LogError($"Error getting map mods: {ex.Message}");
            }

            return modList;
        }

        private bool EvaluateMapCriteria(MapData mapData)
        {
            // Check tier range
            if (mapData.Tier < Settings.MinimumTier.Value || mapData.Tier > Settings.MaximumTier.Value)
                return false;

            // Check quantity
            if (mapData.Quantity < Settings.MinimumQuantity.Value)
                return false;

            // Check rarity
            if (mapData.Rarity < Settings.MinimumRarity.Value)
                return false;

            // Check pack size
            if (mapData.PackSize < Settings.MinimumPackSize.Value)
                return false;

            return true;
        }

        private int CalculateMapScore(MapData mapData)
        {
            int score = 0;

            // Add points for good stats
            score += mapData.Quantity;
            score += mapData.Rarity / 2;
            score += mapData.PackSize * 2;

            // Add points for good mods
            foreach (var mod in mapData.Mods)
            {
                if (_goodModKeywords.Any(keyword => mod.ToLower().Contains(keyword.ToLower())))
                {
                    score += 50;
                    mapData.HasGoodMods = true;
                }

                if (_badModKeywords.Any(keyword => mod.ToLower().Contains(keyword.ToLower())))
                {
                    score -= 100;
                    mapData.HasBadMods = true;
                }
            }

            return score;
        }

        private void RenderMapHighlight(MapData mapData)
        {
            if (!Settings.EnableHighlighting.Value) return;

            try
            {
                var rect = mapData.InventoryItem.GetClientRect();

                // Determine border color
                Color borderColor;
                if (mapData.HasBadMods && Settings.HighlightBadMods.Value)
                {
                    borderColor = Settings.BadMapColor.Value;
                }
                else if (mapData.MeetsCriteria && mapData.HasGoodMods && Settings.HighlightGoodMods.Value)
                {
                    borderColor = Settings.GoodMapColor.Value;
                }
                else if (mapData.MeetsCriteria)
                {
                    borderColor = Settings.NeutralMapColor.Value;
                }
                else
                {
                    return; // Don't highlight if doesn't meet criteria
                }

                // Draw border
                Graphics.DrawFrame(rect, borderColor, Settings.BorderThickness.Value);
            }
            catch (Exception ex)
            {
                LogError($"Error rendering map highlight: {ex.Message}");
            }
        }

        private void RenderMapOverlay(MapData mapData)
        {
            try
            {
                var rect = mapData.InventoryItem.GetClientRect();

                if (!mapData.MeetsCriteria) return;

                // Create overlay text
                var overlayText = $"T{mapData.Tier} | Q:{mapData.Quantity}% R:{mapData.Rarity}% P:{mapData.PackSize}%";

                // Draw semi-transparent background
                var bgColor = Color.Black;
                bgColor.A = (byte)Settings.BackgroundOpacity.Value;
                Graphics.DrawBox(new RectangleF(rect.X, rect.Y - 20, rect.Width, 20), bgColor);

                // Draw text
                var textColor = mapData.HasGoodMods ? Color.LightGreen : Color.White;
                Graphics.DrawText(overlayText, new Vector2(rect.X + 2, rect.Y - 18), textColor, Settings.TextSize.Value);

                // Show score if hovering
                if (rect.Contains(Input.MousePosition))
                {
                    ShowMapTooltip(mapData, rect);
                }
            }
            catch (Exception ex)
            {
                LogError($"Error rendering map overlay: {ex.Message}");
            }
        }

        private void ShowMapTooltip(MapData mapData, RectangleF rect)
        {
            try
            {
                var tooltipLines = new List<string>
                {
                    $"Map: {mapData.Name}",
                    $"Tier: {mapData.Tier}",
                    $"Quality: {mapData.Quantity}%",
                    $"Rarity: {mapData.Rarity}%",
                    $"Pack Size: {mapData.PackSize}%",
                    $"Score: {mapData.Score}",
                    "",
                    "Mods:"
                };

                tooltipLines.AddRange(mapData.Mods);

                var tooltipPos = new Vector2(rect.Right + 10, rect.Top);
                var lineHeight = Settings.TextSize.Value + 2;

                for (int i = 0; i < tooltipLines.Count; i++)
                {
                    var line = tooltipLines[i];
                    var color = Color.White;

                    // Color code mod lines
                    if (i > 7) // Mod lines start after header
                    {
                        if (_goodModKeywords.Any(k => line.ToLower().Contains(k.ToLower())))
                            color = Color.LightGreen;
                        else if (_badModKeywords.Any(k => line.ToLower().Contains(k.ToLower())))
                            color = Color.Red;
                    }

                    Graphics.DrawText(line, new Vector2(tooltipPos.X, tooltipPos.Y + i * lineHeight), color, Settings.TextSize.Value);
                }
            }
            catch (Exception ex)
            {
                LogError($"Error showing tooltip: {ex.Message}");
            }
        }

        private void UpdateModKeywords()
        {
            _goodModKeywords = Settings.GoodModKeywords.Value
                .Split(',')
                .Select(s => s.Trim())
                .Where(s => !string.IsNullOrEmpty(s))
                .ToList();

            _badModKeywords = Settings.BadModKeywords.Value
                .Split(',')
                .Select(s => s.Trim())
                .Where(s => !string.IsNullOrEmpty(s))
                .ToList();
        }
    }

    public class MapData
    {
        public NormalInventoryItem InventoryItem { get; set; }
        public string Name { get; set; }
        public ItemRarity ItemRarity { get; set; }
        public int Tier { get; set; }
        public int Quantity { get; set; }
        public int Rarity { get; set; }
        public int PackSize { get; set; }
        public List<string> Mods { get; set; } = new List<string>();
        public bool MeetsCriteria { get; set; }
        public int Score { get; set; }
        public bool HasGoodMods { get; set; }
        public bool HasBadMods { get; set; }
    }
}
