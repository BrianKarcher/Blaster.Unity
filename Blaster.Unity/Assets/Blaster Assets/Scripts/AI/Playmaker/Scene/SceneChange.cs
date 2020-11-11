using BlueOrb.Scripts.AI.AtomActions;
using HutongGames.PlayMaker;
using BlueOrb.Base.Manager;
using BlueOrb.Common.Container;
using BlueOrb.Controller.Scene;

namespace BlueOrb.Scripts.AI.PlayMaker.Scene
{
    [ActionCategory("RQ.Scene")]
    public class SceneChange : FsmStateAction
    {
        [Tooltip("Scene config to change to.")]
        public FsmObject SceneConfig;

        public FsmString SpawnPointUniqueId;

        public override void OnEnter()
        {
            var entity = Owner.GetComponent<IEntity>();
            var sceneConfig = SceneConfig.Value as SceneConfig;
            //var scene = (UnityEngine.SceneManagement.Scene)sceneConfig.Scene;
            GameStateController.Instance.LoadScene(sceneConfig.SceneName, SpawnPointUniqueId.Value);
            Finish();
        }
    }
}
