using BlueOrb.Scripts.AI.AtomActions.Animation;
using HutongGames.PlayMaker;
using BlueOrb.Common.Container;
using UnityEngine;
using Cinemachine;
using BlueOrb.Messaging;
using static BlueOrb.Controller.DollyCartComponent;
using System;

namespace BlueOrb.Scripts.AI.Playmaker.Cinemachine
{
    [ActionCategory("BlueOrb.Cinemachine")]
    [HutongGames.PlayMaker.Tooltip("Set speed of dolly cart.")]
    public class SetDollySpeed : FsmStateAction
    {
        [RequiredField]
        public FsmOwnerDefault gameObject;

        public FsmBool Immediate;
        public FsmFloat Speed;
        public FsmFloat SmoothTime;

        //private CinemachineDollyCart _dollyCart;

        public override void Reset()
        {
            gameObject = null;
            Speed = 5;
            SmoothTime = 2;
        }

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
            SetSpeedData data = new SetSpeedData();
            data.TargetSpeed = Speed.Value;
            data.SmoothTime = SmoothTime.Value;
            if (entity == null)
                throw new Exception($"Could not locate entity in {Fsm.ActiveStateName}");
            if (Immediate.Value)
            {
                MessageDispatcher.Instance.DispatchMsg("SetSpeed", 0f, string.Empty, entity.GetId(), Speed.Value);
            }
            else
            {
                MessageDispatcher.Instance.DispatchMsg("SetSpeedTarget", 0f, string.Empty, entity.GetId(), data);
            }
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
