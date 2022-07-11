using HutongGames.PlayMaker;
using BlueOrb.Common.Container;

namespace BlueOrb.Scripts.AI.Playmaker.Manager
{
    [ActionCategory("BlueOrb.Manager")]
    [HutongGames.PlayMaker.Tooltip("End stage processing")]
    public class ProcessEndStage : BasePlayMakerAction
    {
        public override void OnEnter()
        {
            EntityContainer.Instance.LevelStateController.ProcessEndStage();
            Finish();
        }
    }
}