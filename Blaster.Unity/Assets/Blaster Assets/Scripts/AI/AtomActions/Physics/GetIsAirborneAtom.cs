using BlueOrb.Common.Container;
using BlueOrb.Controller.Player;
using BlueOrb.Physics;
using UnityEngine;

namespace BlueOrb.Scripts.AI.AtomActions
{
    public class GetIsAirborneAtom : AtomActionBase
    {
        public float SpeedRange;
        private PhysicsComponent _physicsComponent;
        private PlayerController _playerController;

        public override void Start(IEntity entity)
        {
            base.Start(entity);
            if (_physicsComponent == null)
                _physicsComponent = entity.Components.GetComponent<PhysicsComponent>();
            if (_playerController == null)
                _playerController = entity.Components.GetComponent<PlayerController>();
        }

        public bool GetIsAirborne()
        {
            float verticleSpeed = _physicsComponent.GetVelocity3().y;
            return !_physicsComponent.Controller.GetIsGrounded() && (verticleSpeed > SpeedRange || verticleSpeed < -SpeedRange);
        }
    }
}
