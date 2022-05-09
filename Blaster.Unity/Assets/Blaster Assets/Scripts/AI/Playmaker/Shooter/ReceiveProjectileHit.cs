using HutongGames.PlayMaker;
using BlueOrb.Common.Container;
using BlueOrb.Messaging;
using UnityEngine;
using BlueOrb.Scripts.AI.Playmaker;

namespace BlueOrb.Scripts.AI.PlayMaker.Attack
{
    [ActionCategory("BlueOrb.Shooter")]
    public class ReceiveProjectileHit : BasePlayMakerAction
    {
        [RequiredField]
        public FsmOwnerDefault gameObject;
        public FsmString Message;
        [UIHint(UIHint.Tag)]
        public FsmString ShieldTag;

        public FsmEvent Hit;
        public FsmEvent ShieldHit;
        private IEntity entity;
        private long messageId;

        public override void OnEnter()
        {
            var go = Fsm.GetOwnerDefaultTarget(gameObject);
            if (go == null)
            {
                return;
            }
            this.entity = base.GetEntityBase(go);
            if (this.entity == null)
            {
                Debug.LogError($"Entity not found in {Fsm.Name}");
                return;
            }
            StartListening(entity);
        }

        private void StartListening(IEntity entity)
        {
            this.messageId = MessageDispatcher.Instance.StartListening(Message.Value, entity.GetId(), (data) =>
            {
                GameObject collider = data.ExtraInfo as GameObject;
                if (collider != null && collider.CompareTag(ShieldTag.Value))
                {
                    Fsm.Event(ShieldHit);
                }
                else
                {
                    Fsm.Event(Hit);
                }
            });
        }

        public override void OnExit()
        {
            if (this.entity == null)
            {
                Debug.LogError($"Entity not found in {Fsm.Name}");
                return;
            }
            StopListening(this.entity);
            base.OnExit();
        }

        private void StopListening(IEntity entity)
        {
            MessageDispatcher.Instance.StopListening(Message.Value, entity.GetId(), this.messageId);
        }
    }
}