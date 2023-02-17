using HutongGames.PlayMaker;
using BlueOrb.Physics;

namespace BlueOrb.Scripts.AI.Playmaker
{
    [ActionCategory("BlueOrb.Physics")]
    [HutongGames.PlayMaker.Tooltip("Enable Ragdoll")]
    public class EnableRagdoll : BasePlayMakerAction
    {
        [RequiredField]
        public FsmOwnerDefault gameObject;

        public FsmBool Enable;
        public FsmEvent Failed;

        private RagdollController ragdollController;

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
            if (ragdollController == null)
            {
                ragdollController = entity.Components.GetComponent<RagdollController>();
            }
            if (ragdollController == null)
            {
                Fsm.Event(Failed);
                Finish();
                return;
            }
            ragdollController?.EnableRagdoll(Enable.Value);
            Finish();
        }
    }
}