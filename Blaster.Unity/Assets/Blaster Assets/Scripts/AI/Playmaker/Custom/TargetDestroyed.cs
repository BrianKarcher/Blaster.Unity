using HutongGames.PlayMaker;
using UnityEngine;
using BlueOrb.Controller.Manager;
using BlueOrb.Messaging;

namespace BlueOrb.Scripts.AI.Playmaker.Camera
{
    [ActionCategory("RQ.Custom")]
    [HutongGames.PlayMaker.Tooltip("Add a layer weight lerp (gradual transition).")]
    public class TargetDestroyed : BasePlayMakerAction
    {
        [RequiredField]
        public FsmOwnerDefault gameObject;

        public FsmGameObject Label;
        public FsmVector3 LabelOffset;
        public FsmInt Points;
        public Color Color = Color.white;

        public override void OnEnter()
        {
            Debug.Log("(TargetDestroyed) OnEnter called");
            var go = Fsm.GetOwnerDefaultTarget(gameObject);
            if (go == null)
            {
                return;
            }

            var entity = base.GetEntityBase(go);


            var worldPos = entity.GetPosition() + LabelOffset.Value;

            var points = new PointsData()
            {
                Points = Points.Value,
                Color = Color,
                Position = worldPos
            };
            MessageDispatcher.Instance.DispatchMsg("AddPoints", 0f, entity.GetId(), "Level Controller", points);

            Finish();
        }

        public override void OnExit()
        {
            Debug.Log("(TargetDestroyed) OnExit called");
            base.OnExit();
        }
    }
}
