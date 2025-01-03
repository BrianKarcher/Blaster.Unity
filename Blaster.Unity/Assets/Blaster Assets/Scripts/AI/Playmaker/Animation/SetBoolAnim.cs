﻿using BlueOrb.Scripts.AI.AtomActions.Animation;
using HutongGames.PlayMaker;
using BlueOrb.Common.Container;

namespace BlueOrb.Scripts.AI.Playmaker.Camera
{
    [ActionCategory("RQ.Animation")]
    [HutongGames.PlayMaker.Tooltip("Set boolean anim parameter.")]
    public class SetBoolAnim : BasePlayMakerAction
    {
        [RequiredField]
        public FsmOwnerDefault gameObject;

        public SetBoolAnimAtom _atom;

        public override void OnEnter()
        {
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
            base.OnExit();
            _atom.End();
        }
    }
}
