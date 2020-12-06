//using Rewired;
//using BlueOrb.Common.Container;
//using BlueOrb.Controller.Camera;
//using BlueOrb.Controller.Player;
//using BlueOrb.Physics;
//using UnityEngine;

//namespace BlueOrb.Scripts.AI.AtomActions
//{
//    public class OverShoulderCameraAtom : AtomActionBase
//    {
//        public string HorizontalRotationAxis;
//        public string VerticalRotationAxis;
//        public float Depth;
//        public Vector3 TargetOffset;

//        private ThirdPersonCameraController _camera;
//        private Rewired.Player _rewiredPlayer;

//        private float pitch, yaw, roll;
//        private float _oldDepthClose, _oldDepthFar;
//        private Vector3 _oldTargetLocalPosOffset;
//        private Controller.Player.PlayerController _playerController;
        

//        public override void Start(IEntity entity)
//        {
//            base.Start(entity);
//            if (_camera == null)
//                _camera = entity.Components.GetComponent<ThirdPersonCameraController>();
//            if (_rewiredPlayer == null)
//                _rewiredPlayer = ReInput.players.GetPlayer(0);
//            if (_playerController == null)
//                _playerController = EntityContainer.Instance.GetMainCharacter().Components.GetComponent<Controller.Player.PlayerController>();
//            _oldDepthClose = _camera.GetDepthClose();
//            _oldDepthFar = _camera.GetDepthFar();
//            _oldTargetLocalPosOffset = _camera.GetTargetLocalPosOffset();

//            _camera.SetDepthClose(Depth);
//            _camera.SetDepthFar(Depth);
//            _camera.SetTargetLocalPosOffset(TargetOffset);
//            //var cameraRotation = _camera.GetRotation();
//            //pitch = _camera.GetRotation();

//        }

//        public override void End()
//        {
//            base.End();
//            _camera.SetDepthClose(_oldDepthClose);
//            _camera.SetDepthFar(_oldDepthFar);
//            _camera.SetTargetLocalPosOffset(_oldTargetLocalPosOffset);
//        }

//        public void OnLateUpdate()
//        {
//            if (_camera.Player == null)
//            {
//                Debug.Log("(OverShoulderCameraAtom) Camera has no Player, returning.");
//                return;
//            }
//            if (_camera.Target == null)
//            {
//                Debug.Log("(OverShoulderCameraAtom) Camera has no target, returning.");
//                return;
//            }
                

//            //var playerRotationInput = _camera.GetPlayerRotationInput();

//            // The Camera Distance is used to calculate the position, vertical pivot, and desired depth.
//            //_camera.CalculateCameraDistance(playerRotationInput);

//            _camera.ProcessPivotPointToTargetPositionImmediate();

//            //_camera.ProcessRotateCameraBasedOnTargetMovement();

//            //_camera.IncrementYaw(playerRotationInput.x);

//            //_camera.SetCameraPivotRotation();

//            if (_camera.AutoLockOntoTarget)
//            {
//                _camera.RotateTowardsLockOnTarget();
//            }
//            else
//            { 
//                var playerRotationInput = _camera.GetPlayerRotationInput();
//                // float yaw = _rewiredPlayer.GetAxis(HorizontalRotationAxis);
//                // float pitch = _rewiredPlayer.GetAxis(VerticalRotationAxis);
//                _camera.LerpCameraRotation(playerRotationInput.x, playerRotationInput.y, 0f);
//            }

//            // Since this takes into account rotation, rotation must be applied first!
//            _camera.ProcessCameraPositionOffsetImmediate();

//            _camera.ProcessDesiredDepth();

//            //_camera.transform.localPosition = new Vector3(_camera.transform.localPosition.x, _camera.transform.localPosition.y,
//            //    _currentDepth);
//            //_camera.transform.localPosition = Vector3.Slerp(_camera.transform.localPosition,
//            //    new Vector3(_camera.transform.localPosition.x, _camera.transform.localPosition.y,
//            //    _currentDepth), 5f);

//            _camera.YawClamp();
//            if (!_camera.AutoLockOntoTarget)
//            {
//                _playerController.SetRotation(_camera.yaw);
//            }
//            else
//            {
//                _playerController.LookAtTarget();
//            }
//        }

//        //public void SetLayerMask(int layerMask)
//        //{
//        //    _layerMask = layerMask;
//        //}
//    }
//}
