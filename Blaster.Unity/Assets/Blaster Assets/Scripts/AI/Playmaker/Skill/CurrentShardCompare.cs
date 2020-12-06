//using BlueOrb.Scripts.AI.AtomActions;
//using HutongGames.PlayMaker;
//using BlueOrb.Common.Container;

//namespace BlueOrb.Scripts.AI.PlayMaker.Attack
//{
//    [ActionCategory("RQ.Skill")]
//    public class CurrentShardCompare : FsmStateAction
//    {
//        public CurrentShardCompareAtom _atom;

//        [HutongGames.PlayMaker.Tooltip("Shard Configs match.")]
//        public FsmEvent IsEqualEvent;

//        public override void OnEnter()
//        {
//            var entity = Owner.GetComponent<IEntity>();
//            _atom.Start(entity);
//            if (_atom.IsEqual())
//            {
//                Fsm.Event(IsEqualEvent);
//            }
//            Finish();
//        }

//        public override void OnExit()
//        {
//            base.OnExit();
//            _atom.End();
//        }
//    }
//}
