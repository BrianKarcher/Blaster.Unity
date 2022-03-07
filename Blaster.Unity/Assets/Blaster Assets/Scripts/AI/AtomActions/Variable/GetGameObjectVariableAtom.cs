using BlueOrb.Common.Container;
using UnityEngine;

namespace BlueOrb.Scripts.AI.AtomActions
{
    public class GetGameObjectVariableAtom : AtomActionBase
    {
        public enum GameObjectVariableEnum
        {
            Player = 0,
            ParriedEntity = 1,
            Target = 2
        }

        public GameObjectVariableEnum Variable;
        private GameObject value;

        public override void Start(IEntity entity)
        {
            base.Start(entity);
        }

        public override void OnUpdate()
        {
            this.value = GetValue();
            if (this.value == null)
                throw new System.Exception("(GetGameObject) Value was (null) for " + Variable.ToString() + " for " + _entity.name);
        }

        public GameObject GetValue()
        {
            switch (Variable)
            {
                case GameObjectVariableEnum.Player:
                    return EntityContainer.Instance.GetMainCharacter().gameObject;
                case GameObjectVariableEnum.Target:
                    if (_entity.Target == null)
                    {
                        return EntityContainer.Instance.GetMainCharacter().gameObject;
                    }
                    return _entity.Target;
            }
            throw new System.Exception("Enum " + Variable.ToString() + " not found");
        }

        public GameObject GetVariable()
        {
            return this.value;
        }
    }
}
