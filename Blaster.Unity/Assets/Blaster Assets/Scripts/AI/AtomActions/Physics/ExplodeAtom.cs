using BlueOrb.Common.Container;
using BlueOrb.Controller.Damage;
using BlueOrb.Messaging;
using BlueOrb.Physics;
using System;
using UnityEngine;

namespace BlueOrb.Scripts.AI.AtomActions
{
    public class ExplodeAtom : AtomActionBase
    {
        //private Vector3 _position;
        public Vector3 Position { get; set; }
        //private float _damage;
        public float Damage { get; set; }
        //public float Damage => _damage;
        //private float _force;
        public float Force { get; set; }
        //private float _radius;
        public float Radius { get; set; }
        public float UpwardsModifier;
        //public float Force => _force;
        //private GameObject _otherEntity;
        //public GameObject OtherEntity => _otherEntity;

        private PhysicsComponent _physicsComponent;
        private EntityStatsComponent _entityStatsComponent;

        //private Action<Telegram> _explodedDel;
        //private long _explodedIndex;

        public override void Start(IEntity entity)
        {
            base.Start(entity);
            if (_physicsComponent == null)
                _physicsComponent = entity.Components.GetComponent<PhysicsComponent>();
            if (_entityStatsComponent == null)
                _entityStatsComponent = entity.Components.GetComponent<EntityStatsComponent>();

            //if (_explodedDel == null)
            //{
            //    _explodedDel = (data) =>
            //    {
            //        // Aren't Tuples nice?
            //        (_position, _damage, _force, _otherEntity) = ((Vector3, float, float, GameObject))data.ExtraInfo;
            //        Finish();
            //    };
            //}
            Explode();
        }

        private void Explode()
        {
            _entityStatsComponent.AddHp(-Damage);
            _physicsComponent.Explode(Force, Position, Radius, UpwardsModifier);
            Finish();
        }

        //public override void StartListening(IEntity entity)
        //{
        //    base.StartListening(entity);
        //    _explodedIndex = MessageDispatcher.Instance.StartListening("Explode", _entity.GetId(), _explodedDel);
        //}

        //public override void StopListening(IEntity entity)
        //{
        //    base.StopListening(entity);
        //    MessageDispatcher.Instance.StopListening("Explode", _entity.GetId(), _explodedIndex);
        //}

        //public override void OnUpdate()
        //{
        //    base.OnUpdate();
        //    _physicsComponent.AddForce2(_dragForce);
        //}

        public override void End()
        {
            base.End();
        }
    }
}
