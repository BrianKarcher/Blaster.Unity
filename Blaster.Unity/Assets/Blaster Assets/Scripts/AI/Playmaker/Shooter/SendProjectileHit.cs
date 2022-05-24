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
                Debug.LogError($"Entity not found in {Fsm.Name}");
                return;
            }
            var entity = base.GetEntityBase(go);
            if (entity == null)
            {
                Debug.LogError($"Entity not found in {Fsm.Name}");
                return;
            }
            var receiverEntity = base.GetEntityBase(Receiver.Value);
            if (receiverEntity == null)
            {
                Debug.Log($"Receiver Entity not found in {Fsm.Name}");
                return;
            }
            MessageDispatcher.Instance.DispatchMsg(Message.Value, entity.GetId(), receiverEntity.GetId(), Collider.Value);
            Finish();
        }
    }
}