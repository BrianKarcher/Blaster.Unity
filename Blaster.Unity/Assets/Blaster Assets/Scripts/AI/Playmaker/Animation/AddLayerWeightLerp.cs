﻿using BlueOrb.Scripts.AI.AtomActions.Animation;
using HutongGames.PlayMaker;
using BlueOrb.Common.Container;
using UnityEngine;

namespace BlueOrb.Scripts.AI.Playmaker.Camera
{
    [ActionCategory("RQ.Animation")]
    [HutongGames.PlayMaker.Tooltip("Add a layer weight lerp (gradual transition).")]
    public class AddLayerWeightLerp : BasePlayMakerAction
    {
        [RequiredField]
        public FsmOwnerDefault gameObject;

        public AddLayerWeightLerpAtom _atom;

        public override void OnEnter()
        {
            Debug.Log("(AddLayerWeightLerp) OnEnter called");
            var go = Fsm.GetOwnerDefaultTarget(gameObject);
            if (go == null)
            {
                return;
            }

            var entity = base.GetEntityBase(go);
            _atom.Start(entity);
            Finish();
        }

        public override void OnExit()
        {
            Debug.Log("(AddLayerWeightLerp) OnExit called");
            base.OnExit();
            _atom.End();
        }
    }
}
