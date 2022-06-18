using Assets.Blaster_Assets.Scripts.Components;
using Assets.Blaster_Assets.Scripts.UI;
using BlueOrb.Base.Global;
using BlueOrb.Base.Manager;
using BlueOrb.Common.Components;
using BlueOrb.Controller.Manager;
using BlueOrb.Controller.Scene;
using BlueOrb.Messaging;
using Rewired.Integration.UnityUI;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace BlueOrb.Scripts.UI
{
    [AddComponentMenu("RQ/UI/UI Controller")]
    public class UIController : ComponentBase<UIController>
    {
        public const string UIControllerId = "UI Controller";
        public const string EnableCanvasEvent = "EnableCanvas";
        public const string DisableCanvasEvent = "DisableCanvas";

        [SerializeField]
        private Image OverlayImage;
        [SerializeField]
        private CurrentScore _currentScore;
        [SerializeField]
        private GameStateController gameStateController;
        [SerializeField]
        private RewiredEventSystem rewiredEventSystem;

        [SerializeField]
        private Canvas canvas;
        [SerializeField]
        private GameObject HUD;
        [SerializeField]
        private GameObject TempLabel;
        [SerializeField]
        private Slider musicSlider;
        [SerializeField]
        private Slider soundEffectSlider;
        [SerializeField]
        private Slider sensitivitySlider;

        private List<Canvas> canvases;

        //private Dictionary<string, Canvas> canvasesDict;

        public override string GetId()
        {
            return UIControllerId;
        }

        protected override void Awake()
        {
            canvases = new List<Canvas>();
            foreach (Transform child in canvas.transform)
            {
                child.gameObject.SetActive(false);
            }
            base.Awake();
        }

        private void Start()
        {
            SettingsController2 settingsController2 = this.gameStateController.SettingsController;
            this.musicSlider.SetValueWithoutNotify(settingsController2.GetMusicVolume());
            this.soundEffectSlider.SetValueWithoutNotify(settingsController2.GetEffectVolume());
        }

        public override void StartListening()
        {
            base.StartListening();
            MessageDispatcher.Instance.StartListening("CreatePointsLabel", GetId(), (data) =>
            {
                var points = (PointsData)data.ExtraInfo;
                var pos = UnityEngine.Camera.main.WorldToScreenPoint(points.Position);

                var canvas = GetCanvas();

                var label = GameObject.Instantiate(TempLabel, pos, Quaternion.identity, canvas.transform);
                // Calculate *screen* position (note, not a canvas/recttransform position)
                var pinComponent = label.GetComponent<PinUIToWorldSpaceComponent>();
                pinComponent.SetWorldPosition(points.Position);

                var textMeshPro = label.GetComponent<TextMeshProUGUI>();
                string prefix = string.Empty;

                textMeshPro.color = points.Color;
                textMeshPro.SetText(prefix + points.Points);
            });
            MessageDispatcher.Instance.StartListening("CreateTempLabel", GetId(), (data) =>
            {
                var extraInfo = ((Vector3 pos, string text, Color color))data.ExtraInfo;
                var label = GameObject.Instantiate(TempLabel, HUD.transform);
            });
            MessageDispatcher.Instance.StartListening("SetCurrentScore", GetId(), (data) =>
            {
                var currentScore = (int)data.ExtraInfo;
                _currentScore.SetScore(currentScore);
            });
            //MessageDispatcher.Instance.StartListening(EnableCanvasEvent, GetId(), (data) =>
            //{
            //    string canvas = (string)data.ExtraInfo;
            //    if (!canvasesDict.ContainsKey(canvas))
            //    {
            //        Debug.LogError($"Canvas {canvas} does not exist");
            //        return;
            //    }
            //    canvasesDict[canvas].gameObject.SetActive(true);
            //});
            //MessageDispatcher.Instance.StartListening(DisableCanvasEvent, GetId(), (data) =>
            //{
            //    string canvas = (string)data.ExtraInfo;
            //    if (!canvasesDict.ContainsKey(canvas))
            //    {
            //        Debug.LogError($"Canvas {canvas} does not exist");
            //        return;
            //    }
            //    canvasesDict[canvas].gameObject.SetActive(false);
            //});
        }

        public GameObject GetCanvas()
        {
            return HUD;
        }

        public void ButtonClicked(string button)
        {
            Debug.Log($"Button Clicked: {button}");
            MessageDispatcher.Instance.DispatchMsg("ButtonClicked", 0f, GetId(), _componentRepository.GetId(), button);
        }

        public void LevelSelect(SceneConfig sceneConfig)
        {
            GlobalStatic.NextScene = sceneConfig.SceneName;
            GlobalStatic.NextSceneConfig = sceneConfig;
            MessageDispatcher.Instance.DispatchMsg("LevelSelected", 0f, GetId(), GetId(), null);
        }

        public void SetDifficulty(string difficulty)
        {
            Debug.Log($"Button Clicked: {difficulty}");
            GlobalStatic.Difficulty = difficulty;
            MessageDispatcher.Instance.DispatchMsg("DifficultySelected", 0f, GetId(), GetId(), null);
        }

        public void SetEffectVolume()
        {
            float volume = this.soundEffectSlider.value;
            Debug.Log($"Setting Sfx Volume to {volume}");
            this.gameStateController.SettingsController.SetEffectVolume(volume);
        }

        public void SetMusicVolume()
        {
            float volume = this.musicSlider.value;
            Debug.Log($"Setting Music Volume to {volume}");
            this.gameStateController.SettingsController.SetMusicVolume(volume);
        }

        public void SetSensitivity()
        {
            this.gameStateController.SettingsController.SetSensitivity(this.sensitivitySlider.value);
        }

        public void SettingsBackClicked()
        {
            Debug.Log($"Button Clicked: Back");
            this.gameStateController.SettingsController.SaveSettings();
            MessageDispatcher.Instance.DispatchMsg("BackClicked", 0f, GetId(), UIController.UIControllerId, null);
        }
    }
}