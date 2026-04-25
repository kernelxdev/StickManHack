using UnityEngine;

namespace StickManHack
{
    public partial class StickManHack
    {
        private void DrawWindow(int windowId)
        {
            const float horizontalPadding = 40f;
            const float panelGap = 8f;
            float availableWidth = Mathf.Max(320f, _windowRect.width - horizontalPadding);
            float availableHeight = Mathf.Max(320f, _windowRect.height - 70f);
            bool useTwoByTwoLayout = availableWidth < 860f;

            if (useTwoByTwoLayout)
            {
                float columnWidth = Mathf.Floor((availableWidth - panelGap) / 2f);
                float rowHeight = Mathf.Max(150f, Mathf.Floor((availableHeight - panelGap) / 2f));

                GUILayout.BeginHorizontal();
                DrawCombatSection(columnWidth, rowHeight);
                GUILayout.Space(panelGap);
                DrawEconomySection(columnWidth, rowHeight);
                GUILayout.EndHorizontal();

                GUILayout.Space(panelGap);

                GUILayout.BeginHorizontal();
                DrawWorldSection(columnWidth, rowHeight);
                GUILayout.Space(panelGap);
                DrawUtilitySection(columnWidth, rowHeight);
                GUILayout.EndHorizontal();
            }
            else
            {
                float columnWidth = Mathf.Floor((availableWidth - panelGap * 3f) / 4f);
                float columnHeight = Mathf.Max(300f, availableHeight);

                GUILayout.BeginHorizontal();
                DrawCombatSection(columnWidth, columnHeight);
                GUILayout.Space(panelGap);
                DrawEconomySection(columnWidth, columnHeight);
                GUILayout.Space(panelGap);
                DrawWorldSection(columnWidth, columnHeight);
                GUILayout.Space(panelGap);
                DrawUtilitySection(columnWidth, columnHeight);
                GUILayout.EndHorizontal();
            }

            GUILayout.Space(8f);
            GUILayout.Label($"Status: {_statusText}", _statusStyle);
            GUI.DragWindow(new Rect(0f, 0f, 10000f, 24f));
        }

        private void DrawCombatSection(float width, float height)
        {
            GUILayout.BeginVertical(_panelStyle, GUILayout.Width(width), GUILayout.Height(height));
            GUILayout.Label("Combat", _sectionTitleStyle, GUILayout.Height(32f));
            GUILayout.Space(6f);
            DrawToggleRow("Infinite Health", ref _infiniteHealth);
            DrawToggleRow("Max Armor", ref _maxArmor);
            DrawToggleRow("Speed Hack", ref _speedHack);

            GUILayout.Space(6f);
            if (GUILayout.Button("Refill Stats", _buttonStyle, GUILayout.Height(34f)))
            {
                GameModel.Health = InfiniteHealthValue;
                if (GameModel.ArmorMax > 0)
                {
                    GameModel.Armor = GameModel.ArmorMax;
                }

                GameModel.OnHealthChangedInvoke();
                SetStatus("Refilled player stats.");
            }

            GUILayout.EndVertical();
        }

        private void DrawEconomySection(float width, float height)
        {
            GUILayout.BeginVertical(_panelStyle, GUILayout.Width(width), GUILayout.Height(height));
            GUILayout.Label("Economy", _sectionTitleStyle, GUILayout.Height(32f));
            GUILayout.Space(6f);
            DrawToggleRow("Infinite Money", ref _infiniteMoney);
            DrawToggleRow("Auto Gold Obj", ref _autoCompleteCollectGoldObjective);

            GUILayout.Space(6f);
            if (GUILayout.Button("Add 10K Gold", _buttonStyle, GUILayout.Height(34f)))
            {
                ApplyGold(GameModel.Gold + 10000f);
                SetStatus("Added 10,000 gold.");
            }

            if (GUILayout.Button("Set 999,999", _buttonStyle, GUILayout.Height(34f)))
            {
                ApplyGold(GoldBoostValue);
                SetStatus("Gold boosted to 999,999.");
            }

            GUILayout.EndVertical();
        }

        private void DrawWorldSection(float width, float height)
        {
            GUILayout.BeginVertical(_panelStyle, GUILayout.Width(width), GUILayout.Height(height));
            GUILayout.Label("World", _sectionTitleStyle, GUILayout.Height(32f));
            GUILayout.Space(6f);
            DrawToggleRow("Freeze Timer", ref _freezeMissionTimer);

            GUILayout.Space(6f);
            if (GUILayout.Button("Kill Zombies", _buttonStyle, GUILayout.Height(34f)))
            {
                csCommon.DeleteAllEnemiesInGame();
                SetStatus("Deleted all enemies in current scene.");
            }

            if (GUILayout.Button("Complete Obj", _buttonStyle, GUILayout.Height(34f)))
            {
                CompleteObjectiveHack();
            }

            GUILayout.EndVertical();
        }

        private void DrawUtilitySection(float width, float height)
        {
            GUILayout.BeginVertical(_panelStyle, GUILayout.Width(width), GUILayout.Height(height));
            GUILayout.Label("Utility", _sectionTitleStyle, GUILayout.Height(32f));
            GUILayout.Space(6f);
            DrawToggleRow("Show watermark", ref _drawWatermark);

            GUILayout.Space(6f);
            if (GUILayout.Button("Reset Toggles", _buttonStyle, GUILayout.Height(34f)))
            {
                ResetToggles();
                SetStatus("All toggles reset.");
            }

            GUILayout.Space(10f);
            GUILayout.Label("Active Hacks", _headerStyle);
            _activeHacksScroll = GUILayout.BeginScrollView(_activeHacksScroll, GUILayout.Height(Mathf.Max(80f, height - 210f)));
            DrawActiveStatesPanel();
            GUILayout.EndScrollView();

            GUILayout.FlexibleSpace();
            GUILayout.Label("Insert: Toggle menu", _statusStyle);
            GUILayout.EndVertical();
        }
    }
}
