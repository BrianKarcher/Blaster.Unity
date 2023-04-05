using BlueOrb.Messaging;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Blaster_Assets.Scripts.UI
{
    [AddComponentMenu("BlueOrb/UI/Button Click Handler")]
    public class ButtonClickHandler : MonoBehaviour
    {
        [SerializeField]
        private string buttonName;
        private Button button;

        public void Awake()
            => button = GetComponent<Button>();

        public void Start()
            => button.onClick.AddListener(() => MessageDispatcher.Instance.DispatchMsg("ButtonClicked", 0f, string.Empty, null, buttonName));
    }
}