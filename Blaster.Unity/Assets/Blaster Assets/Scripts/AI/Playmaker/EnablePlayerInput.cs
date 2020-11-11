using BlueOrb.Scripts.AI.AtomActions;
using HutongGames.PlayMaker;
using BlueOrb.Common.Container;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueOrb.Scripts.AI.Playmaker
{
    [ActionCategory("RQ.Player")]
    [Tooltip("Enable player input.")]
    public class EnablePlayerInput : FsmStateAction
    {
        [RequiredField]
        public FsmOwnerDefault gameObject;
        public EnablePlayerInputAtom _atom;

        public override void Reset()
        {
            gameObject = null;
        }

        public override void OnEnter()
        {
            var go = Fsm.GetOwnerDefaultTarget(gameObject);
            if (go == null)
            {
                return;
            }
            var entity = go.GetComponent<IEntity>();
            if (entity == null)
                throw new Exception($"Entity not found in {Fsm.ActiveStateName}");
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
