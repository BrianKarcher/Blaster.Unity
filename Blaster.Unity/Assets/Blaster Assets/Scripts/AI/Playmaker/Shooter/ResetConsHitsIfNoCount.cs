using HutongGames.PlayMaker;
using BlueOrb.Scripts.AI.Playmaker;
using BlueOrb.Base.Manager;
using BlueOrb.Controller.Buff;

namespace BlueOrb.Scripts.AI.PlayMaker.Attack
{
    [ActionCategory("BlueOrb.Shooter")]
    public class ResetConsHitsIfNoCount : BasePlayMakerAction
    {
        [RequiredField]
        public FsmOwnerDefault gameObject;

        [UIHint(UIHint.Variable)]
        public FsmInt Counter;

        public BuffConfig MultiplierShieldBuffConfig;

        public override void OnEnter()
        {
            var go = Fsm.GetOwnerDefaultTarget(gameObject);
            if (go == null)
            {
                Finish();
                return;
            }
            if (this.MultiplierShieldBuffConfig != null
                && GameStateController.Instance.LevelStateController.InventoryComponent.ContainsItem(this.MultiplierShieldBuffConfig.UniqueId))
            {
                // As long as you have the item in inventory, that means Multiplier Shield is active. So just return.
                Finish();
                return;
            }
            if (Counter.Value == 0)
            {
                GameStateController.Instance.LevelStateController.PointsMultiplier().ResetConsecutiveHits();
            }
            Finish();
        }
    }
}