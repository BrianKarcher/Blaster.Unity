using BlueOrb.Base.Item;
using BlueOrb.Common.Components;
using BlueOrb.Messaging;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.BlueOrb.Scripts.UI
{
    [AddComponentMenu("BlueOrb/UI/HUD Controller")]
    public class HudController : ComponentBase<HudController>
    {
        [SerializeField] private Image _secondaryProjectileImage;
        [SerializeField] private TextMeshProUGUI _secondaryProjectileText;
        [SerializeField] private TextMeshProUGUI _currentHpText;
        [SerializeField] private TextMeshProUGUI _levelStartTimer;
        [SerializeField] private string _changeProjectileMessage;
        [SerializeField] private string _setAmmoMessage;

        private const string ControllerName = "Hud Controller";

        private long _setProjectileId;
        private long _setAmmoId;
        private long _setHp;

        protected override void Awake()
        {
            base.Awake();
            _secondaryProjectileImage.gameObject.SetActive(false);
        }

        public override void StartListening()
        {
            base.StartListening();

            _setProjectileId = MessageDispatcher.Instance.StartListening(_changeProjectileMessage, ControllerName, (data) =>
            {
                if (data.ExtraInfo == null)
                {
                    _secondaryProjectileImage.gameObject.SetActive(false);
                    _secondaryProjectileImage.sprite = null;
                    _secondaryProjectileText.text = null;
                }
                else
                {
                    _secondaryProjectileImage.gameObject.SetActive(true);
                    var projectileConfig = data.ExtraInfo as ProjectileConfig;
                    _secondaryProjectileImage.sprite = projectileConfig.HUDImageSelected;
                    _secondaryProjectileText.text = projectileConfig.Ammo.ToString();
                }
            });

            _setAmmoId = MessageDispatcher.Instance.StartListening(_setAmmoMessage, ControllerName, (data) =>
            {
                var ammo = (int)data.ExtraInfo;
                _secondaryProjectileText.text = ammo.ToString();
            });

            _setHp = MessageDispatcher.Instance.StartListening("SetHp", ControllerName, (data) =>
            {
                var hp = ((float current, float max))data.ExtraInfo;
                Debug.Log($"(HUD) Setting current hp to {hp.current}");
                _currentHpText.text = Mathf.FloorToInt(hp.current).ToString();
            });

            MessageDispatcher.Instance.StartListening("ShowTimer", ControllerName, (data) =>
            {
                bool show = (bool)data.ExtraInfo;
                _levelStartTimer.gameObject.SetActive(show);
            });

            MessageDispatcher.Instance.StartListening("SetTimer", ControllerName, (data) =>
            {
                int displayTime = (int)data.ExtraInfo;
                _levelStartTimer.text = displayTime.ToString();
            });
        }

        public override void StopListening()
        {
            base.StopListening();
            MessageDispatcher.Instance.StopListening(_changeProjectileMessage, ControllerName, _setProjectileId);
            MessageDispatcher.Instance.StopListening(_setAmmoMessage, ControllerName, _setAmmoId);
            MessageDispatcher.Instance.StopListening("SetHp", ControllerName, _setHp);
        }
    }
}