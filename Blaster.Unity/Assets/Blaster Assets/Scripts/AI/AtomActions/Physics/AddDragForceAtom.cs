using BlueOrb.Common.Container;
using BlueOrb.Physics;
using UnityEngine;

namespace BlueOrb.Scripts.AI.AtomActions
{
    public class AddDragForceAtom : AtomActionBase
    {
        public float Force;

        private IPhysicsComponent _physicsComponent;
        private Vector2 _dragForce;

        public override void Start(IEntity entity)
        {
            base.Start(entity);
            if (_physicsComponent == null)
                _physicsComponent = entity.Components.GetComponent<IPhysicsComponent>();

            var currentVelocity = _physicsComponent.GetVelocity2();
            var oppositeNormalized = currentVelocity.normalized * -1f;
            _dragForce = oppositeNormalized * Force;
        }

        public override void OnUpdate()
        {
            base.OnUpdate();
            _physicsComponent.AddForce2(_dragForce);
        }

        public override void End()
        {
            base.End();
        }
    }
}
