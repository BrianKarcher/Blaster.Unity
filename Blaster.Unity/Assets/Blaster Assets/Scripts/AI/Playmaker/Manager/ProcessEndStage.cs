using HutongGames.PlayMaker;
using BlueOrb.Base.Manager;

namespace BlueOrb.Scripts.AI.Playmaker.Manager
{
    [ActionCategory("BlueOrb.Manager")]
    [HutongGames.PlayMaker.Tooltip("End stage processing")]
    public class ProcessEndStage : BasePlayMakerAction
    {
        public override void OnEnter()
        {
            GameStateController.Instance.LevelStateController.ProcessEndStage();
            Finish();
        }
    }
}