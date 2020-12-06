//using BlueOrb.Scripts.AI.AtomActions;
//using BlueOrb.Scripts.AI.AtomActions.Player;
//using HutongGames.PlayMaker;
//using BlueOrb.Common.Container;

//namespace BlueOrb.Scripts.AI.PlayMaker.Attack
//{
//    [ActionCategory("RQ.Player")]
//    //[Tooltip("Returns Success if Button is pressed.")]
//    public class ThirdPersonMovement : FsmStateAction
//    {
//        [RequiredField]
//        public FsmOwnerDefault gameObject;

//        public ThirdPersonMovementAtom _atom;

//        //[HutongGames.PlayMaker.Tooltip("Event to send if the trigger event is detected.")]
//        //public FsmEvent HangEvent;

//        public override void Reset()
//        {
//            gameObject = null;
//        }

//        public override void OnEnter()
//        {
//            var go = Fsm.GetOwnerDefaultTarget(gameObject);
//            if (go == null)
//            {
//                return;
//            }
//            var entity = go.GetComponent<IEntity>();
//            _atom.Start(entity);
//        }

//        public override void OnExit()
//        {
//            base.OnExit();
//            _atom.End();
//        }

//        public override void OnUpdate()
//        {
//            Tick();
//        }

//        private void Tick()
//        {
//            _atom.OnUpdate();
//            if (_atom.IsFinished)
//                Finish();
//        }
//    }
//}
