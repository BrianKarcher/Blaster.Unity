using HutongGames.PlayMaker;
using BlueOrb.Base.Manager;

namespace BlueOrb.Scripts.AI.PlayMaker.Player
{
    [ActionCategory("BlueOrb.Player")]
    public class IncrementConsecutiveHits : FsmStateAction
    {
        public override void OnEnter()
        {
            GameStateController.Instance.LevelStateController.PointsMultiplier().IncrementConsecutiveHits();
            Finish();
        }
    }
}