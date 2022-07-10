using BlueOrb.Scripts.AI.AtomActions;
using HutongGames.PlayMaker;
using BlueOrb.Common.Container;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rewired;
using BlueOrb.Messaging;
using UnityEngine;
using BlueOrb.Controller.Manager;

namespace BlueOrb.Scripts.AI.Playmaker.Manager
{
    [ActionCategory("BlueOrb.Manager")]
    [HutongGames.PlayMaker.Tooltip("Level State Play Mode.")]
    public class LevelStatePlay : BasePlayMakerAction
    {
        [RequiredField]
        public FsmOwnerDefault gameObject;
        public FsmString ToggleLeftAction = "ToggleLeft";
        public FsmString ToggleRightAction = "ToggleRight";
        public FsmString ToggleMessage = "ToggleProjectile";
        public FsmString ToggleMessageRecipient = "Shooter Controller";
        public FsmEvent DeadEvent;
        private IEntity entity;
        private Player player;
        private LevelStateController levelStateController;

        private int toggleDirectionPressed = 0;

        public override void Reset()
        {
            gameObject = null;
        }

        public override void OnEnter()
        {
            var go = Fsm.GetOwnerDefaultTarget(gameObject);
            if (go == null)
            {
                return;
            }

            this.entity = base.GetEntityBase(go);
            if (this.entity == null)
                throw new Exception($"Entity not found in {Fsm.ActiveStateName}");

            if (levelStateController == null)
            {
                levelStateController = GameObject.FindObjectOfType<LevelStateController>();
            }
            levelStateController.EnableInput = true;
            player = ReInput.players.GetPlayer(0);
            if (player == null)
            {
                Debug.LogError("Could not locate Rewired Player");
                return;
            }

            StartListening(this.entity);
        }

        private void StartListening(IEntity entity)
        {

        }

        public override void OnUpdate()
        {
            base.OnUpdate();
            if (player == null)
            {
                return;
            }
            if (levelStateController.GetCurrentHp() <= 0)
            {
                Fsm.Event(DeadEvent);
                return;
            }
            float toggleAxis = player.GetAxis("Toggle");
            if (toggleAxis > 0.1f)
            {
                if (toggleDirectionPressed != 1)
                {
                    toggleDirectionPressed = 1;
                    Debug.Log("Toggle Right pressed");
                    MessageDispatcher.Instance.DispatchMsg(ToggleMessage.Value, 0, this.entity.GetId(), this.ToggleMessageRecipient.Value, 1.0f);
                }
            }
            else if (toggleAxis < -0.1f)
            {
                if (toggleDirectionPressed != 1)
                {
                    toggleDirectionPressed = -1;
                    Debug.Log("Toggle Left pressed");
                    MessageDispatcher.Instance.DispatchMsg(ToggleMessage.Value, 0, this.entity.GetId(), this.ToggleMessageRecipient.Value, -1.0f);
                }
            }
            else
            {
                toggleDirectionPressed = 0;
            }

            //if (player.GetButtonDown(ToggleRightAction.Value))
            //{
            //    Debug.Log("Toggle Right pressed");
            //    MessageDispatcher.Instance.DispatchMsg(ToggleMessage.Value, 0, this.entity.GetId(), this.ToggleMessageRecipient.Value, 1.0f);
            //}
            //else if (player.GetButtonDown(ToggleLeftAction.Value))
            //{
            //    Debug.Log("Toggle Left pressed");
            //    MessageDispatcher.Instance.DispatchMsg(ToggleMessage.Value, 0, this.entity.GetId(), this.ToggleMessageRecipient.Value, -1.0f);
            //}
        }

        public override void OnExit()
        {
            base.OnExit();
            StopListening(this.entity);
        }

        private void StopListening(IEntity entity)
        {

        }
    }
}
