using HutongGames.PlayMaker;
using BlueOrb.Common.Container;
using BlueOrb.Messaging;
using static BlueOrb.Controller.DollyCartComponent;
using System;

namespace BlueOrb.Scripts.AI.Playmaker.Cinemachine
{
    [ActionCategory("BlueOrb.Cinemachine")]
    [HutongGames.PlayMaker.Tooltip("Set speed of dolly cart.")]
    public class SetDollySpeed : FsmStateAction
    {
        [RequiredField]
        public FsmOwnerDefault gameObject;

        public FsmBool Immediate;
        public FsmFloat Speed;
        public FsmFloat SmoothTime;

        public override void Reset()
        {
            gameObject = null;
            Speed = 5;
            SmoothTime = 2;
        }

        public override void OnEnter()
        {
            var go = Fsm.GetOwnerDefaultTarget(gameObject);
            if (go == null)
            {
                return;
            }

            var entity = go.GetComponent<IEntity>();
            SetSpeedData data = new SetSpeedData();
            data.TargetSpeed = Speed.Value;
            data.SmoothTime = SmoothTime.Value;
            if (entity == null)
                throw new Exception($"Could not locate entity in {Fsm.ActiveStateName}");
            if (Immediate.Value)
            {
                MessageDispatcher.Instance.DispatchMsg("SetSpeed", 0f, string.Empty, entity.GetId(), Speed.Value);
            }
            else
            {
                MessageDispatcher.Instance.DispatchMsg("SetSpeedTarget", 0f, string.Empty, entity.GetId(), data);
            }
            Finish();
        }

        public override void OnExit()
        {
            base.OnExit();
        }
    }
}
