using BlueOrb.Scripts.AI.AtomActions;
using HutongGames.PlayMaker;
using BlueOrb.Common.Container;

namespace BlueOrb.Scripts.AI.Playmaker
{
    [ActionCategory("RQ.Physics")]
    [Tooltip("Get Is Grounded.")]
    public class GetIsGrounded : BasePlayMakerAction
    {
        [RequiredField]
        public FsmOwnerDefault gameObject;

        [UIHint(UIHint.Variable)]
        public FsmBool IsGrounded;
        public FsmEvent Grounded;
        public FsmEvent NotGrounded;
        public bool everyFrame;

        public GetIsGroundedAtom _atom;

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
            _atom.Start(entity);
            Tick();
            if (!everyFrame)
                Finish();
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
        }

        private void Tick()
        {
            _atom.OnUpdate();
            var grounded = _atom.GetIsGrounded();
            if (!IsGrounded.IsNone)
                IsGrounded.Value = grounded;
            if (grounded)
                Fsm.Event(Grounded);
            if (!grounded)
                Fsm.Event(NotGrounded);
        }
    }
}
