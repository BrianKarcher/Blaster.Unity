using HutongGames.PlayMaker;
using BlueOrb.Common.Container;
using UnityEngine;
using BlueOrb.Messaging;
using BlueOrb.Scripts.UI;

namespace BlueOrb.Scripts.AI.Playmaker
{
    [ActionCategory("BlueOrb.UI")]
    [HutongGames.PlayMaker.Tooltip("Notification.")]
    public class Notification : BasePlayMakerAction
    {
        [RequiredField]
        public FsmOwnerDefault gameObject;

        [RequiredField]
        public FsmString NotificationMessage;

        private IEntity entity;

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
            MessageDispatcher.Instance.DispatchMsg("Notification", 0f, null, "Hud Controller", NotificationMessage.Value);
            Finish();
        }
    }
}