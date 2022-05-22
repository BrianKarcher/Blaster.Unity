using BlueOrb.Scripts.AI.AtomActions.Attack;
using HutongGames.PlayMaker;
using BlueOrb.Common.Container;

namespace BlueOrb.Scripts.AI.Playmaker.Attack
{
    [ActionCategory("RQ.Attack")]
    [Tooltip("Get Is Deflected.")]
    public class IsDeflected : BasePlayMakerAction
    {
        [RequiredField]
        public FsmOwnerDefault gameObject;

        [UIHint(UIHint.Variable)]
        public FsmVector3 DeflectPosition;
        [UIHint(UIHint.Variable)]
        public FsmVector3 DeflectNormal;
        public FsmEvent DeflectedEvent;
        public IsDeflectedAtom _atom;

        public override void Reset()
        {
            base.Reset();
            gameObject = null;
            DeflectPosition = null;
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

        public override void OnExit()
        {
            base.OnExit();
        }

        public override void OnUpdate()
        {
            base.OnUpdate();
            if (_atom.IsFinished)
            {
                DeflectPosition.Value = _atom.HitPos;
                DeflectNormal.Value = _atom.HitNormal;
                Fsm.Event(DeflectedEvent);
                Finish();
            }
        }
    }
}
