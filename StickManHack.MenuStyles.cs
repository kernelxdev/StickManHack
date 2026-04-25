using UnityEngine;

namespace StickManHack
{
    public partial class StickManHack
    {
        private void EnsureGuiStyles()
        {
            if (_guiReady)
            {
                return;
            }

            BuildTextures();

            _windowStyle = new GUIStyle(GUI.skin.window)
            {
                fontSize = 19,
                fontStyle = FontStyle.Bold,
                alignment = TextAnchor.UpperLeft,
                border = new RectOffset(0, 0, 0, 0),
                padding = new RectOffset(12, 12, 34, 12)
            };

            _windowStyle.normal.background = _texWindow;
            _windowStyle.hover.background = _texWindow;
            _windowStyle.active.background = _texWindow;
            _windowStyle.focused.background = _texWindow;
            _windowStyle.onNormal.background = _texWindow;
            _windowStyle.onHover.background = _texWindow;
            _windowStyle.onActive.background = _texWindow;
            _windowStyle.onFocused.background = _texWindow;

            Color windowTitleColor = new Color(0.45f, 0.88f, 1f, 1f);
            _windowStyle.normal.textColor = windowTitleColor;
            _windowStyle.hover.textColor = windowTitleColor;
            _windowStyle.active.textColor = windowTitleColor;
            _windowStyle.focused.textColor = windowTitleColor;
            _windowStyle.onNormal.textColor = windowTitleColor;
            _windowStyle.onHover.textColor = windowTitleColor;
            _windowStyle.onActive.textColor = windowTitleColor;
            _windowStyle.onFocused.textColor = windowTitleColor;

            _tabStyle = new GUIStyle(GUI.skin.button)
            {
                fontSize = 16,
                fontStyle = FontStyle.Bold,
                alignment = TextAnchor.MiddleLeft,
                padding = new RectOffset(14, 8, 8, 8)
            };
            _tabStyle.normal.background = _texTab;
            _tabStyle.hover.background = _texTab;
            _tabStyle.active.background = _texTab;
            _tabStyle.focused.background = _texTab;
            _tabStyle.normal.textColor = new Color(1f, 0.82f, 0.55f, 1f);

            _tabStyleActive = new GUIStyle(_tabStyle);
            _tabStyleActive.normal.background = _texTabActive;
            _tabStyleActive.normal.textColor = Color.white;

            _headerStyle = new GUIStyle(GUI.skin.label)
            {
                fontSize = 18,
                fontStyle = FontStyle.Bold
            };
            _headerStyle.normal.textColor = new Color(0.42f, 0.9f, 1f, 1f);

            _labelStyle = new GUIStyle(GUI.skin.label)
            {
                fontSize = 14,
                wordWrap = false
            };
            _labelStyle.normal.textColor = new Color(0.9f, 0.92f, 0.98f, 1f);

            _buttonStyle = new GUIStyle(GUI.skin.button)
            {
                fontSize = 15,
                fontStyle = FontStyle.Bold,
                padding = new RectOffset(10, 10, 7, 7)
            };
            _buttonStyle.normal.background = _texButton;
            _buttonStyle.hover.background = _texButtonHover;
            _buttonStyle.active.background = _texButtonHover;
            _buttonStyle.focused.background = _texButton;
            _buttonStyle.normal.textColor = Color.white;

            _statusStyle = new GUIStyle(GUI.skin.label)
            {
                fontSize = 14,
                alignment = TextAnchor.MiddleLeft
            };
            _statusStyle.normal.textColor = new Color(0.55f, 0.95f, 1f, 1f);

            _panelStyle = new GUIStyle(GUI.skin.box)
            {
                padding = new RectOffset(12, 12, 12, 12),
                margin = new RectOffset(0, 0, 0, 0)
            };
            _panelStyle.normal.background = _texPanel;
            _panelStyle.normal.textColor = new Color(0.88f, 0.92f, 1f, 1f);

            _rowLabelStyle = new GUIStyle(_labelStyle)
            {
                alignment = TextAnchor.MiddleLeft,
                fontStyle = FontStyle.Bold,
                wordWrap = false
            };

            _sectionTitleStyle = new GUIStyle(_tabStyle)
            {
                alignment = TextAnchor.MiddleCenter,
                fontSize = 16
            };

            _chipOnStyle = new GUIStyle(GUI.skin.button)
            {
                fontSize = 14,
                fontStyle = FontStyle.Bold,
                alignment = TextAnchor.MiddleCenter
            };
            _chipOnStyle.normal.background = _texChipOn;
            _chipOnStyle.hover.background = _texChipOnHover;
            _chipOnStyle.active.background = _texChipOnHover;
            _chipOnStyle.normal.textColor = Color.white;

            _chipOffStyle = new GUIStyle(_chipOnStyle);
            _chipOffStyle.normal.background = _texChipOff;
            _chipOffStyle.hover.background = _texChipOffHover;
            _chipOffStyle.active.background = _texChipOffHover;

            _guiReady = true;
        }

        private void BuildTextures()
        {
            _texWindow = CreateTex(new Color(0.07f, 0.08f, 0.11f, 0.98f));
            _texPanel = CreateTex(new Color(0.1f, 0.11f, 0.14f, 0.98f));
            _texTab = CreateTex(new Color(0.37f, 0.2f, 0.12f, 1f));
            _texTabActive = CreateTex(new Color(0.48f, 0.28f, 0.16f, 1f));
            _texButton = CreateTex(new Color(0.14f, 0.16f, 0.2f, 1f));
            _texButtonHover = CreateTex(new Color(0.21f, 0.24f, 0.3f, 1f));
            _texChipOn = CreateTex(new Color(0.14f, 0.55f, 0.28f, 1f));
            _texChipOnHover = CreateTex(new Color(0.2f, 0.65f, 0.33f, 1f));
            _texChipOff = CreateTex(new Color(0.45f, 0.18f, 0.18f, 1f));
            _texChipOffHover = CreateTex(new Color(0.58f, 0.24f, 0.24f, 1f));
        }

        private static Texture2D CreateTex(Color color)
        {
            var texture = new Texture2D(1, 1);
            texture.hideFlags = HideFlags.DontUnloadUnusedAsset;
            texture.SetPixel(0, 0, color);
            texture.Apply();
            return texture;
        }
    }
}
