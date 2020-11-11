using Rewired;
using BlueOrb.Common.Container;
using UnityEngine;

namespace BlueOrb.Scripts.AI.AtomActions
{
    public class PlayerControlAtom : AtomActionBase
    {
        public string _targetLockAction;
        public float _clickNextTargetTime = 1f;

        private LayerMask _targetLockLayers;
        private string[] _tags;

        private Controller.Player.PlayerController _playerController;
        private Rewired.Player _rewiredPlayer;


        public override void Start(IEntity entity)
        {
            base.Start(entity);
            if (_playerController == null)
                _playerController = entity.Components.GetComponent<Controller.Player.PlayerController>();
            if (_rewiredPlayer == null)
                _rewiredPlayer = ReInput.players.GetPlayer(0);
        }

        public override void OnUpdate()
        {
            base.OnUpdate();

        }
    }
}
