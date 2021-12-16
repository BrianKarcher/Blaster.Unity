using BlueOrb.Scripts.AI.AtomActions.Animation;
using HutongGames.PlayMaker;
using BlueOrb.Common.Container;
using UnityEngine;
using BlueOrb.Scripts.UI;
using TMPro;
using Assets.Blaster_Assets.Scripts.Components;
using BlueOrb.Controller.Manager;
using BlueOrb.Messaging;

namespace BlueOrb.Scripts.AI.Playmaker.Camera
{
    [ActionCategory("RQ.Custom")]
    [HutongGames.PlayMaker.Tooltip("Add a layer weight lerp (gradual transition).")]
    public class TargetDestroyed : FsmStateAction
    {
        [RequiredField]
        public FsmOwnerDefault gameObject;

        public FsmGameObject Label;
        public FsmVector3 LabelOffset;
        public FsmInt Points;
        public Color Color = Color.white;

        //public AddLayerWeightLerpAtom _atom;

        public override void OnEnter()
        {
            Debug.Log("(TargetDestroyed) OnEnter called");
            var go = Fsm.GetOwnerDefaultTarget(gameObject);
            if (go == null)
            {
                return;
            }

            var entity = go.GetComponent<IEntity>();


            var worldPos = entity.GetPosition() + LabelOffset.Value;

            var points = new PointsData()
            {
                Points = Points.Value,
                Color = Color,
                Position = worldPos
            };
            MessageDispatcher.Instance.DispatchMsg("AddPoints", 0f, entity.GetId(), "Level Controller", points);

            //_atom.Start(entity);
            Finish();
        }

        public override void OnExit()
        {
            Debug.Log("(TargetDestroyed) OnExit called");
            base.OnExit();
            //_atom.End();
        }
    }
}
