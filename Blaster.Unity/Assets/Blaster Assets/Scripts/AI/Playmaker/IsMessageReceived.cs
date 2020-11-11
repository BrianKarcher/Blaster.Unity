using BlueOrb.Scripts.AI.AtomActions;
using BlueOrb.Scripts.AI.AtomActions.Messaging;
using HutongGames.PlayMaker;
using BlueOrb.Base.Components;
using BlueOrb.Common.Container;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueOrb.Scripts.AI.Playmaker
{
    [ActionCategory("RQ.Messaging")]
    [Tooltip("Triggers when message is received.")]
    public class IsMessageReceived : BasePlayMakerAction
    {
        [RequiredField]
		[CheckForComponent(typeof(EntityCommonComponent))]
        [Tooltip("The main GameObject.")]
		public FsmOwnerDefault gameObject;
        public IsMessageReceivedAtom _atom;

        [UIHint(UIHint.Variable)]
        [Tooltip("Fire when message is received.")]
        public FsmEvent Received;

        [UIHint(UIHint.Variable)]
        [Tooltip("The sender game object to store")]
        public FsmGameObject storeSender;

        [UIHint(UIHint.Tag)]
        public FsmString TagCheck;

        public bool GotoPreviousState;

		public override void Reset()
		{
			gameObject = null;
        }
        public override void OnEnter()
        {
            base.OnEnter();
            var go = Fsm.GetOwnerDefaultTarget(gameObject);
            if (go == null)
            {
                return;
            }
            var entity = go.GetComponent<IEntity>();
            if (entity == null)
                throw new Exception($"Entity not found in {Fsm.Name}-{Fsm.ActiveStateName}, {_atom.Message}");
            //var rqSM = Owner.GetComponent<PlayMakerStateMachineComponent>();
            //var _entity = rqSM.GetComponentRepository();
            //var entity = Owner.GetComponent<IEntity>();
            if (!TagCheck.IsNone)
                _atom.SetTagCheck(TagCheck.Value);
            _atom.Received += _atom_Received;
            _atom.Start(entity);
            //if (_isMessageReceivedAtom.Message == "VictoryPose")
            //{
            //    Debug.LogError($"(IsMessageReceived.OnEnter()) VictoryPose called - FSM = {base.Fsm.Name}, State = {base.State.Name}, IsRunning = {_isMessageReceivedAtom.IsRunning()}");
            //}
            Tick();
        }

        private void _atom_Received(object sender, EventArgs e)
        {
            if (!storeSender.IsNone)
                storeSender.Value = _atom.GetSenderGameObject();
            Finish();
            RunEvent();
        }

        public override void OnExit()
        {
            base.OnExit();
            _atom.Received -= _atom_Received;
            _atom.End();
        }

        public override void OnUpdate()
        {
            base.OnUpdate();
            Tick();
        }

        void Tick()
        {
            if (Finished)
                return;
            if (!base.Entered)
                return;
            _atom.OnUpdate();
            if (_atom.IsFinished)
            {
                if (GotoPreviousState)
                {
                    if (Fsm.PreviousActiveState != null)
                    {
                        //Log("Goto Previous State: " + Fsm.PreviousActiveState.Name);
                        Fsm.GotoPreviousState();
                        return;
                    }
                }
                if (!storeSender.IsNone)
                    storeSender.Value = _atom.GetSenderGameObject();
                RunEvent();
                Finish();
            }
        }

        private void RunEvent()
        {
            //if (_isMessageReceivedAtom.Message == "VictoryPose")
            //{
            //    Debug.LogError($"(IsMessageReceived.RunEvent()) VictoryPose called - FSM = {base.Fsm.Name}, State = {base.State.Name}, IsRunning = {_isMessageReceivedAtom.IsRunning()}");
            //}
            Fsm.Event(Received);
        }
    }
}
