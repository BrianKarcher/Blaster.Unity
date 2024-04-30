using BlueOrb.Common.Components;
using UnityEngine;

namespace BlueOrb.Components.Action
{
    [AddComponentMenu("BlueOrb/Components/Actions/Action System")]
    public class ActionSystemComponent
    {
        private List<IAction> actions;
        private void Awake()
        {
            actions = GetComponents<IAction>();
        }

        private void Act()
        {
            foreach (var action in actions)
            {
                action.Act();
            }
        }
    }
}