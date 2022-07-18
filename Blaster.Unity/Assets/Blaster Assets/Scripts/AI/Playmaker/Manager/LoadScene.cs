using HutongGames.PlayMaker;
using BlueOrb.Controller.Scene;
using BlueOrb.Base.Manager;
using BlueOrb.Base.Global;

namespace BlueOrb.Scripts.AI.Playmaker.Manager
{
    [ActionCategory("BlueOrb.Manager")]
    [HutongGames.PlayMaker.Tooltip("Load a scene using the SceneController")]
    public class LoadScene : BasePlayMakerAction
    {
        [RequiredField]
        public FsmOwnerDefault gameObject;
        public FsmObject SceneConfig;
        public bool isLoadNextScene = false;
        public FsmBool PerformFade;

        public override void Reset()
        {
            gameObject = null;
            SceneConfig = null;
            PerformFade = true;
        }

        public override void OnEnter()
        {
            var go = Fsm.GetOwnerDefaultTarget(gameObject);
            if (go == null)
            {
                return;
            }
            var entity = base.GetEntityBase(go);

            SceneConfig sceneConfig = this.SceneConfig.Value as SceneConfig;
            string sceneName = isLoadNextScene ? GlobalStatic.NextScene : sceneConfig.SceneName;
            GameStateController.Instance.LoadScene(sceneName, PerformFade.Value);
        }

        public override void OnUpdate()
        {
            base.OnUpdate();
            if (!GameStateController.Instance.IsLoadingScene)
            {
                Finish();
            }
        }
    }
}
