using BlueOrb.Common.Container;
using BlueOrb.Physics;

namespace BlueOrb.Scripts.AI.AtomActions
{
    public class JumpViaDistanceAtom : AtomActionBase
    {
        private PhysicsComponent _physicsComponent;
        private float _distance;

        public override void Start(IEntity entity)
        {
            base.Start(entity);
            if (_physicsComponent == null)
                _physicsComponent = entity.Components.GetComponent<PhysicsComponent>();
            _physicsComponent.JumpViaDistance(_distance);
        }

        public void SetDistance(float distance)
        {
            _distance = distance;
        }
    }
}
