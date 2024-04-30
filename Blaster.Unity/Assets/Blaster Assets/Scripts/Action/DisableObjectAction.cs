using BlueOrb.Common.Components;
using UnityEngine;

namespace BlueOrb.Components
{
    [AddComponentMenu("BlueOrb/Components/Actions/Disable Object")]
    public class DisableObjectsActionComponent : IAction
    {
        [SerializeField]
        private List<GameObject> objectsToDisable;

        public void Act()
        {
            foreach (var gameObject in this.objectsToDisable)
            {
                // If it has been destroyed or is otherwise innaccessible, skip
                if (gameObject == null)
                {
                    continue;
                }
                gameObject.enabled = false;
            }
        }
    }
}