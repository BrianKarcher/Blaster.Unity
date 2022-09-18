using HutongGames.PlayMaker;
using BlueOrb.Messaging;
using System;
using static BlueOrb.Controller.DollyCartJointComponent;

namespace BlueOrb.Scripts.AI.Playmaker.Cinemachine
{
    [ActionCategory("BlueOrb.Cinemachine")]
    [HutongGames.PlayMaker.Tooltip("Set speed of dolly cart.")]
    public class SetDollySpeed : BasePlayMakerAction
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

            var entity = base.GetEntityBase(go);
            SetSpeedData data = new SetSpeedData();
            data.TargetSpeed = Speed.Value;
            data.SmoothTime = SmoothTime.Value;
            data.Immediate = Immediate.Value;
            if (entity == null)
                throw new Exception($"Could not locate entity in {Fsm.ActiveStateName}");

            MessageDispatcher.Instance.DispatchMsg("SetSpeed", 0f, string.Empty, entity.GetId(), data);

            //if (Immediate.Value)
            //{
            //    SetSpeedData setSpeedData = new() { TargetSpeed = Speed.Value, Immediate = true, SmoothTime = 0f };
            //    MessageDispatcher.Instance.DispatchMsg("SetSpeed", 0f, string.Empty, entity.GetId(), setSpeedData);
            //}
            //else
            //{
            //    MessageDispatcher.Instance.DispatchMsg("SetSpeedTarget", 0f, string.Empty, entity.GetId(), data);
            //}
            Finish();
        }

        public override void OnExit()
        {
            base.OnExit();
        }
    }
}
