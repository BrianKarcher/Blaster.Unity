using Assets.Blaster_Assets.Scripts.Components;
using Assets.Blaster_Assets.Scripts.UI;
using BlueOrb.Common.Components;
using BlueOrb.Controller.Manager;
using BlueOrb.Messaging;
using TMPro;
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
                var label = GameObject.Instantiate(TempLabel, Canvas.transform);
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
