﻿using Assets.Blaster_Assets.Scripts.AI.Playmaker.Physics.Data;
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
        public string ExplodeMessage = "Explode";
        public bool HasPoints = true;
        public float Force { get; set; }
        public float Radius { get; set; }
        public float Damage { get; set; }
        public float Delay { get; set; }
        public bool CastSphere { get; set; }
        public List<GameObject> Recipients { get; set; }

        //protected EntityCommonComponent _entityCommon;
        public int LayerMask { get; set; }
        public string[] Tags { get; set; }
        //public Vector3 Position { get; set; }
        private float _startTime { get; set; }
        //private bool _result;
        private IPhysicsComponent _physicsComponent;

        public override void Start(IEntity entity)
        {
            base.Start(entity);
            if (_physicsComponent == null)
                _physicsComponent = entity.Components.GetComponent<IPhysicsComponent>();
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
            if (CastSphere)
            {
                ExplodeCastSphere();
            }

            if (Recipients != null && Recipients.Count != 0)
            {
                foreach (var recipient in Recipients)
                {
                    ComponentRepository otherEntity;
                    otherEntity = recipient.GetComponent<ComponentRepository>();
                    ExplodeEntity(otherEntity);
                }
            }
            // Debugging purposes

            _entitiesHit.Clear();
            Finish();
        }

        private void ExplodeCastSphere()
        {
            DrawBox(_entity.GetPosition(), new Vector3(Radius, Radius, Radius), Quaternion.identity, Color.yellow, 10f);
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
        }

        private void ExplodeEntity(ComponentRepository entity)
        {
            ExplodeData explodeData = new ExplodeData()
            {
                Damage = Damage,
                Force = Force,
                ExplodePosition = _entity.GetPosition(),
                HasPoints = HasPoints,
                ExplodingEntity = _entity.gameObject
            };
            MessageDispatcher.Instance.DispatchMsg(ExplodeMessage, 0f, _entity.GetId(), entity.GetId(), explodeData);
        }
    }
}
