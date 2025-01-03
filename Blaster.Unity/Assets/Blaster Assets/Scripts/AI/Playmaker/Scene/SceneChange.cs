﻿using HutongGames.PlayMaker;
using BlueOrb.Base.Manager;
using BlueOrb.Controller.Scene;

namespace BlueOrb.Scripts.AI.PlayMaker.Scene
{
    [ActionCategory("BlueOrb.Scene")]
    public class SceneChange : FsmStateAction
    {
        [Tooltip("Scene config to change to.")]
        public FsmObject SceneConfig;

        public FsmString SpawnPointUniqueId;
        public FsmBool PerformFade;

        public override void OnEnter()
        {
            var sceneConfig = SceneConfig.Value as SceneConfig;
            GameStateController.Instance.LoadScene(sceneConfig.SceneName, SpawnPointUniqueId.Value, PerformFade.Value);
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
