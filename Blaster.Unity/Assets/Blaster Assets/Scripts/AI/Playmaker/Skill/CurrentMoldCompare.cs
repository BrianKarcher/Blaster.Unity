//using BlueOrb.Scripts.AI.AtomActions;
//using HutongGames.PlayMaker;
//using BlueOrb.Common.Container;

//namespace BlueOrb.Scripts.AI.PlayMaker.Attack
//{
//    [ActionCategory("RQ.Skill")]
//    public class CurrentMoldCompare : FsmStateAction
//    {
//        public CurrentMoldCompareAtom _atom;

//        [HutongGames.PlayMaker.Tooltip("Mold Configs match.")]
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
