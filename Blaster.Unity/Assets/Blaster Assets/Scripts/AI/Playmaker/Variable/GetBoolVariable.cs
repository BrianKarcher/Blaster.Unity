using BlueOrb.Scripts.AI.AtomActions;
using BlueOrb.Scripts.AI.Playmaker;
using HutongGames.PlayMaker;
using BlueOrb.Common.Container;
using UnityEngine;

namespace BlueOrb.Scripts.AI.PlayMaker.Attack
{
    [ActionCategory("RQ.Variable")]
    //[Tooltip("Returns Success if Button is pressed.")]
    public class GetBoolVariable : BasePlayMakerAction
    {
        [RequiredField]
        public FsmOwnerDefault gameObject;

        [UIHint(UIHint.Variable)]
        [HutongGames.PlayMaker.Tooltip("Fire when message is received.")]
        public FsmBool Value;

        [UIHint(UIHint.Variable)]
        [HutongGames.PlayMaker.Tooltip("Fire when variable is true.")]
        public FsmEvent IfTrue;

        [UIHint(UIHint.Variable)]
        [HutongGames.PlayMaker.Tooltip("Fire when variable is false.")]
        public FsmEvent IfFalse;

        public GetBoolVariableAtom _atom;
        public bool everyFrame = false;

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

            var entity = base.GetEntityBase(go);
            //var entity = Owner.GetComponent<IEntity>();
            _atom.Start(entity);
            Tick();

            //Finish();
            if (!everyFrame)
                Finish();
        }

        public override void OnUpdate()
        {
            Tick();
        }

        public void Tick()
        {
            base.OnUpdate();
            _atom.OnUpdate();
            if (!Value.IsNone)
            {
                Value.Value = _atom.Value;
            }
            if (_atom.Value)
            {
                //if (IfTrue.)
                Debug.LogWarning($"Running True bool event in {Fsm.ActiveStateName}");
                Fsm.Event(IfTrue);
            }
            if (!_atom.Value)
            {
                Fsm.Event(IfFalse);
            }
        }

        public override void OnExit()
        {
            base.OnExit();
            _atom.End();
        }
    }
}
