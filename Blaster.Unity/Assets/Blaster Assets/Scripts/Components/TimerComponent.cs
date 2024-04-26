using BlueOrb.Common.Components;
using UnityEngine;
using System.Collections.Generic;
using BlueOrb.Scripts.UI;
using BlueOrb.Controller.Manager;
using BlueOrb.Base.Manager;

namespace BlueOrb.Components
{
    [AddComponentMenu("BlueOrb/Components/Timer")]
    public class TimerComponent : ComponentBase<TimerComponent>
    {
        [SerializeField]
        private float timerLength;
        [SerializeField]
        private float speed;
        [SerializeField]
        private List<GameObject> objectsToDisable;
        private bool isActive;
        private float startTime;

        protected override void Awake()
        {
            this.isActive = true;
            this.startTime = Time.time;
        }

        private void FixedUpdate()
        {
            if (!this.isActive) {
                return;
            }

        }

        private void ShowTime(float time) {
            GameStateController.Instance.UIController.HudController.SetTimer(time.ToString());
        }
    }
}
