//using BlueOrb.Scripts.AI.AtomActions;
//using HutongGames.PlayMaker;
//using BlueOrb.Common.Container;

//namespace BlueOrb.Scripts.AI.Playmaker
//{
//    [ActionCategory("RQ.Physics")]
//    [Tooltip("Get Is Grounded.")]
//    public class GetIsAirborne : FsmStateAction
//    {
//        [UIHint(UIHint.Variable)]
//        public FsmBool IsAirborne;
//        public FsmEvent Airborne;
//        public FsmEvent Grounded;
//        public bool everyFrame;

//        public GetIsAirborneAtom _atom;

//        public override void OnEnter()
//        {
//            //var rqSM = Owner.GetComponent<PlayMakerStateMachineComponent>();
//            //_entity = rqSM.GetComponentRepository();
//            var entity = Owner.GetComponent<IEntity>();
//            _atom.Start(entity);
//            Tick();
//            if (!everyFrame)
//                Finish();
//        }

//        public override void OnExit()
//        {
//            base.OnExit();
//            _atom.End();
//        }

//        public override void OnUpdate()
//        {
//            base.OnUpdate();
//            Tick();
//        }

//        private void Tick()
//        {
//            _atom.OnUpdate();
//            var airborne = _atom.GetIsAirborne();
//            if (!IsAirborne.IsNone)
//                IsAirborne.Value = !airborne;
//            if (!airborne)
//                Fsm.Event(Grounded);
//            if (airborne)
//                Fsm.Event(Airborne);
//        }
//    }
//}
