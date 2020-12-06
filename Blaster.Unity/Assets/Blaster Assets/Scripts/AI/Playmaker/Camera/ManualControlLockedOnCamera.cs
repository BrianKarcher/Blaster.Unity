//using BlueOrb.Scripts.AI.AtomActions;
//using BlueOrb.Scripts.AI.AtomActions.Camera;
//using HutongGames.PlayMaker;
//using BlueOrb.Common.Container;

//namespace BlueOrb.Scripts.AI.Playmaker.Camera
//{
//    [ActionCategory("RQ.Camera")]
//    [Tooltip("ManualControlLockedOnCamera.")]
//    public class ManualControlLockedOnCamera : FsmStateAction
//    {
//        public ManualControlLockedOnCameraAtom _atom;

//        public override void OnEnter()
//        {
//            var entity = Owner.GetComponent<IEntity>();
//            _atom.Start(entity);
//        }

//        public override void OnPreprocess()
//        {
//            Fsm.HandleLateUpdate = true;
//        }

//        public override void OnLateUpdate()
//        {
//            _atom.OnLateUpdate();
//            if (_atom.IsFinished)
//                Finish();
//        }

//        public override void OnExit()
//        {
//            base.OnExit();
//            _atom.End();
//        }
//    }
//}
