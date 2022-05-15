using BlueOrb.Common.Container;
using BlueOrb.Physics;
using UnityEngine;

namespace BlueOrb.Scripts.AI.AtomActions
{
    public class IsFacingBigDropAtom : AtomActionBase
    {
        public float Distance;
        private bool _isFacingBigDrop;
        public bool IsFacingBigDrop => _isFacingBigDrop;
        private IPhysicsComponent _physicsComponent;

        public override void Start(IEntity entity)
        {
            base.Start(entity);
            if (_physicsComponent == null)
                _physicsComponent = entity.Components.GetComponent<IPhysicsComponent>();

            _isFacingBigDrop = false;
        }

        public override void OnUpdate()
        {
            base.OnUpdate();
            _isFacingBigDrop = _physicsComponent.Controller.IsFacingBigDrop(Distance);
        }
    }
}
