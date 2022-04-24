using BlueOrb.Base.Components;
using BlueOrb.Common.Container;
using BlueOrb.Controller.Damage;
using BlueOrb.Controller.Player;
using BlueOrb.Messaging;
using BlueOrb.Physics;
using UnityEngine;

namespace BlueOrb.Scripts.AI.AtomActions.Attack
{
    public class IsDeflectedAtom : AtomActionBase
    {
        public string DeflectMessage = "Deflected";
        private long _deflectId;
        private Vector3 _hitPos;
        public Vector3 HitPos => _hitPos;
        private Vector3 _hitNormal;
        public Vector3 HitNormal => _hitNormal;
        private DamageComponent _damageComponent;
        private PhysicsComponent _physicsComponet;

        public override void Start(IEntity entity)
        {
            base.Start(entity);
            if (_damageComponent == null)
                _damageComponent = entity.Components.GetComponent<DamageComponent>();
            if (_physicsComponet == null)
                _physicsComponet = entity.Components.GetComponent<PhysicsComponent>();
        }

        public override void StartListening(IEntity entity)
        {
            base.StartListening(entity);
            _deflectId = MessageDispatcher.Instance.StartListening(DeflectMessage, entity.GetId(), (data) =>
            {
                var otherCollider = (Collider) data.ExtraInfo;
                var otherEntity = otherCollider.attachedRigidbody?.GetComponent<EntityCommonComponent>();
                var dirBetweenEntities = _entity.transform.position - otherEntity.transform.position;
                _hitPos = otherEntity.transform.position + (dirBetweenEntities.normalized) + new Vector3(0f, 1f, 0f);
                //_hitPos = otherEntity.GetPosition();
                _hitNormal = dirBetweenEntities.normalized;
                _physicsComponet.Controller.BounceFromLocation = otherEntity.GetPosition();
                // _physicsComponet.Controller.BounceFromObject = 
                //_hitPos = raycastHit.point;
                //_hitNormal = raycastHit.normal;
                Finish();
            });
        }

        public override void StopListening(IEntity entity)
        {
            base.StopListening(entity);
            MessageDispatcher.Instance.StopListening(DeflectMessage, entity.GetId(), _deflectId);
        }
    }
}
