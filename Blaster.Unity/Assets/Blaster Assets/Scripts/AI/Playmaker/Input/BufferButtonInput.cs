using BlueOrb.Scripts.AI.AtomActions;
using HutongGames.PlayMaker;
using BlueOrb.Common.Container;

namespace BlueOrb.Scripts.AI.Playmaker.Input
{
    [ActionCategory("RQ.Input")]
    [Tooltip("Buffer a button input.")]
    public class BufferButtonInput : FsmStateAction
    {
        [UIHint(UIHint.Variable)]
        [HutongGames.PlayMaker.Tooltip("Store the button buffer state.")]
        public FsmBool bufferState;

        public BufferButtonInputAtom _atom;

        public override void OnEnter()
        {
            var entity = Owner.GetComponent<IEntity>();
            _atom.Start(entity);
        }

        public override void OnExit()
        {
            base.OnExit();
            _atom.End();
        }

        public override void OnUpdate()
        {
            base.OnUpdate();
            Tick();
            if (_atom.IsFinished)
                Finish();
        }

        private void Tick()
        {
            _atom.OnUpdate();
            bufferState.Value = _atom.GetBufferState();
        }
    }
}
