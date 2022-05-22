using BlueOrb.Scripts.AI.Playmaker;
using HutongGames.PlayMaker;
using BlueOrb.Common.Container;
using UnityEngine;

namespace BlueOrb.Scripts.AI.PlayMaker.Attack
{
    [ActionCategory("BlueOrb.Variable")]
    public class SetGameObjectVariable : BasePlayMakerAction
    {
        public GameObjectVariableEnum GameObjectVariable;
        public FsmGameObject Value;
        public bool SetToOriginalValue;
        public bool RevertOnExit;

        private GameObject _originalValue;

        private IEntity _entity;

        public enum GameObjectVariableEnum
        {
            Target = 0
        }

        [RequiredField]
        public FsmOwnerDefault gameObject;

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

            GameObject value;
            _originalValue = GetCurrentValue();
            if (SetToOriginalValue)
                value = GetOriginalValue();
            else
                value = Value.Value;

            SetValue(value);
            Finish();
        }

        public void SetValue(GameObject value)
        {
            switch (GameObjectVariable)
            {
                case GameObjectVariableEnum.Target:

                    _entity.Target = value;
                    break;
            }
        }

        public override void OnExit()
        {
            base.OnExit();
            if (RevertOnExit)
                SetValue(_originalValue);
        }

        public GameObject GetCurrentValue()
        {
            switch (GameObjectVariable)
            {
                case GameObjectVariableEnum.Target:
                    return _entity.Target;
            }
            return null;
        }

        private GameObject GetOriginalValue()
        {
            return null;
        }
    }
}
