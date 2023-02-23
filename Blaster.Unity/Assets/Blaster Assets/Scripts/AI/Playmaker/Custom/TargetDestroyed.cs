using HutongGames.PlayMaker;
using UnityEngine;
using BlueOrb.Controller.Manager;
using BlueOrb.Messaging;
using BlueOrb.Physics;

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
        public FsmBool DisableGravity;
        public FsmBool DisableColliders;
        public Color Color = Color.white;

        private IPhysicsComponent physicsComponent;

        public override void Reset()
        {
            base.Reset();
            DisableGravity = true;
            DisableColliders = true;
        }

        public override void OnEnter()
        {
            //Debug.Log("(TargetDestroyed) OnEnter called");
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
            Debug.Log($"Adding {points} points via {State.Name}");
            MessageDispatcher.Instance.DispatchMsg("AddPoints", 0f, entity.GetId(), "Level Controller", points);

            if (physicsComponent == null)
            {
                physicsComponent = entity.Components.GetComponent<IPhysicsComponent>();
            }
            if (DisableGravity.Value)
            {
                this.physicsComponent?.EnableGravity(false);
            }
            if (DisableColliders.Value)
            {
                var colliders = entity.transform.GetComponents<Collider>();
                for (int i = 0; i < colliders.Length; i++)
                {
                    colliders[i].enabled = false;
                }
            }

            Finish();
        }

        public override void OnExit()
        {
            // Just in case the gravity wasn't disabled before (in case of explosion) disable it now
            this.physicsComponent?.EnableGravity(false);
            Debug.Log("(TargetDestroyed) OnExit called");
            base.OnExit();
        }
    }
}