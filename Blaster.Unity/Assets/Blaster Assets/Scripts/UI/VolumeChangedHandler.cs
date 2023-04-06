using BlueOrb.Base.Manager;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Blaster_Assets.Scripts.UI
{
    [AddComponentMenu("BlueOrb/UI/Volume Changed Handler")]
    public class VolumeChangedHandler : MonoBehaviour
    {
        [SerializeField]
        private Slider slider;

        public enum VolumeType
        {
            Music = 0,
            Audio = 1
        }
        [SerializeField]
        private VolumeType volumeType;

        public void Awake()
            => slider = GetComponent<Slider>();

        public void Start()
            => slider.onValueChanged.AddListener((float value) => {
                Debug.Log($"Setting Volume to {value}");
                if (volumeType == VolumeType.Music)
                {
                    GameStateController.Instance.SettingsController.SetMusicVolume(value);
                }
                else if (volumeType == VolumeType.Audio)
                {
                    GameStateController.Instance.SettingsController.SetEffectVolume(value);
                }
            });
    }
}