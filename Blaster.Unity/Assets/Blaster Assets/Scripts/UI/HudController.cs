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
        [SerializeField] private GameObject alertObject;
        //[SerializeField] private string _changeProjectileMessage;
        [SerializeField] private Color[] StartTimerColors;

        [SerializeField] 
        private string _setAmmoMessage = "SetAmmo";

        [SerializeField]
        private string toggleProjectileHudMessage = "ToggleProjectile";

        [SerializeField]
        private string addProjectileTypeHudMessage = "AddProjectileType";

        [SerializeField]
        private string removeProjectileTypeHudMessage = "RemoveProjectileType";

        [SerializeField]
        private string notificationHudMessage = "Notification";

        [SerializeField]
        private string alertHudMessage = "Alert";

        private const string SetMultiplierMessage = "SetMultiplier";
        private const string SetConsecutiveHitsMessage = "SetConsecutiveHits";

        [SerializeField]
        private UIToggleGroup projectileToggleGroup;

        [SerializeField]
        private GameObject lifeBar;

        [SerializeField]
        private GameObject buffPrefab;

        [SerializeField]
        private ScrollRect buffScrollRect;

        private const string ControllerName = "Hud Controller";

        private TextMeshProUGUI notificationText;
        private TextMeshProUGUI alertText;

        protected override void Awake()
        {
            base.Awake();
            this.notificationText = notificationObject.GetComponent<TextMeshProUGUI>();
            this.alertText = alertObject.GetComponent<TextMeshProUGUI>();
        }

        public override void StartListening()
        {
            base.StartListening();

            MessageDispatcher.Instance.StartListening(toggleProjectileHudMessage, ControllerName, (data) =>
            {
                Debug.Log("(HudController) Toggle Projectile message");
                bool isRight = (bool)data.ExtraInfo;
                projectileToggleGroup.Toggle(isRight);
            });

            MessageDispatcher.Instance.StartListening(addProjectileTypeHudMessage, ControllerName, (data) =>
            {
                Debug.Log("(HudController) Add Projectile message");
                IProjectileItem projectileItem = (IProjectileItem)data.ExtraInfo;
                projectileToggleGroup.AddItem(projectileItem);
            });

            MessageDispatcher.Instance.StartListening(removeProjectileTypeHudMessage, ControllerName, (data) =>
            {
                Debug.Log("(HudController) Remove Projectile message");
                string uniqueId = (string) data.ExtraInfo;
                projectileToggleGroup.RemoveItem(uniqueId);
            });

            MessageDispatcher.Instance.StartListening(_setAmmoMessage, ControllerName, (data) =>
            {
                var ammoData = ((string UniqueId, int CurrentAmmo))data.ExtraInfo;
                Debug.Log($"(HudController) Set Ammo to {ammoData.CurrentAmmo} message");
                var item = projectileToggleGroup.GetItem(ammoData.UniqueId);
                if (item == null)
                {
                    Debug.LogError($"Could not locate item {ammoData.UniqueId} to set ammo to");
                    return;
                }
                item.SetText(ammoData.CurrentAmmo.ToString());
            });

            MessageDispatcher.Instance.StartListening("SetHp", ControllerName, (data) =>
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

            MessageDispatcher.Instance.StartListening(this.alertHudMessage, ControllerName, (data) =>
                CreateAlert((string)data.ExtraInfo));

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

            MessageDispatcher.Instance.StartListening("Clear", ControllerName, (data) =>
            {
                projectileToggleGroup.Clear();
                this.consecutiveHitsText.SetText("0");
                this.multiplierText.SetText("1");
                this.notificationText.SetText("");
            });
        }

        public void CreateAlert(string text)
        {
            this.alertText?.SetText(text);
            iTween.ValueTo(gameObject, iTween.Hash("name", AlertAlhpaTweenName, "from", 1, "to", 0, "time", 3, "onupdate", "SetAlertAlpha"));
        }

        public GameObject CreateBuffUI()
            => GameObject.Instantiate(this.buffPrefab, this.buffScrollRect.content.transform);

        public void SetNotificationAlpha(float alpha) => this.notificationText.alpha = alpha;

        public void SetAlertAlpha(float alpha) => this.alertText.alpha = alpha;

        private string TweenName => $"{this.GetInstanceID()}_sethptween";
        private string NotificationAlhpaTweenName => $"{this.GetInstanceID()}_notificationalphatween";
        private string AlertAlhpaTweenName => $"{this.GetInstanceID()}_alertalphatween";
    }
}