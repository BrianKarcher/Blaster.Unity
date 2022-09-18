using BlueOrb.Scripts.AI.Playmaker;
using HutongGames.PlayMaker;
using BlueOrb.Controller;
using UnityEngine;
using Tooltip = HutongGames.PlayMaker.TooltipAttribute;
using BlueOrb.Messaging;
using BlueOrb.Common.Container;
using Assets.Blaster_Assets.Scripts.Components;

namespace BlueOrb.Scripts.AI.PlayMaker.DollyCart
{
    [ActionCategory("BlueOrb.DollyCart")]
    [Tooltip("Dolly cart driving!.")]
    public class DollyCartGo : BasePlayMakerAction
    {
        private const string StopCartTimerMessage = "StopCartTimerMessage";

        [RequiredField]
        public FsmOwnerDefault gameObject;

        //[UIHint(UIHint.Layer)]
        //[HutongGames.PlayMaker.Tooltip("Layers to check.")]
        //public FsmInt[] Layer;

        public FsmEvent EnemyCollision;
        public FsmEvent Idle;
        public FsmEvent TimerBrake;
        public FsmFloat StopTime;

        private DollyCartComponent dollyCart;
        //private int layerMask;
        private Collider[] colliders = new Collider[20];

        private long stopMessageTimerId;
        private IEntity entity;

        public override void Reset()
        {
            gameObject = null;
        }

        public override void OnPreprocess()
        {
            base.OnPreprocess();
            Fsm.HandleFixedUpdate = true;
        }

        public override void OnEnter()
        {
            Debug.Log("DollyCartGo Entered");
            var go = Fsm.GetOwnerDefaultTarget(gameObject);
            if (go == null)
            {
                return;
            }

            this.entity = base.GetEntityBase(go);
            dollyCart ??= entity.GetComponent<DollyCartComponent>();
            //if (!dollyCart.HasCart)
            //{
            //    Fsm.Event(Idle);
            //}

            //layerMask = ActionHelpers.LayerArrayToLayerMask(Layer, false);
            dollyCart.StartAcceleration(dollyCart.TargetSpeed, dollyCart.SmoothTime);
            StartListening(this.entity);
        }

        public override void OnExit()
        {
            base.OnExit();
            Debug.Log("DollyCartGo Exited");
        }

        public void StartListening(IEntity entity)
        {
            this.stopMessageTimerId = MessageDispatcher.Instance.StartListening(StopCartTimerMessage, entity.GetId(), (data) =>
            {
                float time = (float)data.ExtraInfo;
                StopTime.Value = time;
                Fsm.Event(TimerBrake);
            });
        }

        public void StopListening()
        {
            MessageDispatcher.Instance.StopListening(StopCartTimerMessage, this.entity.GetId(), this.stopMessageTimerId);
        }

        public override void OnFixedUpdate()
        {
            base.OnFixedUpdate();
            dollyCart.ProcessDollyCartSpeedChange();
        }
    }
}