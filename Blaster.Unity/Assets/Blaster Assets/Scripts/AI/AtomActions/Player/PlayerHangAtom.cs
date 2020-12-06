//using System;
//using Rewired;
//using BlueOrb.Common.Container;
//using BlueOrb.Controller.Camera;
//using BlueOrb.Controller.Physics;
//using BlueOrb.Controller.Player;
//using BlueOrb.Messaging;
//using BlueOrb.Physics;
//using UnityEngine;

//namespace BlueOrb.Scripts.AI.AtomActions
//{
//    public class PlayerHangAtom : AtomActionBase
//    {
//        public float deadZone = 0.5f;
//        private Action _pressedUp;
//        private Action _pressedDown;

//        private ThirdPersonUserControl _thirdPersonUserControl;
//        private Controller.Player.PlayerController _playerController;
//        private PhysicsComponent _physicsComponent;
//        private Rewired.Player _player;
//        private bool _readyForInput = false;
//        //private GameObject _hangCollider;
//        //private HangTriggerComponent _hangTriggerComponent;
//        private ThirdPersonCameraController _camera;

//        public override void Start(IEntity entity)
//        {
//            base.Start(entity);
//            if (_thirdPersonUserControl == null)
//                _thirdPersonUserControl = entity.Components.GetComponent<ThirdPersonUserControl>();
//            if (_playerController == null)
//                _playerController = entity.Components.GetComponent<Controller.Player.PlayerController>();
//            if (_physicsComponent == null)
//                _physicsComponent = entity.Components.GetComponent<PhysicsComponent>();
//            _thirdPersonUserControl.SetEnablePlayerInput(false);

//            _physicsComponent.SetEnabled(false);
//            _physicsComponent.GetPhysicsData().ApplyGravity = false;
//            //GameObject.Destroy(_playerController.gameObject);
//            _player = ReInput.players.GetPlayer(0);
//            _readyForInput = false;
//            //_hangTriggerComponent = _hangCollider.GetComponent<HangTriggerComponent>();

//            // Set player position so the hand meets the collider
//            var hangCliffHeight = _playerController.HangCliffHeight;
//            var posDeltaY = hangCliffHeight - _playerController.HandGameObject.transform.position.y;
//            Debug.Log($"(PlayerHangAtom) Moving Player up by {posDeltaY}");
//            _entity.transform.Translate(new Vector3(0f, posDeltaY, 0f));
//            var playerDirection = _playerController.HangCliffNormal * -1f;
//            _playerController.transform.rotation = Quaternion.LookRotation(playerDirection);
//            _playerController.SetMovementActive(false);
//            //_playerController.transform.rotation = Quaternion.Euler(playerDirection.x, playerDirection.y, playerDirection.z);

//            if (_camera == null)
//                _camera = UnityEngine.Camera.main.GetComponentInParent<ThirdPersonCameraController>();
//            MessageDispatcher.Instance.DispatchMsg("CenterCamera", 0f, string.Empty, _camera.GetComponentRepository().GetId(), null);

//            //Debug.Log($"Player Enter Hanging");
//            // Stop the physics system while the player is hanging.
//            _physicsComponent.EnableCollider(false);
//            _physicsComponent.Stop();
//        }

//        public override void OnUpdate()
//        {
//            base.OnUpdate();
//            var vAxis = _player.GetAxis("Vertical");
//            if (!_readyForInput)
//            {
//                if (vAxis > deadZone || vAxis < -deadZone)
//                    return;
//                _readyForInput = true;
//            }
//            if (vAxis < -deadZone)
//            {
//                _pressedDown?.Invoke();
//                //_playerController.ProcessJump();
//                //var playerPos = _hangTriggerComponent.PlayerPlacePosition.transform.position;
//                //Debug.Log($"Setting player position to {playerPos}");
//                //_entity.gameObject.transform.position = playerPos;

//                // Push away from the wall
//                var pushAwayForce = -_entity.transform.forward.normalized;
//                _physicsComponent.AddForce(pushAwayForce, ForceMode.VelocityChange);
//                Finish();
//            }
//            if (vAxis > deadZone)
//            {
//                //_playerController.ProcessJump();
//                //var playerPos = _hangTriggerComponent.PlayerPlacePosition.transform.position;
//                var hangCliffHeight = _playerController.HangCliffHeight;
//                var playerPos = _playerController.transform.position += _playerController.transform.forward * 0.5f;
//                playerPos.y = hangCliffHeight;
//                //var playerPos = _playerController.transform.TransformPoint()
//                Debug.Log($"Setting player position to {playerPos}");
//                _entity.gameObject.transform.position = playerPos;
//                _pressedUp?.Invoke();
//                Finish();
//            }
//        }

//        public override void End()
//        {
//            _thirdPersonUserControl.SetEnablePlayerInput(true);
//            _playerController.SetMovementActive(true);
//            _physicsComponent.SetEnabled(true);
//            _physicsComponent.GetPhysicsData().ApplyGravity = true;
//            _physicsComponent.EnableCollider(true);
//            _playerController.CanHang = false;
//            Debug.Log($"Player Exit Hanging");
//        }

//        //public void SetHangCollider(GameObject hangCollider)
//        //{
//        //    _hangCollider = hangCollider;
//        //}

//        public void SetPressedUp(Action pressedUp)
//        {
//            _pressedUp = pressedUp;
//        }

//        public void SetPressedDown(Action pressedDown)
//        {
//            _pressedDown = pressedDown;
//        }
//    }
//}
