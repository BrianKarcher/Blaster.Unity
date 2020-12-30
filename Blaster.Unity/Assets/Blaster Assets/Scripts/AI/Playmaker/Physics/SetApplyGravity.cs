using BlueOrb.Scripts.AI.AtomActions;
using HutongGames.PlayMaker;
using BlueOrb.Common.Container;

namespace BlueOrb.Scripts.AI.Playmaker
{
    [ActionCategory("RQ.Physics")]
    [Tooltip("Set Apply Gravity.")]
    public class SetApplyGravity : FsmStateAction
    {
        [RequiredField]
        [Tooltip("The main GameObject.")]
        public FsmOwnerDefault gameObject;

        public SetApplyGravityAtom _atom;

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
            //var rqSM = Owner.GetComponent<PlayMakerStateMachineComponent>();
            //_entity = rqSM.GetComponentRepository();
            var entity = go.GetComponent<IEntity>();
            _atom.Start(entity);            
        }

        public override void OnExit()
        {
            base.OnExit();
            _atom.End();
        }
    }
}
