using BlueOrb.Base.Interfaces;
using BlueOrb.Base.Item;
using UnityEngine;

namespace BlueOrb.Source.UI
{
    [AddComponentMenu("BlueOrb/UI/Toggle Group")]
    public class UIToggleGroup : MonoBehaviour
    {
        [SerializeField]
        private UIToggleItem[] _items;

        // TODO Remove serialization when done testing
        [SerializeField]
        private int _currentItemCount;
        public int CurrentIndex;

        public bool TogglingEnabled { get; set; }

        public void Awake()
        {
            //_items[CurrentItem].Selected();
            //_currentItemCount = _items.Length;
            _currentItemCount = 0;
            CurrentIndex = -1;
            
        }

        public void Start()
        {
            //SetCount(CurrentIndex);
            ActivateDisplayItemsFromCount();
        }

        //public void SetCount(int count)
        //{
        //    _currentItemCount = count;

        //    ActivateDisplayItemsFromCount();
        //    // Perform a bounds check in case the shard slot is no longer available
        //    SetCurrentItem(CurrentIndex);
        //}

        public void AddItem(IProjectileItem projectileItem)
        {
            _currentItemCount++;
            Debug.Log($"(UIToggleGroup) AddItem called, count is now {this._currentItemCount}");
            PopulateItem(projectileItem, _currentItemCount - 1);
            // We need to set the first item acquired to active
            if (this._currentItemCount == 1)
            {
                SelectItem(0);
            }
            ActivateDisplayItemsFromCount();
        }

        public void PopulateItem(IProjectileItem projectileItem, int index)
        {
            _items[index].SetItemConfig(projectileItem.ProjectileConfig);
            _items[index].SetText(projectileItem.CurrentAmmo.ToString());
        }

        public void RemoveItem(int index)
        {
            Debug.Log($"(UIToggleGroup) Removing Item {index}");
            if (index < 0 || index >= _currentItemCount)
            {
                throw new System.Exception($"Attempt to remove invalid toggle index {index}");
            }
            // Remove an item by shifting items to the right of it left by one
            for (int i = index + 1; i < _currentItemCount; i++)
            {
                _items[i - 1].SetItemConfig(_items[i].GetItemConfig());
                _items[i - 1].SetText(_items[i].GetText());
            }
            // Then deactivate the last item.
            _items[_currentItemCount - 1].gameObject.SetActive(false);
            // Then subtract 1 from the count
            _currentItemCount--;
            ActivateDisplayItemsFromCount();
        }

        /// <summary>
        /// Disable items that are less than the current count of items to display
        /// </summary>
        public void ActivateDisplayItemsFromCount()
        {
            for (int i = 0; i < _items.Length; i++)
            {
                var go = _items[i].gameObject;
                if (i < _currentItemCount)
                    go.SetActive(true);
                //NGUITools.SetActive(go, true);
                else
                    go.SetActive(false);
                //NGUITools.SetActive(go, false);
            }
        }

        public void SelectItem(int index)
        {
            GetCurrentItem()?.UnSelect();
            CurrentIndex = index;
            //CheckBounds();
            GetCurrentItem()?.Select();
        }

        public UIToggleItem GetCurrentItem()
        {
            if (CurrentIndex < 0)
                return null;
            if (CurrentIndex > _items.Length - 1)
                return null;
            return _items[CurrentIndex];
        }

        public UIToggleItem GetItem(string uniqueId)
        {
            for (int i = 0; i < _currentItemCount; i++)
            {
                if (_items[i].GetItemConfig().UniqueId == uniqueId)
                {
                    return _items[i];
                }
            }
            return null;
        }

        public UIToggleItem[] GetItems()
        {
            return _items;
        }
    }
}
