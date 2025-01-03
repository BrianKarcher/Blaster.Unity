﻿using Assets.Blaster_Assets.Scripts.Components;
using Assets.Blaster_Assets.Scripts.UI;
using Assets.BlueOrb.Scripts.UI;
using BlueOrb.Base.Global;
using BlueOrb.Base.Manager;
using BlueOrb.Common.Components;
using BlueOrb.Controller.Manager;
using BlueOrb.Controller.Scene;
using BlueOrb.Controller.UI;
using BlueOrb.Messaging;
using Rewired.Integration.UnityUI;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace BlueOrb.Scripts.UI
{
    [AddComponentMenu("RQ/UI/UI Controller")]
    public class UIController : ComponentBase<UIController>, IUIController
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

        [SerializeField]
        private HudController hudController;
        public IHudController HudController => hudController;

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

                textMeshPro.color = points.Color;
                Debug.Log($"Adding {points.Points} points");
                textMeshPro.SetText(points.Points.ToString());
            });
            MessageDispatcher.Instance.StartListening("CreateTempLabel", GetId(), (data) =>
            {
                var extraInfo = ((Vector3 pos, string text, Color color))data.ExtraInfo;
                var label = GameObject.Instantiate(TempLabel, HUD.transform);
            });
            MessageDispatcher.Instance.StartListening("SetCurrentScore", GetId(), (data) =>
            {
                var currentScoreData = ((int Score, bool Immediate))data.ExtraInfo;
                if (currentScoreData.Immediate)
                {
                    _currentScore.SetScoreImmediate(currentScoreData.Score);
                }
                else
                {
                    _currentScore.SetScore(currentScoreData.Score);
                }
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
    }
}