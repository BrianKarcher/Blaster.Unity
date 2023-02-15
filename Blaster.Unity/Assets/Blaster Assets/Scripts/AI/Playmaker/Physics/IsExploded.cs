using BlueOrb.Scripts.AI.AtomActions;
using HutongGames.PlayMaker;

namespace BlueOrb.Scripts.AI.Playmaker
{
    [ActionCategory("RQ.Physics")]
    [Tooltip("Did we get exploded?")]
    public class IsExploded : BasePlayMakerAction
    {
        [RequiredField]
        public FsmOwnerDefault gameObject;

        [UIHint(UIHint.Variable)]
        [Tooltip("Fire when exploded.")]
        public FsmEvent Exploded;

        [UIHint(UIHint.Variable)]
        [Tooltip("Position where the explosion occurred.")]
        public FsmVector3 Position;

        public IsExplodedAtom _atom;

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
        }

        public override void OnUpdate()
        {
            base.OnUpdate();
            _atom.OnUpdate();
            if (_atom.IsFinished)
            {
                Fsm.Event(Exploded);
                Finish();
            }
        }

        public override void OnExit()
        {
            base.OnExit();
            _atom.End();
        }
    }
}