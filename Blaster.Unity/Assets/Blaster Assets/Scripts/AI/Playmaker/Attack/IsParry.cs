using BlueOrb.Scripts.AI.AtomActions.Attack;
using BlueOrb.Scripts.AI.Playmaker;
using HutongGames.PlayMaker;
using BlueOrb.Common.Container;
using UnityEngine;

namespace BlueOrb.Scripts.AI.PlayMaker.Attack
{
    [ActionCategory("RQ.Attack")]
    //[Tooltip("Returns Success if Button is pressed.")]
    public class IsParry : BasePlayMakerAction
    {
        [RequiredField]
        public FsmOwnerDefault gameObject;
        public AttackAtom _attackAtom;

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
            _attackAtom.Start(entity);
            if (_attackAtom.IsFinished)
                Finish();
        }

        public override void OnExit()
        {
            base.OnExit();
            _attackAtom.End();
        }

        public override void OnUpdate()
        {
            _attackAtom.OnUpdate();
            if (_attackAtom.IsFinished)
                Finish();
        }

        public override void DoTriggerStay(Collider other)
        {
            base.DoTriggerStay(other);
        }
    }
}
