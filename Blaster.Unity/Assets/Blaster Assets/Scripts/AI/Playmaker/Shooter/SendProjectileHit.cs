using HutongGames.PlayMaker;
using BlueOrb.Messaging;
using UnityEngine;
using BlueOrb.Scripts.AI.Playmaker;

namespace BlueOrb.Scripts.AI.PlayMaker.Attack
{
    [ActionCategory("BlueOrb.Shooter")]
    public class SendProjectileHit : BasePlayMakerAction
    {
        [RequiredField]
        public FsmOwnerDefault gameObject;
        public FsmString Message;
        public FsmGameObject Receiver;
        public FsmGameObject Collider;

        public override void OnEnter()
        {
            var go = Fsm.GetOwnerDefaultTarget(gameObject);
            if (go == null)
            {
                return;
            }
            var entity = base.GetEntityBase(go);
            var receiverEntity = base.GetEntityBase(Receiver.Value);
            MessageDispatcher.Instance.DispatchMsg(Message.Value, 0f, entity.GetId(), receiverEntity.GetId(), Collider.Value);
            Finish();
        }
    }
}
