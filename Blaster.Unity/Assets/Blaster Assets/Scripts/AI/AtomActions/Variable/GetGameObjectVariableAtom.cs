using BlueOrb.Common.Container;
using BlueOrb.Controller;
using BlueOrb.Controller.Player;
using BlueOrb.Physics;
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
        private GameObject _value;
        private PlayerParryComponent _playerParryComponent;

        public override void Start(IEntity entity)
        {
            base.Start(entity);
            if (_playerParryComponent == null)
                _playerParryComponent = entity.Components.GetComponent<PlayerParryComponent>();
        }

        public override void OnUpdate()
        {
            _value = GetValue();
            if (_value == null)
                throw new System.Exception("(GetGameObject) Value was (null) for " + Variable.ToString() + " for " + _entity.name);
        }

        public GameObject GetValue()
        {
            switch (Variable)
            {
                case GameObjectVariableEnum.Player:
                    return EntityContainer.Instance.GetMainCharacter().gameObject;
                case GameObjectVariableEnum.ParriedEntity:
                    return _playerParryComponent.CurrentOtherParryGameObject;
                case GameObjectVariableEnum.Target:
                    return _entity.Target;
            }
            throw new System.Exception("Enum " + Variable.ToString() + " not found");
        }

        public GameObject GetVariable()
        {
            return _value;
        }
    }
}
