using BlueOrb.Scripts.AI.AtomActions;
using BlueOrb.Scripts.AI.Playmaker;
using HutongGames.PlayMaker;
using BlueOrb.Common.Container;

namespace BlueOrb.Scripts.AI.PlayMaker.Attack
{
    [ActionCategory("RQ.Variable")]
    //[Tooltip("Returns Success if Button is pressed.")]
    public class SetVector3Variable : BasePlayMakerAction
    {
        [RequiredField]
        public FsmOwnerDefault gameObject;

        //[UIHint(UIHint.Variable)]
        [HutongGames.PlayMaker.Tooltip("Variable to set.")]
        public FsmVector3 Value;

        public SetVector3VariableAtom _atom;

        public override void Reset()
        {
            base.Reset();
            gameObject = null;
        }

        public override void OnEnter()
        {
            var go = Fsm.GetOwnerDefaultTarget(gameObject);
            if (go == null)
            {
                return;
            }
            var entity = go.GetComponent<IEntity>();
            //var entity = Owner.GetComponent<IEntity>();
            if (!Value.IsNone)
            {
                _atom.SetVariable(Value.Value);
            }
            _atom.Start(entity);
            Finish();
        }

        public override void OnExit()
        {
            base.OnExit();
            _atom.End();
        }
    }
}
