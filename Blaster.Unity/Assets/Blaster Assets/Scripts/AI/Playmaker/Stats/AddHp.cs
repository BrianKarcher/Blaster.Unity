using BlueOrb.Scripts.AI.Playmaker;
using HutongGames.PlayMaker;
using BlueOrb.Common.Container;
using BlueOrb.Controller.Damage;

namespace BlueOrb.Scripts.AI.PlayMaker.Attack
{
    [ActionCategory("RQ.Stats")]
    public class AddHp : BasePlayMakerAction
    {
        [RequiredField]
        public FsmOwnerDefault gameObject;

        public FsmFloat Hp;
        public FsmEvent IsDead;
        private EntityStatsComponent _entityStatsComponent;

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
            if (_entityStatsComponent == null)
            {
                _entityStatsComponent = entity.Components.GetComponent<EntityStatsComponent>();
            }
            _entityStatsComponent.AddHp(Hp.Value);
            if (_entityStatsComponent.IsDead())
            {
                Fsm.Event(IsDead);
            }
            Finish();
        }

        public override void OnExit()
        {
            base.OnExit();
        }
    }
}
