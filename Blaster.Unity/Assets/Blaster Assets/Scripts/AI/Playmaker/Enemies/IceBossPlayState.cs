using HutongGames.PlayMaker;
using BlueOrb.Common.Container;
using UnityEngine;
using BlueOrb.Base.Extensions;

namespace BlueOrb.Scripts.AI.Playmaker.Enemies
{
    [ActionCategory("BlueOrb")]
    [HutongGames.PlayMaker.Tooltip("Ice Boss Play State")]
    public class IceBossPlayState : BasePlayMakerAction
    {
        [RequiredField]
        public FsmOwnerDefault gameObject;

        public FsmEvent TurnLeftEvent;
        public FsmEvent TurnRightEvent;
        public FsmFloat DegreesBeforeTurn = 30;
        private IEntity entity;

        public override void Reset()
        {
            gameObject = null;
        }

        public override void OnEnter()
        {
            var go = Fsm.GetOwnerDefaultTarget(gameObject);
            if (go == null)
            {
                return;
            }
            this.entity = base.GetEntityBase(go);
        }

        public override void OnUpdate()
        {
            base.OnUpdate();
            Vector2 toTarget = this.entity.Target.transform.position.xz() - this.entity.transform.position.xz();
            float angle = Vector2.SignedAngle(toTarget, this.entity.transform.forward.xz());
            //Debug.Log($"Angle: {angle}");
            if (angle < -DegreesBeforeTurn.Value)
            {
                TurnLeft();
            }
            if (angle > DegreesBeforeTurn.Value)
            {
                TurnRight();
            }
        }

        private void TurnLeft() => Fsm.Event(TurnLeftEvent);
        private void TurnRight() => Fsm.Event(TurnRightEvent);
    }
}