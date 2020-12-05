using BlueOrb.Common.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

namespace Assets.Blaster_Assets.Scripts.Components
{
    [AddComponentMenu("BlueOrb/Components/Points Acquired Component")]
    public class PointsAcquiredComponent : ComponentBase<PointsAcquiredComponent>
    {
        [SerializeField]
        private TextMeshProUGUI _textMeshPro;

        private Vector3 _worldPosition;

        private void Update()
        {
            var pos = UnityEngine.Camera.main.WorldToScreenPoint(_worldPosition);
            transform.position = pos;
        }

        public void SetText(string value)
        {
            _textMeshPro.text = value;
        }

        public void SetWorldPosition(Vector3 pos)
        {
            _worldPosition = pos;
        }
    }
}
