//using BlueOrb.Common.Container;
//using BlueOrb.Controller.Player;
//using BlueOrb.Messaging;
//using UnityEngine;

//namespace BlueOrb.Scripts.AI.AtomActions
//{
//    public class MessageCounterAtom : AtomActionBase
//    {
//        private int _hitCount;
//        public string Message;
//        public string MaterialColorName;
//        [ColorUsage(true, true)]
//        public Color[] _tints;
//        private int _limit;
//        private bool _hitLimit = false;
//        private PlayerController _playerController;
//        private Color _originalColor;

//        private long _messageId;

//        public override void Start(IEntity entity)
//        {
//            base.Start(entity);
//            _hitCount = 0;
//            _limit = _tints.Length;
//            _hitLimit = false;
//            if (_playerController == null)
//                _playerController = entity.Components.GetComponent<PlayerController>();
//            _originalColor = _playerController.GetTint(MaterialColorName);
//            SetTint();
//        }

//        public override void End()
//        {
//            base.End();
//            _playerController.SetTint(MaterialColorName, _originalColor);
//        }

//        public override void StartListening(IEntity entity)
//        {
//            base.StartListening(entity);
//            _messageId = MessageDispatcher.Instance.StartListening(Message, entity.GetId(), (data) =>
//            {
//                //_playerController.SetTint(MaterialColorName, _tints[_hitCount]);
//                _hitCount++;
//                SetTint();
//                if (_hitCount >= _limit)
//                {
//                    _hitLimit = true;
//                    Finish();
//                }
//            });
//        }

//        private void SetTint()
//        {
//            if (_hitCount > _tints.Length - 1)
//                return;
//            _playerController.SetTint(MaterialColorName, _tints[_hitCount]);
//        }

//        public override void StopListening(IEntity entity)
//        {
//            base.StopListening(entity);
//            MessageDispatcher.Instance.StopListening(Message, entity.GetId(), _messageId);
//        }
//    }
//}
