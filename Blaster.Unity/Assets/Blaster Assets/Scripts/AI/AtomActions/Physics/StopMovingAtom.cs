using BlueOrb.Common.Container;
using BlueOrb.Controller.Player;
using BlueOrb.Physics;

namespace BlueOrb.Scripts.AI.AtomActions
{
    public class StopMovingAtom : AtomActionBase
    {
        private PhysicsComponent _physicsComponent;
        //private PlayerController _playerController;

        public override void Start(IEntity entity)
        {
            base.Start(entity);
            if (_physicsComponent == null)
                _physicsComponent = entity.Components.GetComponent<PhysicsComponent>();
            //if (_playerController == null)
            //    _playerController = entity.Components.GetComponent<PlayerController>();
            _physicsComponent.Stop();
        }
    }
}
