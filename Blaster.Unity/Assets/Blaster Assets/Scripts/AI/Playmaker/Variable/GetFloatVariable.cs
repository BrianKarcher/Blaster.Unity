using BlueOrb.Scripts.AI.Playmaker;
using HutongGames.PlayMaker;
using UnityEngine;
using BlueOrb.Common.Container;
using BlueOrb.Base.Extensions;

namespace BlueOrb.Scripts.AI.PlayMaker.Attack
{
    [ActionCategory("BlueOrb.Variable")]
    public class GetFloatVariable : BasePlayMakerAction
    {
        [RequiredField]
        public FsmOwnerDefault gameObject;

        public FsmGameObject Target;

        public FloatVariableEnum Variable;

        [UIHint(UIHint.Variable)]
        [HutongGames.PlayMaker.Tooltip("Fire when message is received.")]
        public FsmFloat Value;

        public bool everyFrame = false;
        private IEntity entity;

        public enum FloatVariableEnum
        {
            _2DDistanceToTarget = 0
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

            entity = base.GetEntityBase(go);
            Tick();

            if (!everyFrame)
                Finish();
        }

        public override void OnUpdate()
        {
            base.OnUpdate();
            Tick();
        }

        public void Tick()
        {
            float value = GetValue();
            if (!Value.IsNone)
            {
                Value.Value = value;
            }
        }

        public float GetValue()
        {
            switch (Variable)
            {
                case FloatVariableEnum._2DDistanceToTarget:
                    return Vector2.Distance(this.entity.transform.position.xz(), this.Target.Value.transform.position.xz());
            }
            throw new System.Exception("Enum " + Variable.ToString() + " not found");
        }
    }
}