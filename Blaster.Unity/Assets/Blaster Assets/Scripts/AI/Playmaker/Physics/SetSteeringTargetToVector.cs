using BlueOrb.Scripts.AI.AtomActions.Physics;
using HutongGames.PlayMaker;
using BlueOrb.Common.Container;

namespace BlueOrb.Scripts.AI.Playmaker
{
    [ActionCategory("RQ.Physics")]
    [Tooltip("Sets the Steering Target to Vector.")]
    public class SetSteeringTargetToVector : BasePlayMakerAction
    {
        [RequiredField]
        public FsmOwnerDefault gameObject;

        public FsmVector3 TargetVector;
        public FsmGameObject GoToGameObject;
        public SetSteeringTargetToVectorAtom _atom;
        [Tooltip("Repeat every frame while the state is active.")]
        public bool everyFrame;

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
            //var rqSM = Owner.GetComponent<PlayMakerStateMachineComponent>();
            //_entity = rqSM.GetComponentRepository();
            //var entity = Owner.GetComponent<IEntity>();
            if (!GoToGameObject.IsNone)
            {
                _atom.SetGameObject(GoToGameObject.Value);
            }
            SetTargetVector();
            _atom.Start(entity);
            if (!everyFrame)
            {
                Finish();
            }
        }

        private void SetTargetVector()
        {
            if (!TargetVector.IsNone)
            {
                _atom.SetTargetVector(TargetVector.Value);
            }
        }

        public override void OnUpdate()
        {
            SetTargetVector();
            _atom.OnUpdate();
        }

        public override void OnExit()
        {
            base.OnExit();
            _atom.End();
        }
    }
}
