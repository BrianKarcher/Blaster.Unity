using BlueOrb.Common.Container;
using BlueOrb.Physics;

namespace BlueOrb.Scripts.AI.AtomActions
{
    public class EnableSteeringBehaviorAtom : AtomActionBase
    {
        public behavior_type behavior_Type;

        private IPhysicsComponent _physicsComponent;

        public override void Start(IEntity entity)
        {
            base.Start(entity);
            if (_physicsComponent == null)
                _physicsComponent = entity.Components.GetComponent<IPhysicsComponent>();
            _physicsComponent.GetSteering().TurnOn(behavior_Type);
        }

        public override void End()
        {
            base.End();
            _physicsComponent.GetSteering().TurnOff(behavior_Type);
        }
    }
}
