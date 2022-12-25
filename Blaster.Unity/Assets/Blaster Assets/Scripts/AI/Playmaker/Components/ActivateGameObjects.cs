using BlueOrb.Scripts.AI.AtomActions;
using BlueOrb.Scripts.AI.AtomActions.Components;
using HutongGames.PlayMaker;
using BlueOrb.Common.Container;
using UnityEngine;
using Tooltip = HutongGames.PlayMaker.TooltipAttribute;

namespace BlueOrb.Scripts.AI.Playmaker.Input
{
    [ActionCategory("BlueOrb.GameObject")]
    [Tooltip("Enable Component")]
    public class ActivateGameObjects : BasePlayMakerAction
    {
        [RequiredField]
        [ArrayEditor(VariableType.GameObject)]
        [Tooltip("The GameObject to activate/deactivate.")]
        public FsmArray gameObjects;

        [RequiredField]
        [Tooltip("Check to activate, uncheck to deactivate Game Object.")]
        public FsmBool activate;

        [Tooltip("Reset the game objects when exiting this state. Useful if you want an object to be active only while this state is active.\nNote: Only applies to the last Game Object activated/deactivated (won't work if Game Object changes).")]
        public bool resetOnExit;

        [Tooltip("Repeat this action every frame. Useful if Activate changes over time.")]
        public bool everyFrame;

        public override void Reset()
        {
            gameObjects = null;
        }

        public override void OnEnter()
        {
            DoActivateGameObjects(this.activate.Value);

            if (!everyFrame)
            {
                Finish();
            }
        }

        public override void OnUpdate()
        {
            DoActivateGameObjects(this.activate.Value);
        }

        public override void OnExit()
        {
            base.OnExit();

            if (resetOnExit)
            {
                DoActivateGameObjects(!activate.Value);
            }
        }

        void DoActivateGameObjects(bool activate)
        {
            for (int i = 0; i < gameObjects.Length; i++)
            {
                GameObject go = (GameObject) gameObjects.objectReferences[i];
                // the stored game object might be invalid now
                if (go == null)
                {
                    return;
                }
                go.SetActive(activate);
            }
        }
    }
}