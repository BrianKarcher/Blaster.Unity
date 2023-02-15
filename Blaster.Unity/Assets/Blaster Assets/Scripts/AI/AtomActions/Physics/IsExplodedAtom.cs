using BlueOrb.Common.Container;
using BlueOrb.Controller.Damage;
using BlueOrb.Messaging;
using BlueOrb.Physics;
using System;
using UnityEngine;

namespace BlueOrb.Scripts.AI.AtomActions
{
    public class IsExplodedAtom : AtomActionBase
    {
        public string ExplodeMessage = "Explode";
        private Action<Telegram> _explodedDel;
        private long _explodedIndex;
        private DamageComponent damageComponent;

        public override void Start(IEntity entity)
        {
            if (_explodedDel == null)
            {
                _explodedDel = (data) =>
                {
                    var explodeData = data.ExtraInfo as ExplodeData;
                    damageComponent.StoreExplodeSelfData(explodeData);
                    Finish();
                };
            }

            if (damageComponent == null)
                damageComponent = entity.Components.GetComponent<DamageComponent>();
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
    }
}
