using BlueOrb.Base.Interfaces;
using BlueOrb.Common.Components;
using BlueOrb.Controller.UI;
using BlueOrb.Messaging;
using BlueOrb.Source.UI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.BlueOrb.Scripts.UI
{
    [AddComponentMenu("BlueOrb/UI/HUD Controller")]
    public class HudController : ComponentBase<HudController>, IHudController
    {
        //[SerializeField] private Image _secondaryProjectileImage;
        //[SerializeField] private TextMeshProUGUI _secondaryProjectileText;
        [SerializeField] private TextMeshProUGUI _currentHpText;
        [SerializeField] private TextMeshProUGUI _levelStartTimer;
        [SerializeField] private TextMeshProUGUI multiplierText;
        [SerializeField] private TextMeshProUGUI consecutiveHitsText;
        [SerializeField] private GameObject notificationObject;
        //[SerializeField] private string _changeProjectileMessage;
        [SerializeField] private Color[] StartTimerColors;

        [SerializeField] 
        private string _setAmmoMessage = "SetAmmo";

        [SerializeField]
        private string selectProjectileHudMessage = "SelectProjectile";

        [SerializeField]
        private string addProjectileTypeHudMessage = "AddProjectileType";

        [SerializeField]
        private string removeProjectileTypeHudMessage = "RemoveProjectileType";

        [SerializeField]
        private string notificationHudMessage = "Notification";

        private const string SetMultiplierMessage = "SetMultiplier";
        private const string SetConsecutiveHitsMessage = "SetConsecutiveHits";

        [SerializeField]
        private UIToggleGroup uiToggleGroup;

        [SerializeField]
        private GameObject lifeBar;

        [SerializeField]
        private GameObject buffPrefab;

        [SerializeField]
        private ScrollRect scrollRect;

        private const string ControllerName = "Hud Controller";

        private long _setProjectileIndex, _addProjectileIndex, _removeProjectileIndex;
        private long _setAmmoIndex;
        private long _setHpIndex;
        private TextMeshProUGUI notificationText;

        protected override void Awake()
        {
            base.Awake();
            this.notificationText = notificationObject.GetComponent<TextMeshProUGUI>();
        }

        public override void StartListening()
        {
            base.StartListening();

            _setProjectileIndex = MessageDispatcher.Instance.StartListening(selectProjectileHudMessage, ControllerName, (data) =>
            {
                Debug.Log("(HudController) Select Projectile message");
                int index = (int)data.ExtraInfo;
                uiToggleGroup.SelectItem(index);
            });

            _addProjectileIndex = MessageDispatcher.Instance.StartListening(addProjectileTypeHudMessage, ControllerName, (data) =>
            {
                Debug.Log("(HudController) Add Projectile message");
                IProjectileItem projectileItem = (IProjectileItem)data.ExtraInfo;
                uiToggleGroup.AddItem(projectileItem);
            });

            _removeProjectileIndex = MessageDispatcher.Instance.StartListening(removeProjectileTypeHudMessage, ControllerName, (data) =>
            {
                Debug.Log("(HudController) Remove Projectile message");
                int index = (int)data.ExtraInfo;
                uiToggleGroup.RemoveItem(index);
            });

            _setAmmoIndex = MessageDispatcher.Instance.StartListening(_setAmmoMessage, ControllerName, (data) =>
            {
                var ammoData = ((string UniqueId, int CurrentAmmo))data.ExtraInfo;
                Debug.Log($"(HudController) Set Ammo to {ammoData.CurrentAmmo} message");
                var item = uiToggleGroup.GetItem(ammoData.UniqueId);
                if (item == null)
                {
                    Debug.LogError($"Could not locate item {ammoData.UniqueId} to set ammo to");
                    return;
                }
                item.SetText(ammoData.CurrentAmmo.ToString());
            });

            _setHpIndex = MessageDispatcher.Instance.StartListening("SetHp", ControllerName, (data) =>
            {
                (float current, float max, bool Immediate) hp = ((float current, float max, bool Immediate))data.ExtraInfo;
                Debug.Log($"(HUD) Setting current hp to {hp.current} {hp.Immediate}");
                _currentHpText.text = Mathf.FloorToInt(hp.current).ToString();
                if (hp.max == 0)
                {
                    Debug.LogError("HP Max is zero, divide by zero error!");
                    return;
                }
                Vector3 scale = new Vector3(hp.current / hp.max, 1, 1);
                Debug.Log($"(HudController) Setting hp bar to {scale}");
                if (hp.Immediate)
                {
                    iTween.StopByName(lifeBar, TweenName);
                    this.lifeBar.transform.localScale = scale;
                }
                else
                {
                    iTween.ScaleTo(lifeBar, iTween.Hash("name", TweenName, "scale", scale, "time", 1));
                }
            });

            MessageDispatcher.Instance.StartListening("ShowTimer", ControllerName, (data) =>
            {
                bool show = (bool)data.ExtraInfo;
                //Debug.Log($"Setting timer display to {show}");
                _levelStartTimer.gameObject.SetActive(show);
            });

            MessageDispatcher.Instance.StartListening("SetTimer", ControllerName, (data) =>
            {
                int displayTime = (int)data.ExtraInfo;
                //Debug.Log($"Showing display time {displayTime}");
                if (displayTime >= StartTimerColors.Length)
                {
                    _levelStartTimer.color = StartTimerColors[StartTimerColors.Length - 1];
                }
                else
                {
                    _levelStartTimer.color = StartTimerColors[displayTime];
                }
                _levelStartTimer.text = displayTime == 0 ? "START" : displayTime.ToString();
            });

            MessageDispatcher.Instance.StartListening(this.notificationHudMessage, ControllerName, (data) =>
            {
                string text = (string)data.ExtraInfo;
                this.notificationText?.SetText(text);
                iTween.ValueTo(gameObject, iTween.Hash("name", NotificationAlhpaTweenName, "from", 1, "to", 0, "time", 3, "onupdate", "SetNotificationAlpha"));
            });

            MessageDispatcher.Instance.StartListening(SetMultiplierMessage, ControllerName, (data) =>
            {
                string text = (string)data.ExtraInfo;
                this.multiplierText?.SetText(text);
            });

            MessageDispatcher.Instance.StartListening(SetConsecutiveHitsMessage, ControllerName, (data) =>
            {
                int text = (int)data.ExtraInfo;
                this.consecutiveHitsText?.SetText(text.ToString());
            });
        }

        public GameObject CreateBuffUI()
            => GameObject.Instantiate(this.buffPrefab, this.scrollRect.content.transform);

        public void SetNotificationAlpha(float alpha)
        {
            this.notificationText.alpha = alpha;
        }

        private string TweenName => $"{this.GetInstanceID()}_sethptween";
        private string NotificationAlhpaTweenName => $"{this.GetInstanceID()}_notificationalphatween";

        public override void StopListening()
        {
            base.StopListening();
            MessageDispatcher.Instance.StopListening(selectProjectileHudMessage, ControllerName, _setProjectileIndex);
            MessageDispatcher.Instance.StopListening(addProjectileTypeHudMessage, ControllerName, _addProjectileIndex);
            MessageDispatcher.Instance.StopListening(removeProjectileTypeHudMessage, ControllerName, _removeProjectileIndex);
            MessageDispatcher.Instance.StopListening(_setAmmoMessage, ControllerName, _setAmmoIndex);
            MessageDispatcher.Instance.StopListening("SetHp", ControllerName, _setHpIndex);
        }
    }
}