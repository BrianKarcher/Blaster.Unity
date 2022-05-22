using BlueOrb.Scripts.AI.AtomActions;
using HutongGames.PlayMaker;
using BlueOrb.Common.Container;

namespace BlueOrb.Scripts.AI.Playmaker
{
    [ActionCategory("RQ.Physics")]
    [Tooltip("Can see the entity?")]
    public class CanSee : BasePlayMakerAction
    {
        [RequiredField]
        public FsmOwnerDefault gameObject;

        public CanSeeAtom _atom;

        [UIHint(UIHint.Layer)]
        [Tooltip("Layers to avoid.")]
        public FsmInt[] ObstacleLayer;

        [UIHint(UIHint.Variable)]
        [Tooltip("Fire when target is in FOV.")]
        public FsmEvent CanSeeEvent;

        [UIHint(UIHint.Variable)]
        [Tooltip("Fire when target is not in FOV.")]
        public FsmEvent CanNotSeeEvent;

        [UIHint(UIHint.Variable)]
        [Tooltip("The bool value to store")]
        public FsmBool storeResult;

        public bool everyFrame;

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
            //var rqSM = Owner.GetComponent<PlayMakerStateMachineComponent>();
            //_entity = rqSM.GetComponentRepository();
            //var entity = Owner.GetComponent<IEntity>();
            _atom.SetObstacleLayerMask(ActionHelpers.LayerArrayToLayerMask(ObstacleLayer, false));
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
            bool isTrue = false;
            if (_atom.Check())
            {
                isTrue = true;
                if (CanSeeEvent != null)
                    Fsm.Event(CanSeeEvent);
                //Finish();
            }
            else
            {
                isTrue = false;
                Fsm.Event(CanNotSeeEvent);
            }
            
            if (!storeResult.IsNone)
            {
                storeResult.Value = isTrue;
            }
        }

        public override void OnExit()
        {
            base.OnExit();
            _atom.End();
        }
    }
}
