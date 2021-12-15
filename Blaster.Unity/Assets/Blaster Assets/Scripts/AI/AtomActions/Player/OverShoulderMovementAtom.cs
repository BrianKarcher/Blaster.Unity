//using BlueOrb.Base.Extensions;
//using BlueOrb.Common.Container;
//using BlueOrb.Controller;
//using BlueOrb.Controller.Camera;
//using BlueOrb.Controller.Player;
//using BlueOrb.Physics;
//using UnityEngine;

//namespace BlueOrb.Scripts.AI.AtomActions
//{
//    public class OverShoulderMovementAtom : AtomActionBase
//    {
//        private ThirdPersonUserControl _thirdPersonUserControl;
//        private PlayerController _playerController;

//        //private ThirdPersonCameraController _camera;
//        //private PlayerController _playerController;
//        //private Vector3 _axisInput;
//        //private float _forwardAmount;
//        //private float _turnAmount;
//        //private PhysicsComponent _physicsComponent;

//        //private OverShoulderMovementLogic _overShoulderLogic;

//        public override void Start(IEntity entity)
//        {
//            base.Start(entity);
//            if (_playerController == null)            
//                _playerController = entity.Components.GetComponent<PlayerController>();

//            //if (_overShoulderLogic == null)
//            //{
//            //    _overShoulderLogic = new OverShoulderMovementLogic(_playerController);
//            //}
//            if (_thirdPersonUserControl == null)
//                _thirdPersonUserControl = entity.Components.GetComponent<ThirdPersonUserControl>();


//            //_playerController.SetMovementActive(true);
//            _playerController.SetSkillMovementLogic(PlayerController.MovementLogic.OverShoulder);
//            _animator.SetBool(_playerController.StrafeAnim, true);
//            //if (_playerController == null)
//            //    _playerController = entity.Components.GetComponent<PlayerController>();
//            //if (_physicsComponent == null)
//            //    _physicsComponent = entity.Components.GetComponent<PhysicsComponent>();
//            //if (_camera == null)
//            //    _camera = UnityEngine.Camera.main.transform.parent.GetComponent<ThirdPersonCameraController>();
//            //if (_animator == null)
//            //    _animator = entity.GetComponent<Animator>();
//        }

//        public override void OnUpdate()
//        {
//            base.OnUpdate();
//            if (!_playerController.GetMovementActive())
//            {
//                _animationComponent.SetForwardSpeed(0f);
//                _animationComponent.SetTurnSpeed(0f);
//                _animationComponent.SetSideSpeed(0f);
//                return;
//            }

//            var _movementData = _thirdPersonUserControl.GetInputData();
//            ProcessInput(_movementData);
//        }

//        public override void End()
//        {
//            base.End();
//            //_playerController.SetMovementActive(false);
//            _playerController.SetSkillMovementLogic(PlayerController.MovementLogic.None);
//            _animator.SetBool(_playerController.StrafeAnim, false);
//        }

//        //public override void OnUpdate()
//        //{
//        //    base.OnUpdate();

//        //    var movementData = _thirdPersonUserControl.GetInputData();
//        //    _overShoulderLogic.ProcessInput(movementData);
//        //    //ProcessInput(movementData);
//        //}

//        //public void ProcessInput(MovementInputData inputData)
//        //{
//        //    if (inputData.Move.magnitude > 1f) inputData.Move.Normalize();

//        //    var axisInput = inputData.Move;
//        //    if (axisInput.sqrMagnitude > 0.1f)
//        //    {
//        //        int i = 1;
//        //    }

//        //    _forwardAmount = axisInput.magnitude;

//        //    _playerController.UpdateAnimator(_forwardAmount, _turnAmount);

//        //    _playerController.SetTargetVelocity(axisInput * _physicsComponent.GetPhysicsData().MaxSpeed);
//        //    if (inputData.TurnCrouchOn)
//        //        _playerController.SetCrouch(true);
//        //    if (inputData.TurnCrouchOff)
//        //        _playerController.SetCrouch(false);
//        //}
//    }
//}
