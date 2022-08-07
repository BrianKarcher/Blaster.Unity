using HutongGames.PlayMaker;
using BlueOrb.Base.Manager;

namespace BlueOrb.Scripts.AI.PlayMaker.Player
{
    [ActionCategory("BlueOrb.Player")]
    public class ResetConsecutiveHits : FsmStateAction
    {
        public override void OnEnter()
        {
            GameStateController.Instance.LevelStateController.PointsMultiplier().ResetConsecutiveHits();
            Finish();
        }
    }
}