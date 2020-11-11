using Rewired;
using BlueOrb.Common.Container;

namespace BlueOrb.Scripts.AI.AtomActions
{
    public class BufferButtonInputAtom : AtomActionBase
    {
        public string _action;

        private Rewired.Player _player;
        private bool _bufferState;

        public override void Start(IEntity entity)
        {
            base.Start(entity);

            _player = ReInput.players.GetPlayer(0);
            _bufferState = false;
        }

        public override void OnUpdate()
        {
            base.OnUpdate();
            if (_bufferState)
                return;

            var buttonDown = _player.GetButtonDown(_action);
            if (buttonDown)
                _bufferState = true;
        }

        public override void End()
        {

        }

        public bool GetBufferState()
        {
            return _bufferState;
        }
    }
}
