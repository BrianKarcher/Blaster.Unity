using BlueOrb.Common.Container;
using BlueOrb.Controller.Camera;
using BlueOrb.Physics;

namespace BlueOrb.Scripts.AI.AtomActions
{
    public class NormalCameraAtom : AtomActionBase
    {
        private ThirdPersonCameraController _camera;

        public override void Start(IEntity entity)
        {
            base.Start(entity);
            if (_camera == null)
                _camera = entity.Components.GetComponent<ThirdPersonCameraController>();
        }

        public void OnLateUpdate()
        {
            if (_camera.Player == null)
                return;
            if (_camera.Target == null)
                return;

            var playerRotationInput = _camera.GetPlayerRotationInput();

            // The Camera Distance is used to calculate the position, vertical pivot, and desired depth.
            _camera.CalculateCameraDistance(playerRotationInput);

            _camera.ProcessPivotPointToTargetPosition();

            _camera.ProcessRotateCameraBasedOnTargetMovement();

            _camera.IncrementYaw(playerRotationInput.x);

            _camera.SetCameraPivotRotation();

            _camera.ProcessDesiredDepth();

            //_camera.transform.localPosition = new Vector3(_camera.transform.localPosition.x, _camera.transform.localPosition.y,
            //    _currentDepth);
            //_camera.transform.localPosition = Vector3.Slerp(_camera.transform.localPosition,
            //    new Vector3(_camera.transform.localPosition.x, _camera.transform.localPosition.y,
            //    _currentDepth), 5f);

            _camera.YawClamp();
        }
    }
}
