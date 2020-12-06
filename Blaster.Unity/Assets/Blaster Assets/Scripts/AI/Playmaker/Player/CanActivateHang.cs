//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using BlueOrb.Scripts.AI.AtomActions;
//using BlueOrb.Scripts.AI.AtomActions.Attack;
//using HutongGames.PlayMaker;
//using BlueOrb.Common.Container;
//using UnityEngine;

//namespace BlueOrb.Scripts.AI.PlayMaker.Attack
//{
//    [ActionCategory("RQ.Player")]
//    //[Tooltip("Returns Success if Button is pressed.")]
//    public class CanActivateHang : FsmStateAction
//    {
//        [RequiredField]
//        public FsmOwnerDefault gameObject;

//        public CanActivateHangAtom _atom;
//        [UIHint(UIHint.TagMenu)]
//        [HutongGames.PlayMaker.Tooltip("Filter by Tag.")]
//        public FsmString CollideTag;

//        [UIHint(UIHint.Variable)]
//        [HutongGames.PlayMaker.Tooltip("Store the GameObject that collided with the Owner of this FSM.")]
//        public FsmGameObject storeCollider;

//        [HutongGames.PlayMaker.Tooltip("Event to send if the trigger event is detected.")]
//        public FsmEvent HangEvent;

//        // cached proxy component for callbacks
//        private PlayMakerProxyBase cachedProxy;

//        //public override void OnPreprocess()
//        //{
//        //    //Fsm.HandleTriggerEnter = true;
//        //}

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

//        //public override void DoTriggerEnter(Collider other)
//        //{
//        //    Debug.Log($"Trigger {other.name} entered in CanActivateHang, tag compare {CollideTag} = {other.tag}?");
//        //    TriggerEnter(other);
//        //}

//        //private void TriggerEnter(Collider other)
//        //{
//        //    //Debug.Log($"(CanActivateHang) OnTriggerEnter called for {Fsm.GameObject.name} by {other.name}");
//        //    //if (TagMatches(CollideTag, other))
//        //    //{
//        //    //    Debug.Log("Tag match");
//        //    //    StoreCollisionInfo(other);
//        //    //    Fsm.Event(HangEvent);
//        //    //}
//        //}

//        private void StoreCollisionInfo(Collider collisionInfo)
//        {
//            storeCollider.Value = collisionInfo.gameObject;
//        }

//        public override void OnUpdate()
//        {
//            Tick();
//        }

//        private void Tick()
//        {
//            _atom.OnUpdate();
//            if (_atom.CanHang)
//            {
//                Fsm.Event(HangEvent);
//            }
//            if (_atom.IsFinished)
//            {
//                Fsm.Event(HangEvent);
//                //Finish();
//            }
//        }

//        //public override void OnEnter()
//        //{
//        //    if (gameObject.OwnerOption == OwnerDefaultOption.UseOwner)
//        //        return;

//        //    if (cachedProxy == null)
//        //        GetProxyComponent();

//        //    AddCallback();

//        //    gameObject.GameObject.OnChange += UpdateCallback;
//        //}

//        //public override void OnExit()
//        //{
//        //    if (gameObject.OwnerOption == OwnerDefaultOption.UseOwner)
//        //        return;

//        //    RemoveCallback();

//        //    gameObject.GameObject.OnChange -= UpdateCallback;
//        //}

//        //private void UpdateCallback()
//        //{
//        //    RemoveCallback();
//        //    GetProxyComponent();
//        //    AddCallback();
//        //}

//        //private void GetProxyComponent()
//        //{
//        //    cachedProxy = null;
//        //    var source = gameObject.GameObject.Value;
//        //    if (source == null)
//        //        return;

//        //    switch (trigger)
//        //    {
//        //        case TriggerType.OnTriggerEnter:
//        //            cachedProxy = PlayMakerFSM.GetEventHandlerComponent<PlayMakerTriggerEnter>(source);
//        //            break;
//        //        case TriggerType.OnTriggerStay:
//        //            cachedProxy = PlayMakerFSM.GetEventHandlerComponent<PlayMakerTriggerStay>(source);
//        //            break;
//        //        case TriggerType.OnTriggerExit:
//        //            cachedProxy = PlayMakerFSM.GetEventHandlerComponent<PlayMakerTriggerExit>(source);
//        //            break;
//        //    }
//        //}

//        //private void AddCallback()
//        //{
//        //    if (cachedProxy == null)
//        //        return;

//        //    switch (trigger)
//        //    {
//        //        case TriggerType.OnTriggerEnter:
//        //            cachedProxy.AddTriggerEventCallback(TriggerEnter);
//        //            break;
//        //        case TriggerType.OnTriggerStay:
//        //            cachedProxy.AddTriggerEventCallback(TriggerStay);
//        //            break;
//        //        case TriggerType.OnTriggerExit:
//        //            cachedProxy.AddTriggerEventCallback(TriggerExit);
//        //            break;
//        //    }
//        //}

//        //private void RemoveCallback()
//        //{
//        //    if (cachedProxy == null)
//        //        return;

//        //    switch (trigger)
//        //    {
//        //        case TriggerType.OnTriggerEnter:
//        //            cachedProxy.RemoveTriggerEventCallback(TriggerEnter);
//        //            break;
//        //        case TriggerType.OnTriggerStay:
//        //            cachedProxy.RemoveTriggerEventCallback(TriggerStay);
//        //            break;
//        //        case TriggerType.OnTriggerExit:
//        //            cachedProxy.RemoveTriggerEventCallback(TriggerExit);
//        //            break;
//        //    }
//        //}
//    }
//}
