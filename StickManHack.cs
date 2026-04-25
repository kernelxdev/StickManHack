using MelonLoader;
using UnityEngine;

[assembly: MelonInfo(typeof(StickManHack.StickManHack), "StickMan Hack", "1.1.0", "KernelXDev")]
[assembly: MelonGame(null, "Stickman Killing Zombie")]

namespace StickManHack
{
    public partial class StickManHack : MelonMod
    {
        private const KeyCode ToggleMenuKey = KeyCode.Insert;
        private const float InfiniteHealthValue = 100f;
        private const float GoldBoostValue = 999999f;
        private const float GoldPerTick = 500f;
        private const float SpeedMultiplier = 2f;
        private const int MenuWindowId = 4821;

        private static readonly Rect InitialWindowRect = new Rect(24f, 24f, 900f, 680f);

        private bool _showMenu;
        private bool _infiniteHealth;
        private bool _infiniteMoney;
        private bool _maxArmor;
        private bool _speedHack;
        private bool _freezeMissionTimer;
        private bool _autoCompleteCollectGoldObjective;
        private bool _drawWatermark = true;

        private Rect _windowRect = InitialWindowRect;
        private string _statusText = "Ready";
        private string _toastText = string.Empty;
        private float _toastUntil;
        private bool _guiReady;
        private bool _uiErrored;
        private Vector2 _activeHacksScroll;

        private GUIStyle? _windowStyle;
        private GUIStyle? _tabStyle;
        private GUIStyle? _tabStyleActive;
        private GUIStyle? _labelStyle;
        private GUIStyle? _headerStyle;
        private GUIStyle? _buttonStyle;
        private GUIStyle? _statusStyle;
        private GUIStyle? _panelStyle;
        private GUIStyle? _rowLabelStyle;
        private GUIStyle? _sectionTitleStyle;
        private GUIStyle? _chipOnStyle;
        private GUIStyle? _chipOffStyle;

        private Texture2D? _texWindow;
        private Texture2D? _texPanel;
        private Texture2D? _texTab;
        private Texture2D? _texTabActive;
        private Texture2D? _texButton;
        private Texture2D? _texButtonHover;
        private Texture2D? _texChipOn;
        private Texture2D? _texChipOnHover;
        private Texture2D? _texChipOff;
        private Texture2D? _texChipOffHover;

        public override void OnInitializeMelon()
        {
            _showMenu = false;
            LoggerInstance.Msg("StickMan Hack initialized. Press INSERT to toggle menu.");
        }

        public override void OnSceneWasLoaded(int buildIndex, string sceneName)
        {
            _statusText = $"Scene loaded: {sceneName} ({buildIndex})";
            _guiReady = false;
            LoggerInstance.Msg(_statusText);
        }

        public override void OnUpdate()
        {
            if (Input.GetKeyDown(ToggleMenuKey))
            {
                _showMenu = !_showMenu;
            }

            if (_infiniteHealth)
            {
                GameModel.Health = InfiniteHealthValue;
                if (GameModel.ArmorMax > 0)
                {
                    GameModel.Armor = GameModel.ArmorMax;
                }

                GameModel.OnHealthChangedInvoke();
            }

            if (_maxArmor && GameModel.ArmorMax > 0)
            {
                GameModel.Armor = GameModel.ArmorMax;
                GameModel.OnHealthChangedInvoke();
            }

            if (_infiniteMoney)
            {
                var nextGold = Mathf.Max(GameModel.Gold, GoldBoostValue) + GoldPerTick * Time.unscaledDeltaTime;
                ApplyGold(nextGold);
            }

            if (_freezeMissionTimer)
            {
                GameModel.GameTime = 0f;
            }
        }

        public override void OnLateUpdate()
        {
            if (_speedHack && !GameModel.Paused && !GameModel.GameOver)
            {
                Time.timeScale = SpeedMultiplier;
            }
            else if (!_speedHack && Time.timeScale > 1f)
            {
                Time.timeScale = 1f;
            }
        }

        public override void OnGUI()
        {
            if (_uiErrored)
            {
                return;
            }

            try
            {
                EnsureGuiStyles();
                FitWindowToScreen();

                if (_drawWatermark)
                {
                    GUI.Label(new Rect(10f, 10f, 420f, 26f), "KERNEL MENU | [INSERT] Toggle", _statusStyle);
                }

                if (!_showMenu)
                {
                    return;
                }

                _windowRect = GUI.Window(MenuWindowId, _windowRect, DrawWindow, "KERNEL MENU", _windowStyle);
                FitWindowToScreen();

                if (!string.IsNullOrEmpty(_toastText) && Time.unscaledTime < _toastUntil)
                {
                    GUI.Box(new Rect(_windowRect.x + 14f, _windowRect.yMax + 8f, _windowRect.width - 28f, 32f), _toastText);
                }
            }
            catch (System.Exception ex)
            {
                _uiErrored = true;
                _showMenu = false;
                LoggerInstance.Error($"UI crashed and was disabled: {ex}");
            }
        }

        private void FitWindowToScreen()
        {
            const float margin = 16f;
            float maxWidth = Mathf.Max(420f, Screen.width - margin * 2f);
            float maxHeight = Mathf.Max(300f, Screen.height - margin * 2f);

            _windowRect.width = Mathf.Min(_windowRect.width, maxWidth);
            _windowRect.height = Mathf.Min(_windowRect.height, maxHeight);

            _windowRect.x = Mathf.Clamp(_windowRect.x, margin, Screen.width - _windowRect.width - margin);
            _windowRect.y = Mathf.Clamp(_windowRect.y, margin, Screen.height - _windowRect.height - margin);
        }
    }
}