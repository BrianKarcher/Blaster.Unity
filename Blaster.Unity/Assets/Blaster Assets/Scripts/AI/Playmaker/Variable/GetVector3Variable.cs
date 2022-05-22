using BlueOrb.Scripts.AI.AtomActions;
using BlueOrb.Scripts.AI.Playmaker;
using HutongGames.PlayMaker;
using BlueOrb.Common.Container;
using UnityEngine;

namespace BlueOrb.Scripts.AI.PlayMaker.Attack
{
    [ActionCategory("RQ.Variable")]
    //[Tooltip("Returns Success if Button is pressed.")]
    public class GetVector3Variable : BasePlayMakerAction
    {
        [RequiredField]
        public FsmOwnerDefault gameObject;

        //[UIHint(UIHint.Variable)]
        [HutongGames.PlayMaker.Tooltip("Value to store.")]
        public FsmVector3 StoreValue;

        public GetVector3VariableAtom _atom;
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
            if (!StoreValue.IsNone)
            {
                StoreValue.Value = _atom.Value;
            }
        }

        public override void OnExit()
        {
            base.OnExit();
            _atom.End();
        }
    }
}
