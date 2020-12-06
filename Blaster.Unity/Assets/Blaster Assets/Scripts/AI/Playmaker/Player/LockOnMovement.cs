//using BlueOrb.Scripts.AI.AtomActions;
//using HutongGames.PlayMaker;
//using BlueOrb.Common.Container;

//namespace BlueOrb.Scripts.AI.PlayMaker.Attack
//{
//    [ActionCategory("RQ.Player")]
//    //[Tooltip("Returns Success if Button is pressed.")]
//    public class LockOnMovement : FsmStateAction
//    {
//        [RequiredField]
//        public FsmOwnerDefault gameObject;
//        public LockedOnMovementAtom _atom;

//        //[HutongGames.PlayMaker.Tooltip("Event to send if the trigger event is detected.")]
//        //public FsmEvent HangEvent;

//        public override void Reset()
//        {
//            gameObject = null;
//        }

//        public override void OnPreprocess()
//        {
//            base.OnPreprocess();
//            Fsm.HandleLateUpdate = true;
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

//        public override void OnLateUpdate()
//        {
//            base.OnLateUpdate();
//            _atom.LateUpdate();
//        }

//        private void Tick()
//        {
//            _atom.OnUpdate();
//            //if (_atom.IsFinished)
//            //    Finish();
//        }
//    }
//}
