using BlueOrb.Common.Components;
using TMPro;
using UnityEngine;

namespace Assets.Blaster_Assets.Scripts.Components
{
    [AddComponentMenu("BlueOrb/Components/Pin UI To World Space Component")]
    public class PinUIToWorldSpaceComponent : ComponentBase<PinUIToWorldSpaceComponent>
    {
        [SerializeField]
        private Vector3 _worldPosition;
        private TextMeshProUGUI text;

        protected override void Awake()
        {
            base.Awake();
            text = GetComponent<TextMeshProUGUI>();
        }

        private void Update()
        {
            var pos = UnityEngine.Camera.main.WorldToScreenPoint(_worldPosition);
            this.text.enabled = pos.z > 0;
            transform.position = pos;
        }

        public void SetWorldPosition(Vector3 pos)
        {
            _worldPosition = pos;
        }
    }
}
