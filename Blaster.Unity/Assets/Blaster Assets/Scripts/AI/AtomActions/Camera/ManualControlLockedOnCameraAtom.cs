//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using BlueOrb.Common.Container;
//using BlueOrb.Controller.Camera;

//namespace BlueOrb.Scripts.AI.AtomActions.Camera
//{
//    public class ManualControlLockedOnCameraAtom : AtomActionBase
//    {
//        private ThirdPersonCameraController _camera;

//        public override void Start(IEntity entity)
//        {
//            base.Start(entity);
//            if (_camera == null)
//                _camera = entity.Components.GetComponent<ThirdPersonCameraController>();
//        }

//        public void OnLateUpdate()
//        {
//            if (_camera.Player == null)
//                return;
//            if (_camera.Target == null)
//                return;

//            var playerRotationInput = _camera.GetPlayerRotationInput();

//            // The Camera Distance is used to calculate the position, vertical pivot, and desired depth.
//            _camera.CalculateCameraDistance(playerRotationInput);

//            _camera.ProcessPivotPointToTargetPosition();

//            _camera.IncrementYaw(playerRotationInput.x);

//            //if (_isLockedOn)
//            //{
//            //    _camera.RotateTowardsLockOnTarget();
//            //}

//            _camera.SetCameraPivotRotation();

//            _camera.ProcessDesiredDepth();

//            //_camera.transform.localPosition = new Vector3(_camera.transform.localPosition.x, _camera.transform.localPosition.y,
//            //    _currentDepth);
//            //_camera.transform.localPosition = Vector3.Slerp(_camera.transform.localPosition,
//            //    new Vector3(_camera.transform.localPosition.x, _camera.transform.localPosition.y,
//            //    _currentDepth), 5f);

//            _camera.YawClamp();
//        }
//    }
//}
