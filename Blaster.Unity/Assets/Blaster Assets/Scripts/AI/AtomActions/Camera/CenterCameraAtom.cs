//using BlueOrb.Common.Container;
//using BlueOrb.Controller.Camera;
//using BlueOrb.Messaging;
//using BlueOrb.Physics;
//using UnityEngine;

//namespace BlueOrb.Scripts.AI.AtomActions
//{
//    public class CenterCameraAtom : AtomActionBase
//    {
//        private ThirdPersonCameraController _camera;
//        private IEntity _mainCharacter;

//        public override void Start(IEntity entity)
//        {
//            base.Start(entity);
//            if (_mainCharacter == null)
//                _mainCharacter = EntityContainer.Instance.GetMainCharacter();
//            if (_camera == null)
//                _camera = entity.Components.GetComponent<ThirdPersonCameraController>();
//            MessageDispatcher.Instance.DispatchMsg("EnableInput", 0f, null, _mainCharacter.GetId(), "0");
//        }

//        public override void End()
//        {
//            base.End();
//            MessageDispatcher.Instance.DispatchMsg("EnableInput", 0f, null, _mainCharacter.GetId(), "1");
//        }

//        public void OnLateUpdate()
//        {
//            if (_camera.Player == null)
//                return;
//            if (_camera.Target == null)
//                return;

//            _camera.ProcessRotateCameraBehindTarget();
//            _camera.SetCameraPivotRotation();
//            _camera.ProcessDesiredDepth();

//            float deltaAngle = Mathf.DeltaAngle(_camera.Target.transform.rotation.eulerAngles.y, _camera.GetYaw());
//            float cameraDistance = _camera.StartingCameraDistance - _camera.CameraDistance;

//            if (Mathf.Abs(deltaAngle) < 0.1f && Mathf.Abs(cameraDistance) < 0.01f)
//                Finish();

//            _camera.YawClamp();
//        }
//    }
//}
