using Rewired;
using BlueOrb.Common.Container;
using BlueOrb.Controller.Camera;
using BlueOrb.Physics;
using UnityEngine;

namespace BlueOrb.Scripts.AI.AtomActions
{
    public class StrafeCameraAtom : AtomActionBase
    {
        private ThirdPersonCameraController _camera;
        public string LockCameraAction;
        private Rewired.Player _player;

        public override void Start(IEntity entity)
        {
            base.Start(entity);
            if (_camera == null)
                _camera = entity.Components.GetComponent<ThirdPersonCameraController>();
            Debug.Log("(LockedOnCameraAtom) Start called.");
            if (_player == null)
                _player = ReInput.players.GetPlayer(0);

            var targetLockOffset = _camera.GetLockOnOffset();
            _camera.SetTargetLocalPosOffset(targetLockOffset);
        }

        public void OnLateUpdate()
        {
            if (_camera.Player == null || _camera.Target == null)
                return;

            var playerRotationInput = _camera.GetPlayerRotationInput();

            //if (!_camera.IsLockOnTargetAlive())
            //{
            //    Finish();
            //    return;
            //}

            //if (_player.GetButtonUp(LockCameraAction))
            //{
            //    Finish();
            //    return;
            //}                

            // The Camera Distance is used to calculate the position, vertical pivot, and desired depth.
            _camera.CalculateCameraDistance(playerRotationInput);

            _camera.ProcessPivotPointToTargetPosition();

            _camera.SetCameraPivotRotation();

            _camera.ProcessDesiredDepth();

            //_camera.transform.localPosition = new Vector3(_camera.transform.localPosition.x, _camera.transform.localPosition.y,
            //    _currentDepth);
            //_camera.transform.localPosition = Vector3.Slerp(_camera.transform.localPosition,
            //    new Vector3(_camera.transform.localPosition.x, _camera.transform.localPosition.y,
            //    _currentDepth), 5f);

            _camera.YawClamp();
        }

        public override void End()
        {
            base.End();
            Debug.Log("(LockedOnCameraAtom) End called.");
            //_camera.EnableLockOn(false);
            _camera.SetTargetLocalPosOffset(Vector3.zero);
        }
    }
}
