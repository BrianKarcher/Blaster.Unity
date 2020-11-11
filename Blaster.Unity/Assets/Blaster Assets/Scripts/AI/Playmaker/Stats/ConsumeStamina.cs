using BlueOrb.Scripts.AI.Playmaker;
using HutongGames.PlayMaker;
using BlueOrb.Common.Container;
using BlueOrb.Controller.Damage;

namespace BlueOrb.Scripts.AI.PlayMaker.Attack
{
    [ActionCategory("RQ.Stats")]
    //[Tooltip("Returns Success if Button is pressed.")]
    public class ConsumeStamina : BasePlayMakerAction
    {
        [RequiredField]
        public FsmOwnerDefault gameObject;

        public FsmFloat Stamina;
        public FsmEvent FailedEvent;
        public FsmEvent SuccessEvent;

        private EntityStatsComponent _entityStatsComponent;

        public override void Reset()
        {
            base.Reset();
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
                _entityStatsComponent = entity.Components.GetComponent<EntityStatsComponent>();

            var success = _entityStatsComponent.ConsumeStamina(Stamina.Value);
            if (success)
            {
                Fsm.Event(SuccessEvent);
            }
            else
            {
                Fsm.Event(FailedEvent);
            }

            Finish();
        }

        public override void OnExit()
        {
            base.OnExit();
        }
    }
}
