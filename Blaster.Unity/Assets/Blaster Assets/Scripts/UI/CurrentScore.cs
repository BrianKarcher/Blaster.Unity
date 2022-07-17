using TMPro;
using UnityEngine;

namespace Assets.Blaster_Assets.Scripts.UI
{
    public class CurrentScore : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI _textMeshProUGUI;
        private string TweenName => $"{this.GetInstanceID()}_scoretween";

        public void SetScore(int score)
        {
            int.TryParse(_textMeshProUGUI.text, out int currentScore);
            iTween.ValueTo(gameObject, iTween.Hash("name", TweenName, "from", currentScore, "to", score, "time", 1, "onupdate", "UpdateScore"));
        }

        public void SetScoreImmediate(int score)
        {
            iTween.StopByName(TweenName);
            UpdateScore(score);
        }

        public void UpdateScore(int val)
        {
            _textMeshProUGUI.text = val.ToString();
        }
    }
}
