using BlueOrb.Scripts.AI.AtomActions.Physics;
using HutongGames.PlayMaker;
using BlueOrb.Common.Container;

namespace BlueOrb.Scripts.AI.Playmaker.Physics
{
    [ActionCategory("RQ.Physics")]
    [Tooltip("Look at the player input vector direction.")]
    public class LookAtInputVector : FsmStateAction
    {
        public LookAtInputVectorAtom _atom;

        public override void OnEnter()
        {
            var entity = Owner.GetComponent<IEntity>();
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
