//using BlueOrb.Scripts.AI.AtomActions;
//using BlueOrb.Scripts.AI.AtomActions.Player;
//using HutongGames.PlayMaker;
//using BlueOrb.Common.Container;

//namespace BlueOrb.Scripts.AI.PlayMaker.Attack
//{
//    [ActionCategory("RQ.Shooter")]
//    //[Tooltip("Returns Success if Button is pressed.")]
//    public class ShooterShoot : FsmStateAction
//    {
//        public ShooterShootAtom _atom;

//        //[HutongGames.PlayMaker.Tooltip("Event to send if the trigger event is detected.")]
//        //public FsmEvent HangEvent;

//        public override void OnEnter()
//        {
//            var entity = Owner.GetComponent<IEntity>();
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
