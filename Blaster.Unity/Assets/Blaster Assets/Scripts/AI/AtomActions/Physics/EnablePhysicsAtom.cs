using BlueOrb.Common.Container;
using BlueOrb.Physics;

namespace BlueOrb.Scripts.AI.AtomActions
{
    public class EnablePhysicsAtom : AtomActionBase
    {
        public bool Enable;
        public bool RevertOnExit;

        private PhysicsComponent _physicsComponent;
        private bool _previous;

        public override void Start(IEntity entity)
        {
            base.Start(entity);
            if (_physicsComponent == null)
                _physicsComponent = entity.Components.GetComponent<PhysicsComponent>();
            _previous = _physicsComponent.GetEnabled();
            _physicsComponent.SetEnabled(Enable);
        }

        public override void End()
        {
            base.End();
            if (RevertOnExit)
                _physicsComponent.SetEnabled(_previous);
        }
    }
}
