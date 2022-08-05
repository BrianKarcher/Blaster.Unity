using BlueOrb.Scripts.AI.Playmaker;
using HutongGames.PlayMaker;
using BlueOrb.Controller.Damage;

namespace BlueOrb.Scripts.AI.PlayMaker.Attack
{
    [ActionCategory("BlueOrb.Attack")]
    //[Tooltip("Returns Success if Button is pressed.")]
    public class ReceiveDamage : BasePlayMakerAction
    {
        [RequiredField]
        public FsmOwnerDefault gameObject;

        public FsmFloat DamageAmount;
        private DamageComponent damageComponent;
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
            
            if (entity == null) return;
            damageComponent ??= entity.Components.GetComponent<DamageComponent>();
            damageComponent?.ReceiveDamage(new DamageEntityInfo { DamageAmount = DamageAmount.Value });
            Finish();
        }
    }
}
