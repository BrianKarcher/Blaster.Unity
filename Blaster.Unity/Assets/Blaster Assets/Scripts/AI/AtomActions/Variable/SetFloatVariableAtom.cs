using BlueOrb.Common.Container;
using BlueOrb.Physics;

namespace BlueOrb.Scripts.AI.AtomActions
{
    public class SetFloatVariableAtom : AtomActionBase
    {
        public enum FloatVariableEnum
        {
            MaxSpeed = 0,
            MaxForce = 1,
            ForceMultiplier = 2,
            DragForce = 3
        }

        public FloatVariableEnum FloatVariable;
        public float Value;
        public bool SetToOriginalValue;
        public bool RevertOnExit;

        private float _originalValue;

        private IPhysicsComponent _physicsComponent;

        public override void Start(IEntity entity)
        {
            base.Start(entity);
            if (_physicsComponent == null)
                _physicsComponent = entity.Components.GetComponent<IPhysicsComponent>();

            float value;
            _originalValue = GetCurrentValue();
            if (SetToOriginalValue)
                value = GetOriginalValue();
            else
                value = Value;
            
            SetValue(value);
        }

        public override void End()
        {
            base.End();
            if (RevertOnExit)
                SetValue(_originalValue);
        }

        public void SetValue(float value)
        {
            switch (FloatVariable)
            {
                case FloatVariableEnum.MaxSpeed:
                    _physicsComponent.Controller.GetPhysicsData().MaxSpeed = value;
                    break;
                case FloatVariableEnum.MaxForce:
                    _physicsComponent.Controller.GetPhysicsData().MaxForce = value;
                    break;
                case FloatVariableEnum.ForceMultiplier:
                    _physicsComponent.Controller.GetPhysicsData().ForceMultiplier = value;
                    break;
                case FloatVariableEnum.DragForce:
                    _physicsComponent.Controller.GetPhysicsData().DragForce = value;
                    break;
            }
        }

        public float GetCurrentValue()
        {
            switch (FloatVariable)
            {
                case FloatVariableEnum.MaxSpeed:
                    return _physicsComponent.Controller.GetPhysicsData().MaxSpeed;
                case FloatVariableEnum.MaxForce:
                    return _physicsComponent.Controller.GetPhysicsData().MaxForce;
                case FloatVariableEnum.ForceMultiplier:
                    return _physicsComponent.Controller.GetPhysicsData().ForceMultiplier;
                case FloatVariableEnum.DragForce:
                    return _physicsComponent.Controller.GetPhysicsData().DragForce;
            }
            return 0f;
        }

        private float GetOriginalValue()
        {
            switch (FloatVariable)
            {
                case FloatVariableEnum.MaxSpeed:
                    return _physicsComponent.Controller.GetOriginalPhysicsData().MaxSpeed;
                case FloatVariableEnum.MaxForce:
                    return _physicsComponent.Controller.GetOriginalPhysicsData().MaxForce;
                case FloatVariableEnum.ForceMultiplier:
                    return _physicsComponent.Controller.GetOriginalPhysicsData().ForceMultiplier;
                case FloatVariableEnum.DragForce:
                    return _physicsComponent.Controller.GetOriginalPhysicsData().DragForce;
            }
            return 0f;
        }
    }
}
