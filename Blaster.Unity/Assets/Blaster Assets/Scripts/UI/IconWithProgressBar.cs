using UnityEngine;
using UnityEngine.UI;

namespace Assets.Blaster_Assets.Scripts.UI
{
    public class IconWithProgressBar : MonoBehaviour
    {
        [SerializeField]
        private GameObject bar;
        [SerializeField]
        private Image image;

        //[SerializeField]
        //private TextMeshProUGUI textMeshProUGUI;
        //private string TweenName => $"{this.GetInstanceID()}_valuetween";
        private float scale = 1f;

        public void SetImage(Sprite image) => this.image.sprite = image;

        public void SetValue(float scale)
        {
            this.scale = scale;
            SetScrollbarScale(this.scale);
            //int.TryParse(_textMeshProUGUI.text, out int currentScore);
            //iTween.ScaleTo
            //iTween.ValueTo(gameObject, iTween.Hash("name", TweenName, "from", currentScore, "to", score, "time", 1, "onupdate", "UpdateScore"));
        }

        private void SetScrollbarScale(float scale) => bar.transform.localScale = new Vector3(scale, 1, 1);

        //public void SetScoreImmediate(int score)
        //{
        //    iTween.StopByName(TweenName);
        //    UpdateScore(score);
        //}
    }
}
