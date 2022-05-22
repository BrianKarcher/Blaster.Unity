using HutongGames.PlayMaker;
using BlueOrb.Common.Container;
using UnityEngine;
using BlueOrb.Messaging;
using BlueOrb.Controller.Manager;

namespace BlueOrb.Scripts.AI.Playmaker.Camera
{
    [ActionCategory("RQ.Custom")]
    [HutongGames.PlayMaker.Tooltip("Add a layer weight lerp (gradual transition).")]
    public class AddPoints : BasePlayMakerAction
    {
        [RequiredField]
        public FsmOwnerDefault gameObject;

        public FsmGameObject Label;
        public FsmVector3 LabelOffset;
        public FsmInt Points;
        public Color Color = Color.white;

        public override void OnEnter()
        {
            Debug.Log("(AddPoints) OnEnter called");
            var go = Fsm.GetOwnerDefaultTarget(gameObject);
            if (go == null)
            {
                return;
            }

            var entity = base.GetEntityBase(go);

            // Just add the two. TransformPoint is affected by Scale and we don't want that.
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
            Debug.Log("(AddPoints) OnExit called");
            base.OnExit();
        }
    }
}
