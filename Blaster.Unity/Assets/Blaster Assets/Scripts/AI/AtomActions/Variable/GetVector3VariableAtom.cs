using BlueOrb.Common.Container;
using BlueOrb.Controller;
using BlueOrb.Controller.Player;
using BlueOrb.Physics;
using UnityEngine;

namespace BlueOrb.Scripts.AI.AtomActions
{
    public class GetVector3VariableAtom : AtomActionBase
    {
        public enum Vector3VariableEnum
        {
            Gravity = 0
        }

        public Vector3VariableEnum Variable;
        public Vector3 Value;

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

        public Vector3 GetValue()
        {
            switch (Variable)
            {
                case Vector3VariableEnum.Gravity:
                    return _physicsComponent.GetPhysicsData().GravityVector;
            }
            throw new System.Exception("Enum " + Variable.ToString() + " not found");
        }
    }
}
