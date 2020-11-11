using BlueOrb.Common.Container;
using BlueOrb.Messaging;
using BlueOrb.Physics;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace BlueOrb.Scripts.AI.AtomActions
{
    public class CreateExplosionAtom : AtomActionBase
    {
        public float AffectedDistance;
        public float Force;
        public float Distance;
        public float Damage;
        public float Delay;

        private PhysicsComponent _physicsComponent;
        //protected EntityCommonComponent _entityCommon;
        private int _layerMask = -1;
        private string[] _tags;
        private Vector3 _position;
        private float _startTime;
        //private bool _result;

        public override void Start(IEntity entity)
        {
            base.Start(entity);
            if (_physicsComponent == null)
                _physicsComponent = entity.Components.GetComponent<PhysicsComponent>();
            //if (_entityCommon == null)
            //    _entityCommon = ent
            _startTime = Time.time + Delay;
            if (Time.time >= _startTime)
            {
                Explode();
            }
        }

        public override void OnUpdate()
        {
            base.OnUpdate();
            if (Time.time >= _startTime)
            {
                Explode();
            }
        }

        private readonly HashSet<string> _entitiesHit = new HashSet<string>();

        private void Explode()
        {
            //_itemHits = UnityEngine.Physics.BoxCastAll(attackPos, halfExtent, transform.forward, transform.rotation, _attackData.Distance);
            var itemHits = UnityEngine.Physics.OverlapSphere(_position, Distance, _layerMask);

            Debug.Log($"Explode Count: {itemHits.Length}");
            for (int i = 0; i < itemHits.Length; i++)
            {
                var itemHit = itemHits[i];
                if (!itemHit.isTrigger)
                    continue;
                var otherEntity = itemHit.attachedRigidbody?.GetComponent<ComponentRepository>();
                if (otherEntity == null)
                    continue;

                // It sounds fun, but no, you can't hit yourself
                if (otherEntity.GetId() == _entity.GetId())
                    continue;

                Debug.Log("Exploded : " + otherEntity.name);

                if (_entitiesHit.Contains(otherEntity.GetId()))
                    continue;

                var itemHitTag = itemHit.tag;
                var tagFound = Array.IndexOf(_tags, itemHitTag) > -1;
                if (!tagFound)
                    continue;

                ExplodeEntity(otherEntity);

                _entitiesHit.Add(otherEntity.GetId());
            }
            _entitiesHit.Clear();
            Finish();
        }

        private void ExplodeEntity(ComponentRepository entity)
        {
            MessageDispatcher.Instance.DispatchMsg("Explode", 0f, _entity.GetId(), entity.GetId(), (_position, Damage, Force, _entity.gameObject));
        }

        public void SetLayerMask(int layerMask)
        {
            _layerMask = layerMask;
        }

        public void SetPosition(Vector3 position)
        {
            _position = position;
        }
    }
}
