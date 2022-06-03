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
            ForceMultiplier = 2,
            DragForce = 3
        }

        public FloatVariableEnum FloatVariable;
        public FsmFloat Value;
        public bool SetToOriginalValue;
        public bool RevertOnExit;
        public bool ByPercent;

        private float _originalValue;

        private IPhysicsComponent _physicsComponent;
        private PhysicsData physicsData;
        private PhysicsData originalPhysicsData;

        public override void Reset()
        {
            gameObject = null;
            ByPercent = false;
            RevertOnExit = false;
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
            this.physicsData = _physicsComponent.GetPhysicsData();
            this.originalPhysicsData = _physicsComponent.GetOriginalPhysicsData();

            float value;
            _originalValue = GetCurrentValue();
            if (SetToOriginalValue)
                value = GetOriginalValue();
            else if (ByPercent)
                value = _originalValue * (Value.Value / 100f);
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
                    this.physicsData.MaxSpeed = value;
                    break;
                case FloatVariableEnum.MaxForce:
                    this.physicsData.MaxForce = value;
                    break;
                case FloatVariableEnum.ForceMultiplier:
                    this.physicsData.ForceMultiplier = value;
                    break;
                case FloatVariableEnum.DragForce:
                    this.physicsData.DragForce = value;
                    break;
            }
        }

        public float GetCurrentValue()
        {
            switch (FloatVariable)
            {
                case FloatVariableEnum.MaxSpeed:
                    return this.physicsData.MaxSpeed;
                case FloatVariableEnum.MaxForce:
                    return this.physicsData.MaxForce;
                case FloatVariableEnum.ForceMultiplier:
                    return this.physicsData.ForceMultiplier;
                case FloatVariableEnum.DragForce:
                    return this.physicsData.DragForce;
            }
            return 0f;
        }

        private float GetOriginalValue()
        {
            switch (FloatVariable)
            {
                case FloatVariableEnum.MaxSpeed:
                    return this.originalPhysicsData.MaxSpeed;
                case FloatVariableEnum.MaxForce:
                    return this.originalPhysicsData.MaxForce;
                case FloatVariableEnum.ForceMultiplier:
                    return this.originalPhysicsData.ForceMultiplier;
                case FloatVariableEnum.DragForce:
                    return this.originalPhysicsData.DragForce;
            }
            return 0f;
        }
    }
}