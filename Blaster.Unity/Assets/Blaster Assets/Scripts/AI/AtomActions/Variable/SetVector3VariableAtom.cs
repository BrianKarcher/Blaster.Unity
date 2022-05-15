using BlueOrb.Common.Container;
using BlueOrb.Controller;
using BlueOrb.Controller.Player;
using BlueOrb.Physics;
using UnityEngine;

namespace BlueOrb.Scripts.AI.AtomActions
{
    public class SetVector3VariableAtom : AtomActionBase
    {
        public enum Vector3VariableEnum
        {
            Gravity = 0
        }

        public Vector3VariableEnum Variable;
        private Vector3 Value;
        //public bool SetToOriginalValue;
        public bool RevertOnExit;

        private Vector3 _originalValue;

        private IPhysicsComponent _physicsComponent;
        //private PlayerController _playerController;
        private AnimationComponent _animator;

        public override void Start(IEntity entity)
        {
            base.Start(entity);
            if (_physicsComponent == null)
                _physicsComponent = entity.Components.GetComponent<IPhysicsComponent>();
            //if (_playerController == null)
            //    _playerController = entity.Components.GetComponent<PlayerController>();
            if (_animator == null)
                _animator = entity.Components.GetComponent<AnimationComponent>();

            Vector3 value;
            _originalValue = GetCurrentValue();
            //if (SetToOriginalValue)
            //    value = GetOriginalValue();
            //else
            value = Value;
            
            SetValue(value);
        }

        public override void End()
        {
            base.End();
            if (RevertOnExit)
                SetValue(_originalValue);
        }

        public void SetValue(Vector3 value)
        {
            switch (Variable)
            {
                case Vector3VariableEnum.Gravity:
                    _physicsComponent.GetPhysicsData().GravityVector = value;
                    break;
            }
        }

        public Vector3 GetCurrentValue()
        {
            switch (Variable)
            {
                case Vector3VariableEnum.Gravity:
                    return _physicsComponent.GetPhysicsData().GravityVector;
            }
            throw new System.Exception("Could not locate variable");
        }

        public void SetVariable(Vector3 value)
        {
            Value = value;
        }
    }
}
