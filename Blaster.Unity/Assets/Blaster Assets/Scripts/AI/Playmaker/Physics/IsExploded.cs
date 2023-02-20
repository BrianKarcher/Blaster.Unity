using BlueOrb.Common.Container;
using BlueOrb.Controller.Damage;
using BlueOrb.Messaging;
using BlueOrb.Physics;
using HutongGames.PlayMaker;
using System;

namespace BlueOrb.Scripts.AI.Playmaker
{
    [ActionCategory("RQ.Physics")]
    [Tooltip("Did we get exploded?")]
    public class IsExploded : BasePlayMakerAction
    {
        [RequiredField]
        public FsmOwnerDefault gameObject;

        [UIHint(UIHint.Variable)]
        [Tooltip("Fire when exploded.")]
        public FsmEvent Exploded;

        [UIHint(UIHint.Variable)]
        [Tooltip("Position where the explosion occurred.")]
        public FsmVector3 Position;

        public string ExplodeMessage = "Explode";
        private Action<Telegram> _explodedDel;
        private long _explodedIndex;
        private DamageComponent damageComponent;
        private IEntity entity;

        public override void Reset()
        {
            gameObject = null;
        }

        public override void OnEnter()
        {
            var go = Fsm.GetOwnerDefaultTarget(gameObject);
            if (go == null)
            {
                return;
            }

            entity = base.GetEntityBase(go);
            if (_explodedDel == null)
            {
                _explodedDel = (data) =>
                {
                    var explodeData = data.ExtraInfo as ExplodeData;
                    if (damageComponent == null)
                    {
                        throw new Exception($"{entity.name} has no Damage Component, cannot check IsExploded");
                    }
                    damageComponent.StoreExplodeSelfData(explodeData);
                    Fsm.Event(Exploded);
                    Finish();
                };
            }

            if (damageComponent == null)
                damageComponent = entity.Components.GetComponent<DamageComponent>();

            _explodedIndex = MessageDispatcher.Instance.StartListening(ExplodeMessage, entity.GetId(), _explodedDel);
        }

        public override void OnExit()
        {
            base.OnExit();
            MessageDispatcher.Instance.StopListening(ExplodeMessage, entity.GetId(), _explodedIndex);
        }
    }
}