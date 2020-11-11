using BlueOrb.Scripts.AI.AtomActions;
using HutongGames.PlayMaker;
using BlueOrb.Common.Container;

namespace BlueOrb.Scripts.AI.Playmaker
{
    [ActionCategory("RQ.Physics")]
    [Tooltip("Cast a ray")]
    public class Raycast : FsmStateAction
    {
        public RaycastAtom _atom;

        [UIHint(UIHint.Layer)]
        [Tooltip("Layers to check.")]
        public FsmInt[] Layer;

        [UIHint(UIHint.Variable)]
        [Tooltip("Fire when raycast hits.")]
        public FsmEvent HitEvent;

        [UIHint(UIHint.Variable)]
        [Tooltip("Fire when raycast does not hit.")]
        public FsmEvent NotHitEvent;

        [UIHint(UIHint.Variable)]
        [Tooltip("The bool value to store")]
        public FsmBool storeResult;

        public bool everyFrame;

        public override void OnEnter()
        {
            //var rqSM = Owner.GetComponent<PlayMakerStateMachineComponent>();
            //_entity = rqSM.GetComponentRepository();
            var entity = Owner.GetComponent<IEntity>();
            _atom.SetLayerMask(ActionHelpers.LayerArrayToLayerMask(Layer, false));
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
                if (HitEvent != null)
                    Fsm.Event(HitEvent);
                //Finish();
            }
            else
            {
                isTrue = false;
                Fsm.Event(NotHitEvent);
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
