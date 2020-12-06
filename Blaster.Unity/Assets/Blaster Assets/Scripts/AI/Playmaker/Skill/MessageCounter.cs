//using BlueOrb.Scripts.AI.AtomActions;
//using HutongGames.PlayMaker;
//using BlueOrb.Common.Container;
//using UnityEngine;

//namespace BlueOrb.Scripts.AI.PlayMaker.Attack
//{
//    [ActionCategory("RQ")]
//    public class MessageCounter : FsmStateAction
//    {
//        public MessageCounterAtom _atom;

//        //[HutongGames.PlayMaker.Tooltip("Mold Configs match.")]
//        public FsmEvent Finished;

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
//            base.OnUpdate();
//            if (_atom.IsFinished)
//            {
//                Finish();
//                Fsm.Event(Finished);
//            }
//        }
//    }
//}
