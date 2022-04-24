using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlueOrb.Common.Container;
using BlueOrb.Controller.Attack;
using UnityEngine;

namespace BlueOrb.Scripts.AI.AtomActions.Attack
{
    public class AttackAtom : AtomActionBase
    {
        public string AttackComponentName;
        private AttackComponent _attackComponent;
        private float _attackTime;

        public override void Start(IEntity entity)
        {
            base.Start(entity);
            _attackComponent = entity.Components.GetComponent<AttackComponent>(AttackComponentName);

            //if (_skill != null)
            //    MessageDispatcher2.Instance.DispatchMsg("SkillUsed", 0f, entity.UniqueId, entity.UniqueId, _skill);
            //_attackComponent.Attacked += _attackComponent_Attacked;

            //var delay = _attackComponent.GetAttackData().StrikeDelay;
            //_attackComponent.InitiateAttack();
            //if (_attackComponent.AttackComplete)
            //{
            //    //_attackComponent.ProcessAttackNow();
            //    Finish();
            //    return;
            //}

            //_attackTime = Time.time + delay;
        }

        public override void OnUpdate()
        {
            //if (_attackComponent.AttackComplete)
            //{
            //    Finish();
            //}
            //if (Time.time > _attackTime)
            //{
            //    _attackComponent.ProcessAttackNow();
            //    Finish();
            //}
        }

        public override void End()
        {
            base.End();
            // Stop any delayed attacks in case we get aborted early
            //_attackComponent.Stop();
        }
    }
}
