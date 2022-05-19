using BlueOrb.Scripts.AI.Playmaker;
using HutongGames.PlayMaker;
using BlueOrb.Common.Container;
using BlueOrb.Controller.Damage;
using UnityEngine;

namespace BlueOrb.Scripts.AI.PlayMaker.Attack
{
    [ActionCategory("RQ.Stats")]
    public class AddHp : BasePlayMakerAction
    {
        [RequiredField]
        public FsmOwnerDefault gameObject;

        public FsmFloat Hp;
        public FsmEvent IsDead;
        private IEntityStatsComponent _entityStatsComponent;

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
            IEntity entity = base.GetEntityBase(go);
            if (entity == null)
            {
                Debug.LogError($"Entity not found in {Fsm.Name}");
                return;
            }
            if (_entityStatsComponent == null)
            {
                _entityStatsComponent = entity.Components.GetComponent<IEntityStatsComponent>();
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
