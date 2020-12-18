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

        [UIHint(UIHint.Variable)]
        [HutongGames.PlayMaker.Tooltip("Store the GameObject that collided with the Owner of this FSM.")]
        public FsmArray Recipients;

        public SendMessageAtom _atom;
        private IEntity _entity;

        private List<string> _targetIds = null;


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
            _entity = go.GetComponent<IEntity>();
            if (Recipient.IsNone && Recipients.IsNone)
            {
                _atom.TargetUniqueIds = null;
            }
            else
            {
                if (_targetIds == null)
                    _targetIds = new List<string>();
                _targetIds.Clear();
                if (!Recipients.IsNone && Recipients.objectReferences != null)
                {
                    for (int i = 0; i < Recipients.objectReferences.Length; i++)
                    {
                        var recipientGO = Recipients.objectReferences[i] as GameObject;
                        string recipientId = GetRecipientId(recipientGO);
                        _targetIds.Add(recipientId);
                    }
                }
                
                if (!Recipient.IsNone)
                {
                    string recipientId = GetRecipientId(Recipient.Value);
                    _targetIds.Add(recipientId);
                }
                _atom.TargetUniqueIds = _targetIds;
            }

            _atom.Start(_entity);
        }

        private string GetRecipientId(GameObject recipient)
        {
            if (recipient == null || recipient.gameObject == null)
            {
                Debug.Log($"{_entity.name}.{Fsm.ActiveStateName} (SendMessage) Could not locate Game Object {recipient.name}");
                Finish();
                return null;
            }

            var target = recipient.GetComponent<EntityCommonComponent>();
            if (target == null)
            {
                Debug.LogWarning($"Target {recipient.name} does not contain an EntityCommonComponent");
                return null;
            }
            return target.GetId();
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
