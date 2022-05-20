using BlueOrb.Base.Item;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace BlueOrb.Source.UI
{
    [AddComponentMenu("BlueOrb/UI/Toggle Item")]
    public class UIToggleItem : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI Label;
        [SerializeField]
        private GameObject Arrow;
        [SerializeField]
        private GameObject SelectedObject;
        [SerializeField]
        private GameObject UnselectedGameObject;
        [SerializeField]
        private Image Image;

        //public bool IsHammer;
        /// <summary>
        /// Ties a shard to an Item Config
        /// </summary>
        ///         // TODO Unserialize this when done testing
        [SerializeField]
        private ItemConfig ItemConfig;

        public void Awake()
        {
            Arrow?.SetActive(false);
            //NGUITools.SetActive(Arrow, false);
        }

        public void SetQuantity(int quantity)
        {
            if (Label != null)
                Label.text = quantity.ToString();
        }

        public void Select()
        {
            Arrow?.SetActive(true);
            //if (SelectedObject != null)
            //{
            //    SelectedObject.SetActive(true);
            //}
            Image.sprite = ItemConfig?.HUDImageSelected;
            //UnselectedGameObject.SetActive(false);
            //NGUITools.SetActive(Arrow, true);
        }

        public void UnSelect()
        {
            Arrow?.SetActive(false);
            //if (SelectedObject != null)
            //{
            //    SelectedObject.SetActive(true);
            //}
            Image.sprite = ItemConfig?.HUDImageSelected;
            //UnselectedGameObject.SetActive(true);
            //NGUITools.SetActive(Arrow, false);
        }

        public ItemConfig GetItemConfig()
        {
            return ItemConfig;
        }

        public void SetItemConfig(ItemConfig itemConfig)
        {
            ItemConfig = itemConfig;
            Image.sprite = ItemConfig.HUDImageSelected;
        }

        public void SetText(string text, float time = 1)
        {
            int.TryParse(Label.text, out int currentAmmo);
            int.TryParse(text, out int targetAmmo);
            iTween.ValueTo(gameObject, iTween.Hash("name", TweenName(), "from", currentAmmo, "to", targetAmmo, "time", time, "onupdate", "UpdateAmmo"));
        }

        public void UpdateAmmo(int val)
        {
            Label.text = val.ToString();
        }

        public void SetTextImmediate(string text)
        {
            iTween.StopByName(gameObject, TweenName());
            Label.text = text;
        }

        private string TweenName() => $"{this.GetInstanceID()}_tween";

        public string GetText() => Label.text;

    }
}
