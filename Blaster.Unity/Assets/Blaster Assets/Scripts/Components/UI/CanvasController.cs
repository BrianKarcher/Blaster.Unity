using BlueOrb.Common.Components;
using Rewired.Integration.UnityUI;
using UnityEngine;

namespace BlueOrb.Controller.Inventory
{
    [AddComponentMenu("BlueOrb/Components/Canvas Controller")]
    public class CanvasController : ComponentBase<CanvasController>
    {
        [SerializeField]
        private RewiredEventSystem rewiredEventSystem;
        [SerializeField]
        private GameObject firstSelectedGameObject;

        public override void OnEnable()
        {
            base.OnEnable();
            rewiredEventSystem?.SetSelectedGameObject(firstSelectedGameObject);
        }
    }
}
