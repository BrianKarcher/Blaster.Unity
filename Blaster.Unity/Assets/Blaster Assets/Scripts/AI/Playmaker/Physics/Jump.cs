using HutongGames.PlayMaker;
using BlueOrb.Physics;

namespace BlueOrb.Scripts.AI.Playmaker
{
    [ActionCategory("BlueOrb.Physics")]
    [Tooltip("Performs a full jump.")]
    public class Jump : BasePlayMakerAction
    {
        [RequiredField]
        public FsmOwnerDefault gameObject;

        public FsmInt Count;
        public FsmEvent Landed;

        private IPhysicsComponent physicsComponent;
        private bool isAirborn;
        private int currentCount;

        public override void Reset()
        {
            gameObject = null;
            Landed = null;
            Count = null;
        }

        public override void OnEnter()
        {
            var go = Fsm.GetOwnerDefaultTarget(gameObject);
            if (go == null)
            {
                return;
            }

            var entity = base.GetEntityBase(go);
            this.physicsComponent ??= entity.Components.GetComponent<IPhysicsComponent>();
            this.isAirborn = false;
            this.currentCount = 0;
            JumpNow();
        }

        public override void OnUpdate()
        {
            bool isGrounded = this.physicsComponent.GetIsGrounded();
            base.OnUpdate();
            if (this.isAirborn == false && isGrounded == false)
            {
                this.isAirborn = true;
            }
            else if (this.isAirborn == true && isGrounded == true)
            {
                this.isAirborn = false;
                if (this.currentCount >= this.Count.Value)
                {
                    Fsm.Event(Landed);
                    Finish();
                }
                else
                {
                    JumpNow();
                }
            }
        }

        private void JumpNow()
        {
            this.physicsComponent.Jump();
            this.currentCount++;
        }
    }
}
