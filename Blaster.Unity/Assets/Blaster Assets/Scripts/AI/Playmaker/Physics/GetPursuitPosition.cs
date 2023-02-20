using HutongGames.PlayMaker;
using BlueOrb.Physics;
using BlueOrb.Physics.SteeringBehaviors3D;

namespace BlueOrb.Scripts.AI.Playmaker
{
    [ActionCategory("BlueOrb.Physics")]
    [Tooltip("Get the pursuit position of the target.")]
    public class GetPursuitPosition : BasePlayMakerAction
    {
        [RequiredField]
        public FsmOwnerDefault gameObject;

        [RequiredField]
        public FsmGameObject target;

        [UIHint(UIHint.Variable)]
        public FsmVector3 vector;

        public bool everyFrame;

        private IPhysicsComponent entityPhysics;
        private IPhysicsComponent targetPhysics;

        public override void Reset()
        {
            gameObject = null;
            everyFrame = false;
        }

        public override void OnEnter()
        {
            var go = Fsm.GetOwnerDefaultTarget(gameObject);
            if (go == null)
            {
                return;
            }

            var entity = base.GetEntityBase(go);
            entityPhysics = entity.Components.GetComponent<IPhysicsComponent>();

            var targetEntity = base.GetEntityBase(target.Value);
            targetPhysics = targetEntity.Components.GetComponent<IPhysicsComponent>();
            UnityEngine.Debug.Log($"Entity {targetEntity} must have an IPhysicsComponent!");

            Calculate();

            if (!everyFrame)
            {
                Finish();
            }
        }

        public override void OnUpdate()
        {
            base.OnUpdate();
            Calculate();
        }

        public void Calculate()
        {
            vector.Value = SteeringBehaviorCalculations3.CalculatePursuitPosition(this.targetPhysics, this.entityPhysics);
        }
    }
}