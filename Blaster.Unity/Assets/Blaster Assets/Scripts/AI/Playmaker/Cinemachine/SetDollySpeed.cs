using BlueOrb.Scripts.AI.AtomActions.Animation;
using HutongGames.PlayMaker;
using BlueOrb.Common.Container;
using UnityEngine;
using Cinemachine;

namespace BlueOrb.Scripts.AI.Playmaker.Cinemachine
{
    [ActionCategory("RQ.Cinemachine")]
    [HutongGames.PlayMaker.Tooltip("Set speed of dolly cart.")]
    public class SetDollySpeed : FsmStateAction
    {
        [RequiredField]
        public FsmOwnerDefault gameObject;

        public FsmFloat Speed;

        private CinemachineDollyCart _dollyCart;

        public override void Reset()
        {
            gameObject = null;
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
            if (_dollyCart == null)
            {
                _dollyCart = go.GetComponent<CinemachineDollyCart>();
            }
            _dollyCart.m_Speed = Speed.Value;
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
