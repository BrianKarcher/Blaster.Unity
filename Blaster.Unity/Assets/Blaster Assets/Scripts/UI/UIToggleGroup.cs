using BlueOrb.Base.Interfaces;
using BlueOrb.Base.Manager;
using BlueOrb.Common.Container;
using BlueOrb.Messaging;
using System.Collections.Generic;
using UnityEngine;

namespace BlueOrb.Source.UI
{
    [AddComponentMenu("BlueOrb/UI/Toggle Group")]
    public class UIToggleGroup : MonoBehaviour
    {
        [SerializeField]
        private UIToggleItem itemPrefab;

        [SerializeField]
        private GameObject parent;

        [SerializeField]
        private string selectProjectileHudMessage = "SelectProjectile";

        private List<UIToggleItem> items = new List<UIToggleItem>();
        public int CurrentIndex;

        public bool TogglingEnabled { get; set; }

        public void Awake()
        {
            //_items[CurrentItem].Selected();
            //_currentItemCount = _items.Length;
            CurrentIndex = -1;
        }

        public void Start()
        {
            //SetCount(CurrentIndex);
            //ActivateDisplayItemsFromCount();
        }

        public void AddItem(IProjectileItem projectileItem)
        {
            UIToggleItem newItem = GameObject.Instantiate<UIToggleItem>(itemPrefab, parent.transform);
            newItem.SetItemConfig(projectileItem.ProjectileConfig);
            newItem.SetText(projectileItem.CurrentAmmo.ToString());
            this.items.Add(newItem);
            Debug.Log($"(UIToggleGroup) AddItem called, count is now {this.items.Count}");
            // We need to set the first item acquired to active
            if (this.items.Count == 1)
            {
                SelectItem(0);
            }
        }

        public void RemoveItem(string uniqueId)
        {
            for (int i = 0; i < items.Count; i++)
            {
                if (items[i].GetItemConfig().UniqueId == uniqueId)
                {
                    Debug.LogError($"(UIToggleGroup) Removing Item {i}");
                    GameObject.Destroy(items[i].gameObject);
                    items.RemoveAt(i);
                    if (CurrentIndex == i)
                    {
                        CurrentIndex = CheckBounds(CurrentIndex);
                    }
                    SelectItem(CurrentIndex);
                    return;
                }
            }
        }

        public void SelectItem(int index)
        {
            GetCurrentItem()?.UnSelect();
            CurrentIndex = index;
            CurrentIndex = CheckBounds(CurrentIndex);
            GetCurrentItem()?.Select();
            string currentProjectileConfig = CurrentIndex < 0 ? null : this.items[CurrentIndex].GetItemConfig()?.UniqueId;
            GameStateController.Instance.LevelStateController.ShooterComponent.SetSecondaryProjectile(currentProjectileConfig);
        }

        private int CheckBounds(int index)
        {
            if (items.Count == 0)
            {
                return -1;
            }
            if (index < 0)
            {
                return items.Count - 1;
            }
            if (index > items.Count - 1)
            {
                return 0;
            }
            return index;
        }

        public UIToggleItem GetCurrentItem()
        {
            if (CurrentIndex < 0)
                return null;
            if (CurrentIndex > items.Count - 1)
                return null;
            return items[CurrentIndex];
        }

        public void Toggle(bool isRight)
        {
            if (items.Count == 0)
            {
                CurrentIndex = 0;
                return;
            }
            var newItem = isRight ? CurrentIndex + 1 : CurrentIndex - 1;
            SelectItem(newItem);
            var mainPlayer = EntityContainer.Instance.GetMainCharacter();
            // Inform player object the projectile has changed
            MessageDispatcher.Instance.DispatchMsg(this.selectProjectileHudMessage, 0f, null, mainPlayer.GetId(), CurrentIndex);
        }

        public UIToggleItem GetItem(string uniqueId)
        {
            for (int i = 0; i < items.Count; i++)
            {
                if (items[i].GetItemConfig().UniqueId == uniqueId)
                {
                    return items[i];
                }
            }
            return null;
        }

        public List<UIToggleItem> GetItems() => items;

        public void Clear()
        {
            for (int i = 0; i < items.Count; i++)
            {
                GameObject.Destroy(items[i].gameObject);
            }
            items.Clear();
        }
    }
}