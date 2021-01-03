//using BlueOrb.Base.Item;
//using UnityEngine;
//using UnityEngine.UI;

//namespace BlueOrb.Source.UI
//{
//    [AddComponentMenu("RQ/UI/Toggle Item")]
//    public class UIToggleItem : MonoBehaviour
//    {
//        [SerializeField]
//        private Text Label;
//        [SerializeField]
//        private GameObject Arrow;
//        [SerializeField]
//        private GameObject SelectedObject;
//        [SerializeField]
//        private GameObject UnselectedGameObject;
//        [SerializeField]
//        private Image Image;

//        //public bool IsHammer;
//        /// <summary>
//        /// Ties a shard to an Item Config
//        /// </summary>
//        ///         // TODO Unserialize this when done testing
//        [SerializeField]
//        private ItemConfig ItemConfig;

//        public void Awake()
//        {
//            Arrow.SetActive(false);
//            //NGUITools.SetActive(Arrow, false);
//        }

//        public void SetQuantity(int quantity)
//        {
//            if (Label != null)
//                Label.text = quantity.ToString();
//        }

//        public void Select()
//        {
//            Arrow.SetActive(true);
//            SelectedObject.SetActive(true);
//            Image.sprite = ItemConfig?.HUDImageSelected;
//            //UnselectedGameObject.SetActive(false);
//            //NGUITools.SetActive(Arrow, true);
//        }

//        public void UnSelect()
//        {
//            Arrow.SetActive(false);
//            SelectedObject.SetActive(true);
//            Image.sprite = ItemConfig?.HUDImageUnselected;
//            //UnselectedGameObject.SetActive(true);
//            //NGUITools.SetActive(Arrow, false);
//        }

//        public ItemConfig GetItemConfig()
//        {
//            return ItemConfig;
//        }

//        public void SetItemConfig(ItemConfig itemConfig)
//        {
//            ItemConfig = itemConfig;
//            Image.sprite = ItemConfig.HUDImageUnselected;
//        }

//        public void SetText(string text)
//        {
//            Label.text = text;
//        }
//    }
//}
