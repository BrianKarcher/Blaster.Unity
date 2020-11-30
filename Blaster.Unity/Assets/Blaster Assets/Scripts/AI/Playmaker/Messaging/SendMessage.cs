using BlueOrb.Scripts.AI.AtomActions;
using HutongGames.PlayMaker;
using BlueOrb.Base.Components;
using BlueOrb.Common.Container;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using TooltipAttribute = HutongGames.PlayMaker.TooltipAttribute;

namespace BlueOrb.Scripts.AI.Playmaker
{
    [ActionCategory("RQ.Messaging")]
    [Tooltip("Sends a message.")]
    public class SendMessage : FsmStateAction
    {
        [RequiredField]
		[CheckForComponent(typeof(EntityCommonComponent))]
        [Tooltip("The main GameObject.")]
		public FsmOwnerDefault gameObject;

        [UIHint(UIHint.Variable)]
        [HutongGames.PlayMaker.Tooltip("Store the GameObject that collided with the Owner of this FSM.")]
        public FsmGameObject Recipient;

        public SendMessageAtom _atom;

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
            //var rqSM = Owner.GetComponent<PlayMakerStateMachineComponent>();
            //_entity = rqSM.GetComponentRepository();
            var entity = go.GetComponent<IEntity>();
            if (!Recipient.IsNone)
            {
                if (Recipient.Value == null || Recipient.Value.gameObject == null)
                {
                    Debug.Log("(SendMessage) Could not locate Game Object " + Recipient.GetDisplayName());
                    Finish();
                    return;
                }

                var target = Recipient.Value.GetComponent<EntityCommonComponent>();
                if (target == null)
                    throw new Exception($"Invalid target {Recipient.Value.name}");
                _atom.TargetUniqueIds = new string[] { target.GetId() };
            }
            _atom.Start(entity);
        }

        public override void OnExit()
        {
            base.OnExit();
            _atom.End();
        }

        public override void OnPreprocess()
        {
            base.OnPreprocess();
            Fsm.HandleFixedUpdate = true;
        }

        public override void OnFixedUpdate()
        {
            base.OnFixedUpdate();
            _atom.OnUpdate();
            if (_atom.IsFinished)
                Finish();
        }
    }
}
