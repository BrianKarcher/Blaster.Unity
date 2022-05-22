using BlueOrb.Scripts.AI.AtomActions.Animation;
using HutongGames.PlayMaker;
using BlueOrb.Common.Container;
using UnityEngine;
using Cinemachine;
using BlueOrb.Messaging;
using static BlueOrb.Controller.DollyCartComponent;
using System;
using BlueOrb.Controller;

namespace BlueOrb.Scripts.AI.Playmaker.Cinemachine
{
    [ActionCategory("BlueOrb.Cinemachine")]
    [HutongGames.PlayMaker.Tooltip("Get speed of dolly cart.")]
    public class GetDollySpeed : BasePlayMakerAction
    {
        [RequiredField]
        public FsmOwnerDefault gameObject;
        public FsmFloat Speed;
        public bool everyFrame;

        //private CinemachineDollyCart _dollyCart;

        //public AddLayerWeightLerpAtom _atom;
        private DollyCartComponent _dolly;

        public override void OnEnter()
        {
            var go = Fsm.GetOwnerDefaultTarget(gameObject);
            if (go == null)
            {
                return;
            }

            var entity = base.GetEntityBase(go);
            _dolly = entity.Components.GetComponent<DollyCartComponent>();
            Tick();
            if (!everyFrame)
                Finish();
        }

        public override void OnExit()
        {
            base.OnExit();
        }

        public override void OnUpdate()
        {
            base.OnUpdate();
            Tick();
        }

        private void Tick()
        {
            Speed.Value = _dolly.GetSpeed();
        }
    }
}
