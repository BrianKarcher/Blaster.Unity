using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlueOrb.Scripts.AI.AtomActions.Attack;
using BlueOrb.Scripts.AI.Playmaker;
using HutongGames.PlayMaker;
using BlueOrb.Common.Container;
using BlueOrb.Messaging;
using UnityEngine;

namespace BlueOrb.Scripts.AI.PlayMaker.Attack
{
    [ActionCategory("RQ.Stats")]
    //[Tooltip("Returns Success if Button is pressed.")]
    public class SetHP : BasePlayMakerAction
    {
        [HasFloatSlider(0f, 1f)]
        public FsmFloat Hp;
        public bool SetOnMainPlayer;

        public override void OnEnter()
        {
            var entity = base.GetEntityBase(Owner);
            string receiver = entity?.GetId();
            if (SetOnMainPlayer)
            {
                receiver = EntityContainer.Instance.GetMainCharacter().GetId();
            }
            MessageDispatcher.Instance.DispatchMsg("SetHpPercent", 0f, entity.GetId(), receiver, Hp.Value);
            Finish();
        }

        public override void OnExit()
        {
            base.OnExit();
        }
    }
}
