using BlueOrb.Scripts.AI.AtomActions;
using HutongGames.PlayMaker;
using BlueOrb.Common.Container;

namespace BlueOrb.Scripts.AI.Playmaker
{
    [ActionCategory("RQ.Physics")]
    [Tooltip("Set Apply Gravity.")]
    public class SetApplyGravity : FsmStateAction
    {
        public SetApplyGravityAtom _atom;

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
    }
}
