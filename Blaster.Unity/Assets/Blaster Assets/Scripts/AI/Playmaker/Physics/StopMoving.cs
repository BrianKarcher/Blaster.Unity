using BlueOrb.Scripts.AI.AtomActions;
using HutongGames.PlayMaker;
using BlueOrb.Common.Container;

namespace BlueOrb.Scripts.AI.Playmaker
{
    [ActionCategory("RQ.Physics")]
    [Tooltip("Stop moving.")]
    public class StopMoving : BasePlayMakerAction
    {
        [RequiredField]
        [Tooltip("The main GameObject.")]
        public FsmOwnerDefault gameObject;

        public StopMovingAtom _atom;
        public bool everyFrame;

        public override void Reset()
        {
            gameObject = null;
            everyFrame = false;
        }

        public override void OnEnter()
        {
            var go = Fsm.GetOwnerDefaultTarget(gameObject);
            if (go == null)
            {
                return;
            }

            var entity = base.GetEntityBase(go);

            //var rqSM = Owner.GetComponent<PlayMakerStateMachineComponent>();
            //_entity = rqSM.GetComponentRepository();
            //var entity = Owner.GetComponent<IEntity>();
            _atom.Start(entity);
            if (!everyFrame)
                Finish();
        }

        public override void OnUpdate()
        {
            base.OnUpdate();
            _atom.Tick();
        }

        public override void OnExit()
        {
            base.OnExit();
            _atom.End();
        }
    }
}
