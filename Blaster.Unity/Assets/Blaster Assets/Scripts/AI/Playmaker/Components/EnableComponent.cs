using BlueOrb.Scripts.AI.AtomActions;
using BlueOrb.Scripts.AI.AtomActions.Components;
using HutongGames.PlayMaker;
using BlueOrb.Common.Container;

namespace BlueOrb.Scripts.AI.Playmaker.Input
{
    [ActionCategory("RQ.Component")]
    [Tooltip("Enable Component")]
    public class EnableComponent : FsmStateAction
    {
        [RequiredField]
        public FsmOwnerDefault gameObject;

        public EnableComponentAtom _atom;

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
            var entity = go.GetComponent<IEntity>();
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
