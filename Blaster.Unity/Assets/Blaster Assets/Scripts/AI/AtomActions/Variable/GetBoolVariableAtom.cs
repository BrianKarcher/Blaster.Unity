using BlueOrb.Common.Container;
using BlueOrb.Controller;
using BlueOrb.Controller.Player;
using BlueOrb.Physics;
using UnityEngine;

namespace BlueOrb.Scripts.AI.AtomActions
{
    public class GetBoolVariableAtom : AtomActionBase
    {
        public enum BoolVariableEnum
        {
            Grounded = 0,
            CanParry = 1
        }

        public BoolVariableEnum Variable;
        public bool Value;

        private PhysicsComponent _physicsComponent;
        //private PlayerController _playerController;
        //private PlayerParryComponent _playerParryComponent;
        private AnimationComponent _animator;

        public override void Start(IEntity entity)
        {
            base.Start(entity);
            if (_physicsComponent == null)
                _physicsComponent = entity.Components.GetComponent<PhysicsComponent>();
            //if (_playerController == null)
            //    _playerController = entity.Components.GetComponent<PlayerController>();
            if (_animator == null)
                _animator = entity.Components.GetComponent<AnimationComponent>();
            //if (_playerParryComponent == null)
            //    _playerParryComponent = entity.Components.GetComponent<PlayerParryComponent>();
        }

        public override void OnUpdate()
        {
            Value = GetValue();
        }

        public bool GetValue()
        {
            switch (Variable)
            {
                case BoolVariableEnum.Grounded:
                    return _physicsComponent.Controller.GetIsGrounded();
                //case BoolVariableEnum.CanParry:
                //    return _playerParryComponent.CanParry;
            }
            throw new System.Exception("Enum " + Variable.ToString() + " not found");
        }
    }
}
