using BlueOrb.Scripts.AI.AtomActions;
using HutongGames.PlayMaker;

namespace BlueOrb.Scripts.AI.Playmaker
{
    [ActionCategory("RQ.Physics")]
    [Tooltip("Big drop in front of entity?")]
    public class IsFacingBigDrop : BasePlayMakerAction
    {
        public IsFacingBigDropAtom _atom;

        [UIHint(UIHint.Variable)]
        [Tooltip("Fire when facing big drop.")]
        public FsmEvent FacingBigDropEvent;

        [UIHint(UIHint.Variable)]
        [Tooltip("Fire when not facing big drop.")]
        public FsmEvent NotFacingBigDropEvent;

        [UIHint(UIHint.Variable)]
        [Tooltip("The bool value to store")]
        public FsmBool storeResult;

        public bool everyFrame;

        public override void OnEnter()
        {
            var entity = base.GetRepo(Owner);
            _atom.Start(entity);
            Tick();
            if (!everyFrame)
                Finish();
        }

        public override void OnUpdate()
        {
            base.OnUpdate();
            Tick();
        }

        private void Tick()
        {
            _atom.OnUpdate();
            storeResult = _atom.IsFacingBigDrop;

            if (_atom.IsFacingBigDrop)
            {
                if (FacingBigDropEvent != null)
                    Fsm.Event(FacingBigDropEvent);
            }
            else
            {
                Fsm.Event(NotFacingBigDropEvent);
            }
        }

        public override void OnExit()
        {
            base.OnExit();
            _atom.End();
        }
    }
}
