using BlueOrb.Common.Container;
using BlueOrb.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace BlueOrb.Scripts.AI.AtomActions
{
    public enum SendMessageTime
    {
        Immediately = 0,
        FirstUpdate = 1,
        TriggerEnter = 2,
        OnExit = 3
    }

    public class SendMessageAtom : AtomActionBase
    {
        public string Message;
        public string ExtraInfo;
        private List<string> _targetUniqueIds;
        public List<string> TargetUniqueIds { get { return _targetUniqueIds; } set { _targetUniqueIds = value; } }
        public string collideTag;

        public bool _sendToSelf = false;
        public bool _sendToAll = false;
        public bool _sendToMainCharacter;
        public bool _sendToUsableController;
        public bool _sendToGameController;
        public bool _sendToLevelController;
        public bool _sendToUIManager;
        public SendMessageTime SendMessageTime;
        public bool _finishOnFirstMessageSent = true;

        //public bool _sendOnFirstUpdate = false;
        private System.Action DebugEvent;

        private long _messageId;
        private bool _processedFirstUpdate = false;
        private bool _messageSent = false;

        //public override reset

        public override void Start(IEntity entity)
        {
            base.Start(entity);
            _messageSent = false;
            _processedFirstUpdate = false;
            if (Message == "VictoryPose")
            {
                DebugEvent?.Invoke();
                Debug.LogError("SendMessage VictoryPose called");
                int i = 1;
            }
            if (SendMessageTime == SendMessageTime.Immediately)
            {
                SendMessage(entity);
            }
        }

        public override void End()
        {
            base.End();
            if (SendMessageTime == SendMessageTime.OnExit)
            {
                SendMessage(_entity);
            }
        }

        private void SendMessage(IEntity entity)
        {
            if (_sendToAll)
            {
                // null dispatches to all who are listening
                Process(null);
            }
            else if (_sendToSelf)
            {
                Process(entity.GetId());
            }
            else if (_sendToMainCharacter)
            {
                var mainCharacter = GetTarget(ActionTarget.MainCharacter);
                if (mainCharacter == null)
                    Debug.LogError($"No Main Character to send message to.");
                else
                    Process(mainCharacter.GetId());
            }
            else if (_sendToUsableController)
            {
                Process("Usable Controller");
            }
            else if (_sendToUIManager)
            {
                Process("UI Controller");
            }
            else if (_sendToGameController)
            {
                Process("Game Controller");
            }
            else if (_sendToLevelController)
            {
                Process("Level Controller");
            }
            else
            {
                for (int i = 0; i < _targetUniqueIds.Count; i++)
                {
                    // Don't accidently trigger a "Send To All"
                    if (_targetUniqueIds[i] == null)
                        continue;
                    Process(_targetUniqueIds[i]);
                }
            }
            _messageSent = true;
        }

        public override void StartListening(IEntity entity)
        {
            base.StartListening(entity);
            if (SendMessageTime != SendMessageTime.TriggerEnter)
                return;
            //_messageId = MessageDispatcher.Instance.StartListening("TriggerEnter", entity.GetId(), (data) =>
            //{

            //    var collisionData = (CollisionMessageData)data.ExtraInfo;
            //    if (collisionData.OtherCollider.tag != collideTag)
            //        return;
            //    var otherCollisionComponent = collisionData.OtherCollider.GetComponent<CollisionComponent>();
            //    if (otherCollisionComponent == null)
            //        return;
            //    var repo = otherCollisionComponent.GetComponentRepository();
            //    if (repo == null)
            //        return;
            //    Debug.Log("Caught trigger for " + repo.UniqueId);
            //    // Send the message to both the requested recipients and also the target we hit.
            //    // TODO May want to change this with a flag on whether to send to target we hit or not.
            //    SendMessage(_entity);
            //    Process(repo.UniqueId);
            //    //Received(null, EventArgs.Empty);
            //    //_isRunning = false;
            //});
        }

        public override void StopListening(IEntity entity)
        {
            base.StopListening(entity);
            if (SendMessageTime != SendMessageTime.TriggerEnter)
                return;
            //MessageDispatcher.Instance.StopListening("TriggerEnter", entity.GetId(), _messageId);
        }

        public IEntity GetTarget(ActionTarget ActionTarget)
        {
            switch (ActionTarget)
            {
                case ActionTarget.Self:
                    return _entity;
                //case ActionTarget.Target:
                //    var targetingData = _entity.Components.GetComponent<AIComponent>()?.Target;
                //    if (targetingData == null)
                //    {
                //        Debug.LogError($"Could not locate AIComponent in {_entity.name}");
                //        return null;
                //    }
                //    return targetingData.GetComponent<IEntity>();
                case ActionTarget.MainCharacter:
                    return EntityContainer.Instance.GetMainCharacter();
                case ActionTarget.Companion:
                    return EntityContainer.Instance.GetCompanionCharacter();
                case ActionTarget.MCJointUnit:
                    var mainCharacter = EntityContainer.Instance.GetMainCharacter();
                    var joint = mainCharacter.GetComponent<Joint>();
                    var mcRigidBody = joint.connectedBody;
                    var jointUnit = mcRigidBody.GetComponent<IEntity>();
                    return jointUnit;

                //case ActionTarget.Parent:
                //    var parentData = _entity.Components.GetComponent<AIComponent>()?.Parent;
                //    if (parentData == null)
                //    {
                //        //Debug.LogError($"Could not locate AIComponent or Parent in {_entity.name}");
                //        return null;
                //    }
                //    return parentData.GetComponent<IEntity>();

                default:
                    return null;
            }
        }

        public void Process(string targetUniqueId)
        {
            if (Message == "TitleScreen")
            {
                Debug.LogError($"Sending TitleScreen message to {targetUniqueId}");
            }
            if (_entity == null)
                throw new Exception($"Could not locate Entity Common for message {Message}");

            MessageDispatcher.Instance.DispatchMsg(Message, 0f, _entity.GetId(), targetUniqueId, ExtraInfo);
            if (_finishOnFirstMessageSent)
                Finish();
        }

        public override void OnUpdate()
        {
            if (!_processedFirstUpdate && SendMessageTime == SendMessageTime.FirstUpdate)
            {
                SendMessage(_entity);
                _processedFirstUpdate = true;
            }

            //return AtomActionResults.Success;
        }

        //public bool IsFinished()
        //{
        //    // Never finishes
        //    if (!_finishOnFirstMessageSent)
        //        return false;
        //    return _messageSent;
        //}

        public void SetDebugEvent(System.Action debugEvent)
        {
            DebugEvent = debugEvent;
        }
    }
}
