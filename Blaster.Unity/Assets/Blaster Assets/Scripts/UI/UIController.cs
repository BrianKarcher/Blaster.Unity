//using Assets.Source.UI;
using Assets.Blaster_Assets.Scripts.UI;
using BlueOrb.Base.Input;
using BlueOrb.Base.Item;
using BlueOrb.Base.Manager;
//using BlueOrb.Base.Skill;
using BlueOrb.Common.Components;
using BlueOrb.Messaging;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BlueOrb.Scripts.UI
{
    [AddComponentMenu("RQ/UI/UI Controller")]
    public class UIController : ComponentBase<UIController>
    {
        [SerializeField]
        private Image OverlayImage;
        [SerializeField]
        private CurrentScore _currentScore;

        [SerializeField]
        private Canvas Canvas;
        [SerializeField]
        private GameObject TempLabel;

        public override string GetId()
        {
            return "UI Controller";
        }

        public override void StartListening()
        {
            base.StartListening();
            MessageDispatcher.Instance.StartListening("CreateTempLabel", GetId(), (data) =>
            {
                var extraInfo = ((Vector3 pos, string text, Color color))data.ExtraInfo;
                var label = GameObject.Instantiate(TempLabel, Canvas.transform);
                // Calculate *screen* position (note, not a canvas/recttransform position)
                Vector2 canvasPos;
                //Vector2 screenPoint = Camera.main.WorldToScreenPoint(offsetPos);

                // Convert screen position to Canvas / RectTransform space <- leave camera null if Screen Space Overlay
                //RectTransformUtility.ScreenPointToLocalPointInRectangle(Canvas., screenPoint, null, out canvasPos);

                //label.transform.localPosition = 
            });
            MessageDispatcher.Instance.StartListening("SetCurrentScore", GetId(), (data) =>
            {
                var currentScore = (int)data.ExtraInfo;
                _currentScore.SetScore(currentScore);
            });
        }

        public Canvas GetCanvas()
        {
            return Canvas;
        }
    }
}
