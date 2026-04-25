using UnityEngine;

namespace StickManHack
{
    public partial class StickManHack
    {
        private void CompleteObjectiveHack()
        {
            if (GameModel.LocationInfo == null || GameModel.LocationInfo.MissionObjectivesList == null)
            {
                SetStatus("No active mission detected.");
                return;
            }

            int index = Mathf.Clamp(GameModel.ActiveObjectiveIndex, 0, GameModel.LocationInfo.MissionObjectivesList.Count - 1);
            GameModel.LocationInfo.SetObjectiveComplete(index);
            GameModel.CollectedGoldInGame = Mathf.Max(GameModel.CollectedGoldInGame, GameModel.NeededGold);
            SetStatus($"Marked objective {index + 1} complete.");
        }

        private void DrawToggleRow(string label, ref bool value)
        {
            bool previous = value;
            GUILayout.BeginHorizontal();
            GUILayout.Label(label, _rowLabelStyle, GUILayout.ExpandWidth(true));
            if (GUILayout.Button(value ? "ON" : "OFF", value ? _chipOnStyle : _chipOffStyle, GUILayout.Width(88f), GUILayout.Height(30f)))
            {
                value = !value;
            }
            GUILayout.EndHorizontal();

            if (value != previous)
            {
                SetStatus($"{label}: {(value ? "Enabled" : "Disabled")}");
            }
        }

        private void DrawActiveStatesPanel()
        {
            int activeCount = 0;
            activeCount += DrawStateLine("Infinite Health", _infiniteHealth);
            activeCount += DrawStateLine("Max Armor", _maxArmor);
            activeCount += DrawStateLine("Speed Hack", _speedHack);
            activeCount += DrawStateLine("Infinite Money", _infiniteMoney);
            activeCount += DrawStateLine("Freeze Timer", _freezeMissionTimer);
            activeCount += DrawStateLine("Auto Gold Objective", _autoCompleteCollectGoldObjective);

            if (activeCount == 0)
            {
                GUILayout.Label("No hacks currently enabled.", _statusStyle);
            }
        }

        private int DrawStateLine(string name, bool enabled)
        {
            GUILayout.BeginHorizontal();
            GUILayout.Label(name, _labelStyle, GUILayout.ExpandWidth(true));
            GUI.contentColor = enabled ? new Color(0.4f, 1f, 0.4f, 1f) : new Color(1f, 0.45f, 0.45f, 1f);
            GUILayout.Label(enabled ? "ENABLED" : "DISABLED", _statusStyle, GUILayout.Width(78f));
            GUI.contentColor = Color.white;
            GUILayout.EndHorizontal();
            return enabled ? 1 : 0;
        }

        private void SetStatus(string message)
        {
            _statusText = message;
            _toastText = message;
            _toastUntil = Time.unscaledTime + 2.5f;
            LoggerInstance.Msg(message);
        }

        private void ApplyGold(float value)
        {
            GameModel.Gold = value;
            var manager = csGameManager.Instance;
            if (manager != null)
            {
                manager.goldCount = Mathf.RoundToInt(value);
                var gui = manager.GetComponent<csGUI>();
                if (gui != null)
                {
                    gui.DisplayGoldCount(manager.goldCount);
                }
            }

            if (_autoCompleteCollectGoldObjective)
            {
                GameModel.CollectedGoldInGame = Mathf.Max(GameModel.CollectedGoldInGame, GameModel.NeededGold);
            }

            GameModel.OnCollectCoinsInvoke();
            GameModel.OnGoldFlashInvoke();
        }

        private void ResetToggles()
        {
            _infiniteHealth = false;
            _infiniteMoney = false;
            _maxArmor = false;
            _speedHack = false;
            _freezeMissionTimer = false;
            _autoCompleteCollectGoldObjective = false;
        }
    }
}
