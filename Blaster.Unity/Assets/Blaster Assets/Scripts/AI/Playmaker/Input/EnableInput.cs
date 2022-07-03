using HutongGames.PlayMaker;
using UnityEngine;
using BlueOrb.Scripts.AI.Playmaker;
using BlueOrb.Controller.Manager;

namespace BlueOrb.Scripts.AI.PlayMaker.Attack
{
    [ActionCategory("BlueOrb.Input")]
    [HutongGames.PlayMaker.Tooltip("Enable player input.")]
    public class EnableInput : BasePlayMakerAction
    {
        public bool Enable;
        public bool RevertOnExit;
        private bool oldValue;
        private LevelStateController levelStateController;

        public override void Reset()
        {
            Enable = true;
            RevertOnExit = false;
        }

        public override void OnEnter()
        {
            if (levelStateController == null)
            {
                levelStateController = GameObject.FindObjectOfType<LevelStateController>();
            }
            oldValue = levelStateController.EnableInput;
            levelStateController.EnableInput = Enable;
            Finish();
        }

        public override void OnExit()
        {
            base.OnExit();
            if (RevertOnExit)
            {
                levelStateController.EnableInput = oldValue;
            }
        }
    }
}