using BlueOrb.Scripts.AI.AtomActions.Animation;
using HutongGames.PlayMaker;
using BlueOrb.Common.Container;
using UnityEngine;
using Cinemachine;
using BlueOrb.Messaging;
using static BlueOrb.Controller.DollyCartComponent;
using BlueOrb.Controller;

namespace BlueOrb.Scripts.AI.Playmaker.Cinemachine
{
    [ActionCategory("BlueOrb.Cinemachine")]
    [HutongGames.PlayMaker.Tooltip("Set speed of dolly cart.")]
    public class SetDollyCart : FsmStateAction
    {
        [RequiredField]
        public FsmOwnerDefault gameObject;

        public FsmGameObject DollyCart;
        //private CinemachineDollyCart _dollyCart;

        public override void Reset()
        {
            gameObject = null;
        }

        //public override void Init(FsmState state)
        //{
        //    base.Init(state);
        //    Speed = 5;
        //    SmoothTime = 2;
        //}

        //public AddLayerWeightLerpAtom _atom;

        public override void OnEnter()
        {
            //Debug.Log("(AddLayerWeightLerp) OnEnter called");
            var go = Fsm.GetOwnerDefaultTarget(gameObject);
            if (go == null)
            {
                return;
            }
            //Debug.Log("(CenterCamera) OnEnter called");
            //var entity = go.GetComponent<IEntity>();
            //if (_dollyCart == null)
            //{
            //    _dollyCart = go.GetComponent<CinemachineDollyCart>();
            //}
            var entity = go.GetComponent<IEntity>();
            var dollyComponent = entity.Components.GetComponent<DollyCartComponent>();
            dollyComponent.SetDollyCart(DollyCart.Value);
            //MessageDispatcher.Instance.DispatchMsg("SetSpeedTarget", 0f, string.Empty, entity.GetId(), data);
            //_dollyCart.m_Speed = Speed.Value;
            //_atom.Start(entity);
            Finish();
        }

        public override void OnExit()
        {
            //Debug.Log("(AddLayerWeightLerp) OnExit called");
            base.OnExit();
            //_atom.End();
        }
    }
}
