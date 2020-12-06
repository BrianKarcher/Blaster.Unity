using BlueOrb.Common.Components;
using TMPro;
using UnityEngine;

namespace Assets.Blaster_Assets.Scripts.Components
{
    [AddComponentMenu("BlueOrb/Components/Pin UI To World Space Component")]
    public class PinUIToWorldSpaceComponent : ComponentBase<PinUIToWorldSpaceComponent>
    {
        private Vector3 _worldPosition;

        private void Update()
        {
            var pos = UnityEngine.Camera.main.WorldToScreenPoint(_worldPosition);
            transform.position = pos;
        }

        public void SetWorldPosition(Vector3 pos)
        {
            _worldPosition = pos;
        }
    }
}
