//using UnityEngine;

//namespace BlueOrb.Source.UI
//{
//    public enum ToggleDirection
//    {
//        Right = 0,
//        Left = 1
//    }

//    [AddComponentMenu("RQ/UI/Toggle Group")]
//    public class UIToggleGroup : MonoBehaviour
//    {
//        [SerializeField]
//        private UIToggleItem[] _items;
//        public ToggleDirection ToggleDirection = ToggleDirection.Right;
        
//        // TODO Remove serialization when done testing
//        [SerializeField]
//        // Starts at 1
//        private int _currentItemCount;
//        public int CurrentIndex;

//        public bool TogglingEnabled { get; set; }

//        public void Awake()
//        {
//            //_items[CurrentItem].Selected();
//            _currentItemCount = _items.Length;
//            CurrentIndex = -1;
//        }

//        public void Start()
//        {            
//            SetCurrentItem(CurrentIndex);
//        }



//        private void CheckBounds()
//        {
//            if (CurrentIndex < 0)
//                CurrentIndex = _currentItemCount - 1;
//            if (CurrentIndex > _currentItemCount - 1)
//                CurrentIndex = 0;
//        }

//        public void SetCount(int count)
//        {
//            _currentItemCount = count;

//            ProcessDisplayItemsFromCount();
//            // Perform a bounds check in case the shard slot is no longer available
//            SetCurrentItem(CurrentIndex);
//        }

//        /// <summary>
//        /// Disable items that are less than the current count of items to display
//        /// </summary>
//        public void ProcessDisplayItemsFromCount()
//        {
//            for (int i = 0; i < _items.Length; i++)
//            {
//                var go = _items[i].gameObject;
//                if (i < _currentItemCount)
//                    go.SetActive(true);
//                    //NGUITools.SetActive(go, true);
//                else
//                    go.SetActive(false);
//                //NGUITools.SetActive(go, false);
//            }
//        }

//        public void Toggle(bool right)
//        {
//            if (ToggleDirection != ToggleDirection.Right)
//                right = !right;
//            var newItem = right ? CurrentIndex - 1 : CurrentIndex + 1;
//            SetCurrentItem(newItem);
//        }

//        public void SetCurrentItem(int index)
//        {
//            GetCurrentItem()?.UnSelect();
//            CurrentIndex = index;
//            CheckBounds();
//            GetCurrentItem()?.Select();
//        }

//        public UIToggleItem GetCurrentItem()
//        {
//            if (CurrentIndex < 0)
//                return null;
//            if (CurrentIndex > _items.Length - 1)
//                return null;
//            return _items[CurrentIndex];
//        }

//        public UIToggleItem[] GetItems()
//        {
//            return _items;
//        }
//    }
//}
