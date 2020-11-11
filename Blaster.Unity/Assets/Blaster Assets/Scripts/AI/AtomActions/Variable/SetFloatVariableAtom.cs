using BlueOrb.Common.Container;
using BlueOrb.Physics;

namespace BlueOrb.Scripts.AI.AtomActions
{
    public class SetFloatVariableAtom : AtomActionBase
    {
        public enum FloatVariableEnum
        {
            MaxSpeed = 0,
            Gravity = 1
        }

        public FloatVariableEnum FloatVariable;
        public float Value;
        public bool SetToOriginalValue;
        public bool RevertOnExit;

        private float _originalValue;

        private PhysicsComponent _physicsComponent;

        public override void Start(IEntity entity)
        {
            base.Start(entity);
            if (_physicsComponent == null)
                _physicsComponent = entity.Components.GetComponent<PhysicsComponent>();

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
                case FloatVariableEnum.Gravity:
                    _physicsComponent.Controller.GetPhysicsData().Gravity = value;
                    break;
            }
        }

        public float GetCurrentValue()
        {
            switch (FloatVariable)
            {
                case FloatVariableEnum.MaxSpeed:
                    return _physicsComponent.Controller.GetPhysicsData().MaxSpeed;
                case FloatVariableEnum.Gravity:
                    return _physicsComponent.Controller.GetPhysicsData().Gravity;
            }
            return 0f;
        }

        private float GetOriginalValue()
        {
            switch (FloatVariable)
            {
                case FloatVariableEnum.MaxSpeed:
                    return _physicsComponent.Controller.GetOriginalPhysicsData().MaxSpeed;
            }
            return 0f;
        }
    }
}
