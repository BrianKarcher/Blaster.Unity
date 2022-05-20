using TMPro;
using UnityEngine;

namespace Assets.Blaster_Assets.Scripts.UI
{
    public class CurrentScore : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI _textMeshProUGUI;

        public void SetScore(int score)
        {
            int.TryParse(_textMeshProUGUI.text, out int currentScore);
            iTween.ValueTo(gameObject, iTween.Hash("from", currentScore, "to", score, "time", 1, "onupdate", "UpdateScore"));
        }

        public void UpdateScore(int val)
        {
            _textMeshProUGUI.text = val.ToString();
        }
    }
}
