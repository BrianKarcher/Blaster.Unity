//using HutongGames.PlayMaker;
//using BlueOrb.Common.Container;
//using UnityEngine;
//using BlueOrb.Messaging;
//using BlueOrb.Scripts.UI;

//namespace BlueOrb.Scripts.AI.Playmaker
//{
//    [ActionCategory("BlueOrb.UI")]
//    [HutongGames.PlayMaker.Tooltip("Enable a Canvas.")]
//    public class EnableCanvas : BasePlayMakerAction
//    {
//        [RequiredField]
//        [CheckForComponent(typeof(Rigidbody))]
//        public FsmOwnerDefault gameObject;

//        [HutongGames.PlayMaker.Tooltip("The canvas to enable")]
//        public FsmGameObject Canvas;

//        public bool Enable;
//        public bool RevertOnExit;

//        private IEntity entity;

//        public override void Reset()
//        {
//            gameObject = null;
//            Canvas = null;
//        }

//        public override void OnEnter()
//        {
//            var go = Fsm.GetOwnerDefaultTarget(gameObject);
//            if (go == null)
//            {
//                return;
//            }
//            this.entity = base.GetEntityBase(go);
//            string eventName = Enable ? UIController.EnableCanvasEvent : UIController.DisableCanvasEvent;
//            MessageDispatcher.Instance.DispatchMsg(eventName, 0f, null, UIController.UIControllerId, Canvas.Name);
//        }

//        public override void OnExit()
//        {
//            base.OnExit();
//            if (RevertOnExit)
//            {
//                string eventName = Enable ? UIController.DisableCanvasEvent : UIController.EnableCanvasEvent;
//                MessageDispatcher.Instance.DispatchMsg(eventName, 0f, null, UIController.UIControllerId, Canvas.Name);
//            }
//        }
//    }
//}