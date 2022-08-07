using HutongGames.PlayMaker;
using BlueOrb.Base.Manager;
using BlueOrb.Controller.Buff;

namespace BlueOrb.Scripts.AI.PlayMaker.Player
{
    [ActionCategory("BlueOrb.Player")]
    public class ResetConsecutiveHits : FsmStateAction
    {
        //[SerializeField]
        public BuffConfig multiplierShieldBuffConfig;

        public override void OnEnter()
        {
            if (this.multiplierShieldBuffConfig != null
                && GameStateController.Instance.LevelStateController.InventoryComponent.ContainsItem(this.multiplierShieldBuffConfig.UniqueId))
            {
                // As long as you have the item in inventory, that means Multiplier Shield is active. So just return.
                Finish();
                return;
            }
            GameStateController.Instance.LevelStateController.PointsMultiplier().ResetConsecutiveHits();
            Finish();
        }
    }
}