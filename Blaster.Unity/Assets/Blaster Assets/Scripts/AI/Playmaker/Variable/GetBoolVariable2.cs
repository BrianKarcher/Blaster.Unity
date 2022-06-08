using BlueOrb.Scripts.AI.Playmaker;
using HutongGames.PlayMaker;
using UnityEngine;
using BlueOrb.Physics;
using BlueOrb.Controller;
using BlueOrb.Base.Manager;

namespace BlueOrb.Scripts.AI.PlayMaker.Attack
{
    [ActionCategory("BlueOrb.Variable")]
    public class GetBoolVariable2 : BasePlayMakerAction
    {
        [RequiredField]
        public FsmOwnerDefault gameObject;

        public BoolVariableEnum Variable;

        [UIHint(UIHint.Variable)]
        [HutongGames.PlayMaker.Tooltip("Fire when message is received.")]
        public FsmBool Value;

        [UIHint(UIHint.Variable)]
        [HutongGames.PlayMaker.Tooltip("Fire when variable is true.")]
        public FsmEvent IfTrue;

        [UIHint(UIHint.Variable)]
        [HutongGames.PlayMaker.Tooltip("Fire when variable is false.")]
        public FsmEvent IfFalse;

        public bool everyFrame = false;
        private IPhysicsComponent physicsComponent;
        private AnimationComponent animator;

        public enum BoolVariableEnum
        {
            Grounded = 0,
            CanParry = 1,
            ImmediateStartGame = 2
        }

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
            if (physicsComponent == null)
                physicsComponent = entity.Components.GetComponent<IPhysicsComponent>();
            if (animator == null)
                animator = entity.Components.GetComponent<AnimationComponent>();
            Tick();

            if (!everyFrame)
                Finish();
        }

        public override void OnUpdate()
        {
            Tick();
        }

        public void Tick()
        {
            base.OnUpdate();
            bool value = GetValue();
            if (!Value.IsNone)
            {
                Value.Value = value;
            }
            if (value)
            {
                Debug.LogWarning($"Running True bool event in {Fsm.ActiveStateName}");
                Fsm.Event(IfTrue);
            }
            if (!value)
            {
                Fsm.Event(IfFalse);
            }
        }

        public bool GetValue()
        {
            switch (Variable)
            {
                case BoolVariableEnum.Grounded:
                    return physicsComponent.Controller.GetIsGrounded();
                case BoolVariableEnum.ImmediateStartGame:
                    return GameStateController.Instance.GameSettingsConfig.ImmediateStartGame;
            }
            throw new System.Exception("Enum " + Variable.ToString() + " not found");
        }
    }
}