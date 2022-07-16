using BlueOrb.Base.Interfaces;
using BlueOrb.Base.Item;
using BlueOrb.Common.Components;
using BlueOrb.Messaging;
using BlueOrb.Source.UI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.BlueOrb.Scripts.UI
{
    [AddComponentMenu("BlueOrb/UI/HUD Controller")]
    public class HudController : ComponentBase<HudController>
    {
        //[SerializeField] private Image _secondaryProjectileImage;
        //[SerializeField] private TextMeshProUGUI _secondaryProjectileText;
        [SerializeField] private TextMeshProUGUI _currentHpText;
        [SerializeField] private TextMeshProUGUI _levelStartTimer;
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
        private UIToggleGroup uiToggleGroup;

        [SerializeField]
        private GameObject lifeBar;


        private const string ControllerName = "Hud Controller";

        private long _setProjectileIndex, _addProjectileIndex, _removeProjectileIndex;
        private long _setAmmoIndex;
        private long _setHpIndex;

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
                if (hp.Immediate)
                {
                    iTween.StopByName(gameObject, TweenName());
                    this.lifeBar.transform.localScale = scale;
                }
                else
                {
                    iTween.ScaleTo(gameObject, iTween.Hash("name", TweenName(), "scale", scale, "time", 1));
                }
            });

            MessageDispatcher.Instance.StartListening("ShowTimer", ControllerName, (data) =>
            {
                bool show = (bool)data.ExtraInfo;
                Debug.Log($"Setting timer display to {show}");
                _levelStartTimer.gameObject.SetActive(show);
            });

            MessageDispatcher.Instance.StartListening("SetTimer", ControllerName, (data) =>
            {
                int displayTime = (int)data.ExtraInfo;
                Debug.Log($"Showing display time {displayTime}");
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
        }

        private string TweenName() => $"{this.GetInstanceID()}_sethptween";


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