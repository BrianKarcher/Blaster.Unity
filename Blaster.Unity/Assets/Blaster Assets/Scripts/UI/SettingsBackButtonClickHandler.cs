using BlueOrb.Base.Manager;
using BlueOrb.Messaging;
using BlueOrb.Scripts.UI;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Blaster_Assets.Scripts.UI
{
    [AddComponentMenu("BlueOrb/UI/Settings Back Button Click Handler")]
    public class SettingsBackButtonClickHandler : MonoBehaviour
    {
        [SerializeField]
        private Slider slider;

        [SerializeField]
        private string buttonName;
        private Button button;

        public void Awake()
            => button = GetComponent<Button>();

        public void Start()
            => button.onClick.AddListener(() =>
            {
                GameStateController.Instance.SettingsController.SetSensitivity(this.slider.value);
                Debug.Log($"Button Clicked: Back");
                GameStateController.Instance.SettingsController.SaveSettings();
                MessageDispatcher.Instance.DispatchMsg("BackClicked", 0f, null, UIController.UIControllerId, null);
            });
    }
}