//using RQ.Common.Components;
//using RQ.Common.Container;

//namespace RQ_Assets.Scripts.AI.AtomActions.Components
//{
//    public class RandomWaitWithActionChanceAtom : AtomActionBase
//    {
//        public bool EnableOnEnter;
//        public bool EnableOnExit;
//        public string ComponentName;
//        private IComponentBase _component;

//        public class ActionsAndWaits
//        {
//            public string 
//        }

//        public override void Start(IEntity entity)
//        {
//            base.Start(entity);
//            if (_component == null)
//                _component = entity.Components.GetComponent(ComponentName);
//            _component?.gameObject.SetActive(EnableOnEnter);
//        }

//        public override void End()
//        {
//            _component?.gameObject.SetActive(EnableOnExit);
//        }
//    }
//}
