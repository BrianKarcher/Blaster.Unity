using BlueOrb.Common.Container;
using BlueOrb.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace BlueOrb.Scripts.AI.AtomActions.Messaging
{
    public class IsMessageReceivedAtom : AtomActionBase
    {
        public string Message;
        public string Data;
        private string _tagCheck;
        private long _messageId;
        public event EventHandler Received;
        private bool _executedAction = false;
        private GameObject _senderGameObject;

        private Action<Telegram> _messageReceivedDel;

        public override void Start(IEntity entity)
        {
            if (_messageReceivedDel == null)
            {
                _messageReceivedDel = (data) =>
                {
                    if (Message != data.EventName)
                    {
                        Debug.LogError($"(IsMessageReceivedAtom) '{Message}' '{data.EventName}' mismatch");
                    }
                    if (!String.IsNullOrEmpty(Data))
                    {
                        string externData = data.ExtraInfo.ToString();
                        if (Data != externData)
                            return;
                    }
                    if (!String.IsNullOrEmpty(_tagCheck))
                    {
                        var otherEntity = EntityContainer.Instance.GetEntity(data.SenderId);
                        if (otherEntity == null)
                            return;
                        if (otherEntity.tag != _tagCheck)
                            return;
                    }
                    if (Received == null)
                    {
                        Debug.LogError($"(IsMessageReceived) Received handler broken in {Message} {Data} for {entity.name}");
                    }
                    else
                    {
                        var sender = data.ExtraInfo as GameObject;
                        _senderGameObject = sender;
                        //if (sender != null)
                        //{
                        //    _senderGameObject = sender;
                        //    //var senderEntity = EntityContainer._instance.GetEntity(sender);
                        //    //if (senderEntity != null)
                        //    //{
                        //    //    _senderGameObject = senderEntity.gameObject;
                        //    //}
                        //}
                        Received(null, EventArgs.Empty);
                    }

                    _executedAction = true;
                    //_isRunning = false;
                };
            }

            base.Start(entity);
            _executedAction = false;
            _senderGameObject = null;
        }

        public override void End()
        {
            base.End();
            //if (Message == "VictoryPose")
            //{
            //    Debug.LogError("(IsMessageReceivedAtom.End()) VictoryPose called");
            //    int i = 1;
            //}
        }

        public override void StartListening(IEntity entity)
        {
            base.StartListening(entity);
            //if (Message == "TitleScreen")
            //    Debug.LogError($"{entity.name} Listening to TitleScreen");
            _messageId = MessageDispatcher.Instance.StartListening(Message, entity.GetId(), _messageReceivedDel);
        }

        public override void StopListening(IEntity entity)
        {
            base.StopListening(entity);
            MessageDispatcher.Instance.StopListening(Message, entity.GetId(), _messageId);
        }

        public override void OnUpdate()
        {
            //return _executedAction ? AtomActionResults.Success : AtomActionResults.Failure;
        }

        public GameObject GetSenderGameObject()
        {
            return _senderGameObject;
        }

        public void SetTagCheck(string tagCheck)
        {
            _tagCheck = tagCheck;
        }
    }
}
