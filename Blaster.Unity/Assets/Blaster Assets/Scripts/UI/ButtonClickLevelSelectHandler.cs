using BlueOrb.Base.Global;
using BlueOrb.Controller.Scene;
using BlueOrb.Messaging;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Blaster_Assets.Scripts.UI
{
    [AddComponentMenu("BlueOrb/UI/Button Click Level Select Handler")]
    public class ButtonClickLevelSelectHandler : MonoBehaviour
    {
        [SerializeField]
        private SceneConfig sceneConfig;
        private Button button;

        public void Awake()
            => button = GetComponent<Button>();

        public void Start()
            => button.onClick.AddListener(() => {
                GlobalStatic.NextScene = sceneConfig.SceneName;
                GlobalStatic.NextSceneConfig = sceneConfig;
                MessageDispatcher.Instance.DispatchMsg("LevelSelected", 0f, null, null, null);
            });
    }
}