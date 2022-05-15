using BlueOrb.Scripts.AI.Playmaker;
using HutongGames.PlayMaker;
using BlueOrb.Physics;

namespace BlueOrb.Scripts.AI.PlayMaker.Attack
{
    [ActionCategory("BlueOrb.Variable")]
    public class SetFloatVariable2 : BasePlayMakerAction
    {
        [RequiredField]
        public FsmOwnerDefault gameObject;
        public enum FloatVariableEnum
        {
            MaxSpeed = 0,
            MaxForce = 1,
            ForceMultiplier = 2
        }

        public FloatVariableEnum FloatVariable;
        public FsmFloat Value;
        public bool SetToOriginalValue;
        public bool RevertOnExit;

        private float _originalValue;

        private IPhysicsComponent _physicsComponent;

        public override void Reset()
        {
            gameObject = null;
        }

        public override void OnEnter()
        {
            var go = Fsm.GetOwnerDefaultTarget(gameObject);
            if (go == null)
            {
                return;
            }
            var entity = base.GetEntityBase(go);
            if (_physicsComponent == null)
                _physicsComponent = entity.Components.GetComponent<IPhysicsComponent>();

            float value;
            _originalValue = GetCurrentValue();
            if (SetToOriginalValue)
                value = GetOriginalValue();
            else
                value = Value.Value;

            SetValue(value);
            Finish();
        }

        public override void OnExit()
        {
            base.OnExit();
            if (RevertOnExit)
                SetValue(_originalValue);
        }

        public void SetValue(float value)
        {
            switch (FloatVariable)
            {
                case FloatVariableEnum.MaxSpeed:
                    _physicsComponent.GetPhysicsData().MaxSpeed = value;
                    break;
                case FloatVariableEnum.MaxForce:
                    _physicsComponent.GetPhysicsData().MaxForce = value;
                    break;
                case FloatVariableEnum.ForceMultiplier:
                    _physicsComponent.GetPhysicsData().ForceMultiplier = value;
                    break;
            }
        }

        public float GetCurrentValue()
        {
            switch (FloatVariable)
            {
                case FloatVariableEnum.MaxSpeed:
                    return _physicsComponent.GetPhysicsData().MaxSpeed;
                case FloatVariableEnum.MaxForce:
                    return _physicsComponent.GetPhysicsData().MaxForce;
                case FloatVariableEnum.ForceMultiplier:
                    return _physicsComponent.GetPhysicsData().ForceMultiplier;
            }
            return 0f;
        }

        private float GetOriginalValue()
        {
            switch (FloatVariable)
            {
                case FloatVariableEnum.MaxSpeed:
                    return _physicsComponent.GetOriginalPhysicsData().MaxSpeed;
                case FloatVariableEnum.MaxForce:
                    return _physicsComponent.GetOriginalPhysicsData().MaxForce;
                case FloatVariableEnum.ForceMultiplier:
                    return _physicsComponent.GetOriginalPhysicsData().ForceMultiplier;
            }
            return 0f;
        }
    }
}