//using Rewired;
//using BlueOrb.Common.Container;
//using BlueOrb.Controller.Camera;
//using BlueOrb.Messaging;
//using BlueOrb.Physics;
//using BlueOrb.Controller.Player;
//using UnityEngine;

//namespace BlueOrb.Scripts.AI.AtomActions
//{
//    public class LockedOnCameraAtom : AtomActionBase
//    {
//        private ThirdPersonCameraController _camera;
//        public string LockCameraAction;
//        private Rewired.Player _player;
//        private IEntity _lockedOnObject;
//        //private IEntity _mainCharacter;
//        private Controller.Player.PlayerController _playerController;

//        public override void Start(IEntity entity)
//        {
//            base.Start(entity);
//            if (_camera == null)
//                _camera = entity.Components.GetComponent<ThirdPersonCameraController>();
//            Debug.Log("(LockedOnCameraAtom) Start called.");
//            if (_player == null)
//                _player = ReInput.players.GetPlayer(0);

//            var targetLockOffset = _camera.GetLockOnOffset();
//            _camera.SetTargetLocalPosOffset(targetLockOffset);
//            var lockedOnTarget = _camera.GetLockedOnIndicator();
//            _lockedOnObject = lockedOnTarget.GetComponentRepository();
//            var mainCharacter = EntityContainer.Instance.GetMainCharacter();
//            _playerController = mainCharacter.GetComponent<Controller.Player.PlayerController>();
//        }

//        public void OnLateUpdate()
//        {
//            if (_camera.Player == null || _camera.Target == null)
//                return;

//            //_mainCharacter.transform.LookAt(_camera.Target.transform);
//            // TODO Don't call this directly, send a message
//            _playerController.LookAtTarget();

//            var playerRotationInput = _camera.GetPlayerRotationInput();

//            //if (!_camera.IsLockOnTargetAlive())
//            //{
//            //    Finish();
//            //    return;
//            //}

//            //if (_player.GetButtonUp(LockCameraAction))
//            //{
//            //    Finish();
//            //    return;
//            //}                

//            // The Camera Distance is used to calculate the position, vertical pivot, and desired depth.
//            _camera.CalculateCameraDistance(playerRotationInput);

//            _camera.ProcessPivotPointToTargetPosition();

            

//            _camera.ProcessDesiredDepth();

//            //if (_camera.AutoLockOntoTarget)
//                _camera.LookAtLockOnTarget();

//            _camera.SetCameraPivotRotation();

//            //_camera.transform.localPosition = new Vector3(_camera.transform.localPosition.x, _camera.transform.localPosition.y,
//            //    _currentDepth);
//            //_camera.transform.localPosition = Vector3.Slerp(_camera.transform.localPosition,
//            //    new Vector3(_camera.transform.localPosition.x, _camera.transform.localPosition.y,
//            //    _currentDepth), 5f);

//            _camera.YawClamp();

//            MessageDispatcher.Instance.DispatchMsg("LookAt", 0f, _entity.GetId(), _lockedOnObject.GetId(), null);
//        }

//        public override void End()
//        {
//            base.End();
//            Debug.Log("(LockedOnCameraAtom) End called.");
//            _camera.EnableLockOn(false);
//            _camera.SetTargetLocalPosOffset(Vector3.zero);
//        }
//    }
//}
