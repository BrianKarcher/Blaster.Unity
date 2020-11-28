using BlueOrb.Common.Components;
using UnityEngine;

namespace BlueOrb.Controller.Inventory
{
    [AddComponentMenu("RQ/Components/AI Component")]
    public class AIComponent : ComponentBase<AIComponent>
    {
        [SerializeField]
        private Seeker _seeker;
        public Seeker Seeker => _seeker;

    }
}
