using BlueOrb.Base.Input;
using BlueOrb.Base.Item;
using BlueOrb.Base.Manager;
//using BlueOrb.Base.Skill;
using BlueOrb.Common.Components;
using BlueOrb.Messaging;
//using BlueOrb.Source.UI;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.BlueOrb.Scripts.UI
{
    [AddComponentMenu("BlueOrb/UI/HUD Controller")]
    public class HudController : ComponentBase<HudController>
    {

        private const string ControllerName = "Hud Controller";

        private long _setProjectileId;

        //        public GameObject HpBar;
        //        public GameObject SpBar;
        //        public GameObject StaminaBar;
        //        //public GameObject[] ShardGameObjects;
        //        //private Text[] ShardLabels;
        //        // TODO Unserialize this when done testing
        //        //[SerializeField]
        //        //private ItemConfig[] DisplayedShardConfigs;
        //        // TODO Unserialize this when done testing
        //        //[SerializeField]
        //        //private ItemConfig DisplayedMoldConfig;
        //        [SerializeField]
        //        private UIToggleGroup uIToggleGroup;
        //        //[SerializeField]
        //        //private float _deadzone = 0.3f;
        //        //[SerializeField]
        //        //private string _horizontalAxisName;
        //        //[SerializeField]
        //        //private string _verticalAxisName;
        //        //private List<ShardConfig> ShardConfigs;
        //        //private List<MoldConfig> MoldConfigs;
        //        private List<ItemDesc> Shards = new List<ItemDesc>();
        //        private List<ItemDesc> Molds = new List<ItemDesc>();

        //        private MoldConfig CurrentMold;
        //        private int _currentMoldIndex;
        //        //private MoldData MoldData;
        //        //private Rewired.Player _player;
        //        //public GameObject MoldHUD;
        //        //private Image MoldImage;
        //        [SerializeField]
        //        private AxisInput _horizontalAxis;
        //        [SerializeField]
        //        private AxisInput _verticalAxis;


        //        private RectTransform _hpRect;
        //        private RectTransform _staminaRect;

        //        private Action<Telegram> _setHpDel;
        //        private Action<Telegram> _setStaminaDel;
        //        private long _setHpId;
        //        private bool _axisExtendedLeft = false;
        //        private bool _axisExtendedRight = false;

        //        protected override void Awake()
        //        {
        //            base.Awake();
        //            _horizontalAxis.Init();
        //            _verticalAxis.Init();
        //            uIToggleGroup.SetCount(0);
        //            _setHpDel = (data) =>
        //            {
        //                float val = (float)data.ExtraInfo;
        //                SetHp(val);
        //            };
        //            _setStaminaDel = (data) =>
        //            {
        //                float val = (float)data.ExtraInfo;
        //                SetStamina(val);
        //            };
        //            //ShardLabels = new Text[ShardGameObjects.Length];
        //            //ShardConfigs = new List<ItemConfig>(ShardGameObjects.Length);
        //            //for (int i = 0; i < ShardGameObjects.Length; i++)
        //            //{
        //            //    ShardLabels[i] = ShardGameObjects[i].GetComponent<Text>();
        //            //}
        //            //MoldImage = MoldHUD.GetComponent<Image>();
        //        }

        //        protected void Start()
        //        {
        //            //_player = Rewired.ReInput.players.GetPlayer(0);
        //            _hpRect = HpBar.GetComponent<RectTransform>();
        //            _staminaRect = StaminaBar.GetComponent<RectTransform>();
        //        }

        //        public void Update()
        //        {
        //            ProcessHorizontalAxis();
        //            ProcessVerticalAxis();
        //        }

        //        private void ProcessVerticalAxis()
        //        {
        //            if (Molds == null)
        //                return;

        //            var axisData = _verticalAxis.GetAxisDown();
        //            bool isChanged = false;
        //            switch (axisData)
        //            {
        //                case AxisInput.AxisExtended.Negative:
        //                    //uIToggleGroup.Toggle(false);
        //                    _currentMoldIndex--;
        //                    isChanged = true;
        //                    break;
        //                case AxisInput.AxisExtended.Positive:
        //                    //uIToggleGroup.Toggle(true);
        //                    _currentMoldIndex++;
        //                    isChanged = true;
        //                    break;
        //            }
        //            if (!isChanged)
        //                return;

        //            ProcessMoldBoundsCheck();

        //            CurrentMold = Molds[_currentMoldIndex].ItemConfig as MoldConfig;
        //            GameStateController.Instance.CurrentMold = CurrentMold;
        //            DisplayMoldData();
        //        }

        //        private void ProcessMoldBoundsCheck()
        //        {
        //            // Bounds check
        //            if (_currentMoldIndex >= Molds.Count)
        //            {
        //                _currentMoldIndex = 0;
        //            }
        //            if (_currentMoldIndex < 0)
        //            {
        //                _currentMoldIndex = Molds.Count - 1;
        //            }
        //        }

        //        private void ProcessHorizontalAxis()
        //        {
        //            var axisData = _horizontalAxis.GetAxisDown();
        //            bool isChanged = false;
        //            switch (axisData)
        //            {
        //                case AxisInput.AxisExtended.Negative:
        //                    uIToggleGroup.Toggle(false);
        //                    isChanged = true;
        //                    break;
        //                case AxisInput.AxisExtended.Positive:
        //                    uIToggleGroup.Toggle(true);
        //                    isChanged = true;
        //                    break;
        //            }
        //            if (isChanged)
        //            {
        //                var currentShard = uIToggleGroup.GetCurrentItem().GetItemConfig();
        //                // The "as" will make MoldConfigs null automatically.
        //                GameStateController.Instance.CurrentShard = currentShard as ShardConfig;
        //            }
        //            //var currentAxis = _player.GetAxis(_horizontalAxisName);

        //            //if (currentAxis < -_deadzone && !_axisExtendedLeft)
        //            //{
        //            //    uIToggleGroup.Toggle(false);
        //            //    //newItem = ToggleDirection == ToggleDirection.Right ? CurrentItem - 1 : CurrentItem + 1;
        //            //    //SetCurrentItem(newItem);
        //            //    _axisExtendedLeft = true;
        //            //    _axisExtendedRight = false;
        //            //}
        //            //if (currentAxis > -_deadzone && currentAxis < _deadzone)
        //            //{
        //            //    _axisExtendedLeft = false;
        //            //    _axisExtendedRight = false;
        //            //}
        //            //if (currentAxis > _deadzone && !_axisExtendedRight)
        //            //{
        //            //    uIToggleGroup.Toggle(true);
        //            //    //newItem = ToggleDirection == ToggleDirection.Right ? CurrentItem + 1 : CurrentItem - 1;
        //            //    //SetCurrentItem(newItem);
        //            //    _axisExtendedLeft = false;
        //            //    _axisExtendedRight = true;
        //            //}

        //            // Is it a mold?
        //            //if (currentShard.ItemType != ItemTypeEnum.Shard)
        //            //    currentShard = null;
        //        }

        public override void StartListening()
        {
            base.StartListening();
            //            _setHpId = MessageDispatcher.Instance.StartListening("SetHp", "Hud Controller", _setHpDel);

            _setProjectileId = MessageDispatcher.Instance.StartListening("SetProjectile", ControllerName, (data) =>
            {
                var projectileConfig = data.ExtraInfo as ProjectileConfig;
                if (projectileConfig == null)
                    throw new Exception("No Projectile Config");


            });

            //            MessageDispatcher.Instance.StartListening("SetShardCount", "Hud Controller", (data) =>
            //            {
            //                ItemDesc itemConfigAndCount = (ItemDesc)data.ExtraInfo;
            //                var toggleItems = uIToggleGroup.GetItems();
            //                Debug.Log($"Setting shard count on {itemConfigAndCount.ItemConfig.name} to {itemConfigAndCount.Qty}");
            //                for (int i = 0; i < toggleItems.Length; i++)
            //                {
            //                    var toggleItem = toggleItems[i];
            //                    var toggleItemConfig = toggleItem.GetItemConfig();

            //                    // We found a displayed Shard to update!
            //                    if (toggleItemConfig != itemConfigAndCount.ItemConfig)
            //                        continue;
            //                    // Make sure we don't display any decimals or parts of a unit
            //                    int qty = Mathf.FloorToInt(itemConfigAndCount.Qty);
            //                    toggleItem.SetText(qty.ToString());
            //                    // Can only update one shard at a time in this call
            //                    break;
            //                }
            //                //for (int i = 0; i < ShardConfigs.Count; i++)
            //                //{
            //                //    var shardItemConfig = ShardConfigs[i];
            //                //    if (shardItemConfig == itemConfigAndCount.ItemConfig)
            //                //    {
            //                //        ShardLabels[i].text = itemConfigAndCount.Qty.ToString();
            //                //    }
            //                //}
            //            });
            //            MessageDispatcher.Instance.StartListening("AddMold", "Hud Controller", (data) =>
            //            {
            //                MoldConfig moldConfig = (MoldConfig)data.ExtraInfo;
            //                Molds.Add(new ItemDesc() { ItemConfig = moldConfig, Qty = 1 });
            //                CurrentMold = moldConfig;
            //                GameStateController.Instance.CurrentMold = moldConfig;
            //                DisplayMoldData();
            //            });
            //            MessageDispatcher.Instance.StartListening("SetMoldData", "Hud Controller", (data) =>
            //            {
            //                Molds = (List<ItemDesc>)data.ExtraInfo;
            //                if (CurrentMold == null && Molds.Count != 0)
            //                {
            //                    Debug.Log("(HudController) No current mold set, setting current mold to " + Molds[0].ItemConfig.name);
            //                    CurrentMold = Molds[0].ItemConfig as MoldConfig;
            //                    _currentMoldIndex = 0;
            //                }
            //                DisplayMoldData();
            //            });
            //            MessageDispatcher.Instance.StartListening("SetShardData", "Hud Controller", (data) =>
            //            {
            //                Shards = (List<ItemDesc>)data.ExtraInfo;
            //                DisplayMoldData();
            //            });
            //            //MessageDispatcher.Instance.StartListening("AddShardConfig", "Hud Controller", (data) =>
            //            //{

            //            //});
            //            MessageDispatcher.Instance.StartListening("RemoveMoldsAndShards", "Hud Controller", (data) =>
            //            {
            //                CurrentMold = null;
            //                Molds = null;
            //                Shards = null;
            //                DisplayMoldData();
            //            });
        }

        public override void StopListening()
        {
            base.StopListening();
            MessageDispatcher.Instance.StopListening("SetProjectile", ControllerName, _setProjectileId);
        }

        //        private void DisplayMoldData()
        //        {
        //            if (CurrentMold == null)
        //            {
        //                uIToggleGroup.SetCount(0);
        //                return;
        //            }
        //            // Account for the Mold itself as the first item
        //            uIToggleGroup.SetCount(1 + CurrentMold.Shards.Count);
        //            var toggleItems = uIToggleGroup.GetItems();
        //            toggleItems[0].SetItemConfig(CurrentMold);
        //            for (int i = 0; i < CurrentMold.Shards.Count; i++)
        //            {
        //                var shardConfig = CurrentMold.Shards[i];
        //                if (shardConfig.ItemConfig == GameStateController.Instance.CurrentShard)
        //                {
        //                    uIToggleGroup.SetCurrentItem(i + 1);
        //                }
        //                toggleItems[i + 1].SetItemConfig(shardConfig.ItemConfig);
        //                var shardQty = GetShardQty(shardConfig.ItemConfig.UniqueId);
        //                toggleItems[i + 1].SetQuantity(shardQty);
        //            }
        //        }

        //        public int GetShardQty(string uniqueId)
        //        {
        //            if (Shards == null)
        //                return 0;
        //            for (int i = 0; i < Shards.Count; i++)
        //            {
        //                var shard = Shards[i];
        //                if (shard.ItemConfig.UniqueId != uniqueId)
        //                    continue;
        //                return shard.Qty;
        //            }
        //            return 0;
        //        }

        //        public void SetHp(float value)
        //        {
        //            _hpRect.localScale = new Vector3(value, 1, 1);
        //        }

        //        public void SetStamina(float value)
        //        {
        //            _staminaRect.localScale = new Vector3(value, 1, 1);
        //        }
    }
}
