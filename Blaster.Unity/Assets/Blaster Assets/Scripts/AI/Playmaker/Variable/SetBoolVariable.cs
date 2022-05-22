﻿using BlueOrb.Scripts.AI.AtomActions;
using BlueOrb.Scripts.AI.Playmaker;
using HutongGames.PlayMaker;
using BlueOrb.Common.Container;

namespace BlueOrb.Scripts.AI.PlayMaker.Attack
{
    [ActionCategory("RQ.Variable")]
    //[Tooltip("Returns Success if Button is pressed.")]
    public class SetBoolVariable : BasePlayMakerAction
    {
        [RequiredField]
        public FsmOwnerDefault gameObject;
        public SetBoolVariableAtom _atom;

        public override void Reset()
        {
            base.Reset();
            gameObject = null;
        }

        public override void OnEnter()
        {
            var go = Fsm.GetOwnerDefaultTarget(gameObject);
            if (go == null)
            {
                return;
            }

            var entity = base.GetEntityBase(go);
            //var entity = Owner.GetComponent<IEntity>();
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
