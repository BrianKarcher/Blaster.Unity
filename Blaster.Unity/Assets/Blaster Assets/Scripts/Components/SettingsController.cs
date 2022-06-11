using BlueOrb.Base.Manager;
using BlueOrb.Common.Components;
using BlueOrb.Controller.Persistence;
using BlueOrb.Messaging;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace Assets.Blaster_Assets.Scripts.Components
{
    [AddComponentMenu("BlueOrb/Components/Settings Controller")]
    public class SettingsController : ComponentBase<SettingsController>
    {
        [SerializeField]
        private GameStateController gameState;
        [SerializeField]
        private Slider musicSlider;
        [SerializeField]
        private Slider soundEffectSlider;
        [SerializeField]
        private AudioMixer masterMixer;
        [SerializeField]
        private string musicAudioParameter;
        [SerializeField]
        private string effectAudioParameter;
        private SettingsData settingsData;

        protected override void Awake()
        {
            base.Awake();
            this.settingsData = LoadSettings();
            if (this.settingsData == null)
            {
                this.settingsData = new SettingsData
                {
                    MusicVolume = 0,
                    SoundEffectVolume = 0
                };
            }
            SetMusicVolume(this.settingsData.MusicVolume);
            SetEffectVolume(this.settingsData.SoundEffectVolume);
            this.musicSlider.value = this.settingsData.MusicVolume;
            this.soundEffectSlider.value = this.settingsData.SoundEffectVolume;
        }

        public void SaveSettings(SettingsData settingsData) => this.gameState.PersistenceController.Save(this.gameState.PersistenceController.DataFileName, settingsData);

        public SettingsData LoadSettings() => this.gameState.PersistenceController.Load<SettingsData>(this.gameState.PersistenceController.DataFileName);

        public void SetEffectVolume(float volume)
        {
            this.settingsData.SoundEffectVolume = volume;
            this.masterMixer.SetFloat(effectAudioParameter, volume);
        }

        public void SetMusicVolume(float volume)
        {
            this.settingsData.MusicVolume = volume;
            this.masterMixer.SetFloat(musicAudioParameter, volume);
        }

        public void BackClicked()
        {
            this.gameState.PersistenceController.Save(this.gameState.PersistenceController.DataFileName, this.settingsData);
            MessageDispatcher.Instance.DispatchMsg("BackClicked", 0f, GetId(), GetId(), null);
        }
    }
}
