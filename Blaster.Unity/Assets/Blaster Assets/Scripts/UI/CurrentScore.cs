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
            // TODO: Do something fancy graphically here
            _textMeshProUGUI.text = score.ToString();
        }
    }
}
