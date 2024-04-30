using BlueOrb.Common.Components;
using UnityEngine;
using BlueOrb.Base.Manager;
using BlueOrb.Components.Action;

namespace BlueOrb.Components
{
    [AddComponentMenu("BlueOrb/Components/Timer")]
    public class TimerComponent : ComponentBase<TimerComponent>
    {
        [SerializeField]
        private float timerLength;
        [SerializeField]
        private float speed;
        private bool isActive;
        private float startTime;
        private int currentTimeLeft;
        private ActionSystem actionSystem;

        protected override void Awake()
        {
            this.actionSystem = GetComponent<ActionSystem>();
        }

        public override void OnEnable()
        {
            this.isActive = true;
            this.startTime = Time.time;
            this.currentTimeLeft = (int)this.timerLength;
            SetTime(currentTimeLeft.ToString());
        }

        private void FixedUpdate()
        {
            if (!this.isActive) {
                return;
            }
            float timeLeft = Time.time - this.startTime;
            if (this.currentTimeLeft != (int)timeLeft) {
                this.currentTimeLeft = (int)timeLeft;
                SetTime(this.currentTimeLeft.ToString());
            }
            if (timeLeft < 0) {
                this.isActive = false;
                SetTime(string.Empty);
                this.actionSystem.Act();
            }

        }

        private void SetTime(string time) {
            GameStateController.Instance.UIController.HudController.SetTimer(time);
        }
    }
}