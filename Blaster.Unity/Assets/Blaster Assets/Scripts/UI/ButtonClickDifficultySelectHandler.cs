using BlueOrb.Base.Global;
using BlueOrb.Controller.Scene;
using BlueOrb.Messaging;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Blaster_Assets.Scripts.UI
{
    [AddComponentMenu("BlueOrb/UI/Button Click Difficulty Select Handler")]
    public class ButtonClickDifficultySelectHandler : MonoBehaviour
    {
        [SerializeField]
        private string difficulty;
        private Button button;

        public void Awake()
            => button = GetComponent<Button>();

        public void Start()
            => button.onClick.AddListener(() => {
                Debug.Log($"Button Clicked: {difficulty}");
                GlobalStatic.Difficulty = difficulty;
                MessageDispatcher.Instance.DispatchMsg("DifficultySelected", 0f, null, null, null);
            });
    }
}