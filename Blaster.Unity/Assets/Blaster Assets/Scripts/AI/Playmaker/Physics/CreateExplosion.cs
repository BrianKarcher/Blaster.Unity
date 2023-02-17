using HutongGames.PlayMaker;
using PM = HutongGames.PlayMaker;
using BlueOrb.Common.Container;
using System.Linq;
using UnityEngine;
using System.Collections.Generic;
using BlueOrb.Physics;
using BlueOrb.Messaging;
using System;
using BlueOrb.Controller.Block;

namespace BlueOrb.Scripts.AI.Playmaker
{
    [ActionCategory("BlueOrb.Physics")]
    [PM.Tooltip("Create an explosion.")]
    public class CreateExplosion : BasePlayMakerAction
    {
        [RequiredField]
        public FsmOwnerDefault gameObject;
        public string ExplodeMessage = "Explode";

        public FsmFloat Force = 7;
        public FsmFloat Radius = 5;
        public FsmFloat UpwardModifier = 5;
        public FsmFloat Damage = 1;
        public FsmFloat Delay;
        public bool CanHitSelf = false;

        [UIHint(UIHint.Layer)]
        [PM.Tooltip("Layers to check.")]
        public FsmInt[] Layer;
        [UIHint(UIHint.Tag)]
        [PM.Tooltip("The Tag to search for. If Child Name is set, both name and Tag need to match.")]
        public FsmString[] Tags;
        private IEntity entity;
        private float startTime;
        private int layerMask;
        //private IPhysicsComponent _physicsComponent;

        public override void Reset()
        {
            gameObject = null;
            ExplodeMessage = "Explode";
            Force = 7;
            Radius = 5;
            Damage = 1;
        }

        public override void OnEnter()
        {
            var go = Fsm.GetOwnerDefaultTarget(gameObject);
            if (go == null)
            {
                return;
            }

            this.entity = base.GetEntityBase(go);
            layerMask = ActionHelpers.LayerArrayToLayerMask(Layer, false);
            startTime = Time.time + Delay.Value;
            if (Time.time >= startTime)
            {
                Explode();
            }
        }

        public override void OnUpdate()
        {
            base.OnUpdate();
            if (Time.time >= startTime)
            {
                Explode();
            }
        }

        private readonly HashSet<string> entitiesHit = new HashSet<string>();

        private void Explode()
        {
            entitiesHit.Clear();
            //_itemHits = UnityEngine.Physics.BoxCastAll(attackPos, halfExtent, transform.forward, transform.rotation, _attackData.Distance);
            ExplodeCastSphere();

            // Debugging purposes

            Finish();
        }

        private void ExplodeCastSphere()
        {
            //DrawBox(_entity.GetPosition(), new Vector3(Radius, Radius, Radius), Quaternion.identity, Color.yellow, 10f);
            var itemHits = UnityEngine.Physics.OverlapSphere(entity.GetPosition(), Radius.Value, layerMask);
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
                    otherEntity = itemHit.GetComponent<ComponentRepository>();
                }
                if (otherEntity == null)
                {
                    continue;
                }

                // It sounds fun, but no, you can't hit yourself
                if (!CanHitSelf && otherEntity.GetId() == entity.GetId())
                    continue;

                if (entitiesHit.Contains(otherEntity.GetId()))
                    continue;

                var itemHitTag = itemHit.tag;
                Debug.Log($"Exploded tag check on  {itemHitTag}");
                bool tagFound = false;
                for (int j = 0; j < Tags.Length; j++)
                {
                    if (Tags[j].Value == itemHitTag)
                    {
                        tagFound = true;
                        break;
                    }
                }
                Debug.Log($"Exploded tag check on  {itemHitTag}: {tagFound}");
                if (!tagFound)
                    continue;

                Debug.Log("Exploded : " + otherEntity.name);

                ExplodeEntity(otherEntity);

                entitiesHit.Add(otherEntity.GetId());
            }
        }

        private void ExplodeEntity(ComponentRepository entity)
        {
            ExplodeData explodeData = new ExplodeData()
            {
                Damage = Damage.Value,
                Force = Force.Value,
                ExplodePosition = entity.GetPosition(),
                ExplodingEntity = entity.gameObject,
                Radius = Radius.Value,
                UpwardModifier = UpwardModifier.Value
            };
            MessageDispatcher.Instance.DispatchMsg(ExplodeMessage, 0f, entity.GetId(), entity.GetId(), explodeData);
        }
    }
}
