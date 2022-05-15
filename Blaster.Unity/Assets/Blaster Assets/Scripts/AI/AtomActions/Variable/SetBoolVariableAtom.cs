using BlueOrb.Common.Container;
using BlueOrb.Controller;
using BlueOrb.Controller.Player;
using BlueOrb.Physics;
using UnityEngine;

namespace BlueOrb.Scripts.AI.AtomActions
{
    public class SetBoolVariableAtom : AtomActionBase
    {
        public enum BoolVariableEnum
        {
            //AutoRotateSameAsCamera = 0,
            IkActive = 1,
            AutoTurn = 2
        }

        public BoolVariableEnum Variable;
        public bool Value;
        //public bool SetToOriginalValue;
        public bool RevertOnExit;

        private bool _originalValue;

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

            bool value;
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

        public void SetValue(bool value)
        {
            switch (Variable)
            {
                //case BoolVariableEnum.AutoRotateSameAsCamera:
                //    _playerController.AutoRotateSameAsCamera = value;
                    //_physicsComponent.Controller.GetPhysicsData().MaxSpeed = value;
                    //break;
                case BoolVariableEnum.IkActive:
                    _animator.IkActive = value;
                    break;
                case BoolVariableEnum.AutoTurn:
                    _physicsComponent.GetPhysicsData().AutoTurn = value;
                    break;
            }
        }

        public bool GetCurrentValue()
        {
            switch (Variable)
            {
                //case BoolVariableEnum.AutoRotateSameAsCamera:
                //    return _playerController.AutoRotateSameAsCamera;
                case BoolVariableEnum.IkActive:
                    return _animator.IkActive;
                case BoolVariableEnum.AutoTurn:
                    return _physicsComponent.GetPhysicsData().AutoTurn;
                    //return _physicsComponent.Controller.GetPhysicsData().MaxSpeed;
            }
            throw new System.Exception("Could not locate variable");
        }

        //private float GetOriginalValue()
        //{
        //    switch (FloatVariable)
        //    {
        //        case FloatVariableEnum.MaxSpeed:
        //            return _physicsComponent.Controller.GetOriginalPhysicsData().MaxSpeed;
        //            break;
        //    }
        //    return 0f;
        //}
    }
}
