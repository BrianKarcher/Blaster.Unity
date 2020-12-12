using BlueOrb.Common.Container;
using BlueOrb.Messaging;
using BlueOrb.Physics;
using System;
using UnityEngine;

namespace BlueOrb.Scripts.AI.AtomActions
{
    public class IsExplodedAtom : AtomActionBase
    {
        private Vector3 _position;
        public Vector3 Position => _position;
        private float _damage;
        public float Damage => _damage;
        private float _force;
        public float Force => _force;
        private GameObject _otherEntity;
        public GameObject OtherEntity => _otherEntity;

        private Action<Telegram> _explodedDel;
        private long _explodedIndex;

        public override void Start(IEntity entity)
        {
            if (_explodedDel == null)
            {
                _explodedDel = (data) =>
                {
                    // Aren't Tuples nice?
                    (_position, _damage, _force, _otherEntity) = ((Vector3, float, float, GameObject))data.ExtraInfo;
                    Finish();
                };
            }

            base.Start(entity);
        }

        public override void StartListening(IEntity entity)
        {
            base.StartListening(entity);
            _explodedIndex = MessageDispatcher.Instance.StartListening("Explode", _entity.GetId(), _explodedDel);
        }

        public override void StopListening(IEntity entity)
        {
            base.StopListening(entity);
            MessageDispatcher.Instance.StopListening("Explode", _entity.GetId(), _explodedIndex);
        }

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
