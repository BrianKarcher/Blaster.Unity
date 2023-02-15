using BlueOrb.Controller.Damage;
using BlueOrb.Scripts.AI.AtomActions;
using HutongGames.PlayMaker;

namespace BlueOrb.Scripts.AI.Playmaker
{
    [ActionCategory("BlueOrb.Physics")]
    [Tooltip("Explode self")]
    public class ProcessExplosion : BasePlayMakerAction
    {
        [RequiredField]
        public FsmOwnerDefault gameObject;

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
            if (damageComponent == null)
            {
                damageComponent = entity.Components.GetComponent<DamageComponent>();
            }
            damageComponent?.ProcessExplodeSelf();
            Finish();
        }
    }
}