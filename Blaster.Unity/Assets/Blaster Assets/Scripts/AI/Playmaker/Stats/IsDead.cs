using HutongGames.PlayMaker;
using BlueOrb.Common.Container;
using BlueOrb.Controller.Damage;

namespace BlueOrb.Scripts.AI.Playmaker.Enemies
{
    [ActionCategory("BlueOrb")]
    [HutongGames.PlayMaker.Tooltip("Is Dead State")]
    public class IsDead : BasePlayMakerAction
    {
        [RequiredField]
        public FsmOwnerDefault gameObject;

        [RequiredField]
        public FsmEvent BossDeadEvent;
        private IEntity entity;
        private EntityStatsComponent entityStatsComponent;
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
            this.entity = base.GetEntityBase(go);
            if (entityStatsComponent == null)
            {
                this.entityStatsComponent = this.entity.GetComponent<EntityStatsComponent>();
            }
            Tick();
            if (!everyFrame)
                Finish();
        }

        public override void OnUpdate()
        {
            base.OnUpdate();
            Tick();
        }

        private void Tick()
        {
            if (this.entityStatsComponent.GetEntityStats().CurrentHP <= 0)
            {
                Fsm.Event(BossDeadEvent);
            }
        }
    }
}