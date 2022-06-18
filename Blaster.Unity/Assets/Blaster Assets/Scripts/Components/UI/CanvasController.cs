using BlueOrb.Common.Components;
using Rewired.Integration.UnityUI;
using UnityEngine;
using UnityEngine.UI;

namespace BlueOrb.Controller.Inventory
{
    [AddComponentMenu("BlueOrb/Components/Canvas Controller")]
    public class CanvasController : ComponentBase<CanvasController>
    {
        [SerializeField]
        private RewiredEventSystem rewiredEventSystem;
        [SerializeField]
        private Selectable firstSelectedGameObject;

        public override void OnEnable()
        {
            base.OnEnable();
            Debug.Log($"Setting selected object to {firstSelectedGameObject.name}");
            firstSelectedGameObject.Select();
            rewiredEventSystem?.SetSelectedGameObject(firstSelectedGameObject.gameObject);
        }

        public override void OnDisable()
        {
            base.OnDisable();
            if (rewiredEventSystem.currentSelectedGameObject != null)
            {
                this.firstSelectedGameObject = rewiredEventSystem.currentSelectedGameObject.GetComponent<Selectable>();
            }
        }
    }
}
