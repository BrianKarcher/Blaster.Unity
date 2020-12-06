//using BlueOrb.Scripts.AI.AtomActions;
//using HutongGames.PlayMaker;
//using BlueOrb.Common.Container;

//namespace BlueOrb.Scripts.AI.Playmaker
//{
//    [ActionCategory("RQ.Player")]
//    [Tooltip("Player is hanging.")]
//    public class PlayerHang : FsmStateAction
//    {
//        //[UIHint(UIHint.Variable)]
//        //[HutongGames.PlayMaker.Tooltip("Store the GameObject that collided with the Owner of this FSM.")]
//        //public FsmGameObject hangCollider;

//        public PlayerHangAtom _atom;

//        public override void OnEnter()
//        {
//            //var rqSM = Owner.GetComponent<PlayMakerStateMachineComponent>();
//            //_entity = rqSM.GetComponentRepository();
//            var entity = Owner.GetComponent<IEntity>();
//            //_atom.SetHangCollider(hangCollider.Value);
//            _atom.Start(entity);
//            Tick();
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
//            if (_atom.IsFinished)
//                Finish();
//        }

//        private void Tick()
//        {
//            _atom.OnUpdate();
//        }
//    }
//}
