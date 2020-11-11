using BlueOrb.Scripts.AI.AtomActions;
using BlueOrb.Scripts.AI.Playmaker;
using HutongGames.PlayMaker;
using BlueOrb.Common.Container;
using UnityEngine;

namespace BlueOrb.Scripts.AI.PlayMaker.Attack
{
    [ActionCategory("RQ.Variable")]
    //[Tooltip("Returns Success if Button is pressed.")]
    public class GetGameObjectVariable : BasePlayMakerAction
    {
        [RequiredField]
        public FsmOwnerDefault gameObject;

        //[RequiredField]
        //[HutongGames.PlayMaker.Tooltip("The main GameObject.")]
        //public FsmOwnerDefault gameObject;

        [UIHint(UIHint.Variable)]
        [HutongGames.PlayMaker.Tooltip("Fire when message is received.")]
        public FsmGameObject StoreObject;

        public GetGameObjectVariableAtom _atom;
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
            var entity = go.GetComponent<IEntity>();
            if (entity == null)
                Debug.LogError($"Error in {Fsm.ActiveStateName}, IEntity not found in {gameObject.GameObject.Name}");
            _atom.Start(entity);
            DoUpdate();

            if (!everyFrame)
                Finish();
        }

        public override void OnUpdate()
        {
            base.OnUpdate();
            DoUpdate();
        }

        private void DoUpdate()
        {
            _atom.OnUpdate();
            StoreObject.Value = _atom.GetVariable();
        }

        public override void OnExit()
        {
            base.OnExit();
            _atom.End();
        }
    }
}
