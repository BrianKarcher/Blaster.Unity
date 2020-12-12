using BlueOrb.Common.Container;
using BlueOrb.Controller.Block;
using BlueOrb.Messaging;
using BlueOrb.Physics;
using System;
using System.Collections.Generic;
using UnityEngine;
using static BlueOrb.Physics.Helpers.DrawBoxCast;

namespace BlueOrb.Scripts.AI.AtomActions
{
    public class CreateExplosionAtom : AtomActionBase
    {
        public float Force { get; set; }
        public float Radius { get; set; }
        public float Damage { get; set; }
        public float Delay { get; set; }

        //protected EntityCommonComponent _entityCommon;
        public int LayerMask { get; set; }
        public string[] Tags { get; set; }
        //public Vector3 Position { get; set; }
        private float _startTime { get; set; }
        //private bool _result;
        private PhysicsComponent _physicsComponent;

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
            //Debug.

            // Debugging purposes
            DrawBox(_entity.GetPosition(), new Vector3(Radius, Radius, Radius), Quaternion.identity, Color.yellow, 1f);
            var itemHits = UnityEngine.Physics.OverlapSphere(_entity.GetPosition(), Radius, LayerMask);
            Debug.Log($"Explode hit {itemHits.Length} items");

            Debug.Log($"Explode Count: {itemHits.Length}");
            for (int i = 0; i < itemHits.Length; i++)
            {
                var itemHit = itemHits[i];
                //if (!itemHit.isTrigger)
                //    continue;
                ComponentRepository otherEntity;
                otherEntity = itemHit.attachedRigidbody?.GetComponent<ComponentRepository>();
                if (otherEntity == null)
                {
                    otherEntity = itemHit.GetComponent<EntityItemComponent>()?.GetComponentRepository() as ComponentRepository;
                }
                if (otherEntity == null)
                {
                    continue;
                }

                // It sounds fun, but no, you can't hit yourself
                if (otherEntity.GetId() == _entity.GetId())
                    continue;

                if (_entitiesHit.Contains(otherEntity.GetId()))
                    continue;

                var itemHitTag = itemHit.tag;
                Debug.Log($"Exploded tag check on  {itemHitTag}");
                var tagFound = Array.IndexOf(Tags, itemHitTag) > -1;
                Debug.Log($"Exploded tag check on  {itemHitTag}: {tagFound}");
                if (!tagFound)
                    continue;

                Debug.Log("Exploded : " + otherEntity.name);

                ExplodeEntity(otherEntity);

                _entitiesHit.Add(otherEntity.GetId());
            }
            _entitiesHit.Clear();
            Finish();
        }

        private void ExplodeEntity(ComponentRepository entity)
        {
            MessageDispatcher.Instance.DispatchMsg("Explode", 0f, _entity.GetId(), entity.GetId(), (_entity.GetPosition(), Damage, Force, _entity.gameObject));
        }
    }
}
