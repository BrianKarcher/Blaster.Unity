using Assets.Blaster_Assets.Scripts.AI.Playmaker.Physics.Data;
using BlueOrb.Common.Container;
using BlueOrb.Messaging;
using BlueOrb.Physics;
using System;
using UnityEngine;

namespace BlueOrb.Scripts.AI.AtomActions
{
    public class IsExplodedAtom : AtomActionBase
    {
        public string ExplodeMessage = "Explode";
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
                    var explodeData = data.ExtraInfo as ExplodeData;
                    _position = explodeData.ExplodePosition;
                    _damage = explodeData.Damage;
                    _force = explodeData.Force;
                    _otherEntity = explodeData.ExplodingEntity;
                    // Aren't Tuples nice?
                    //(_position, _damage, _force, _otherEntity) = data.ExtraInfo as ExplodeData;
                    Finish();
                };
            }

            base.Start(entity);
        }

        public override void StartListening(IEntity entity)
        {
            base.StartListening(entity);
            _explodedIndex = MessageDispatcher.Instance.StartListening(ExplodeMessage, _entity.GetId(), _explodedDel);
        }

        public override void StopListening(IEntity entity)
        {
            base.StopListening(entity);
            MessageDispatcher.Instance.StopListening(ExplodeMessage, _entity.GetId(), _explodedIndex);
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
