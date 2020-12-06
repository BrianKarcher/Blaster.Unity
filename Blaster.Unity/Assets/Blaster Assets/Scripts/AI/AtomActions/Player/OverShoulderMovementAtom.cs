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
//        private Animator _animator;
//        private PhysicsComponent _physicsComponent;
//        private AnimationComponent _animationComponent;
//        //private ThirdPersonCameraController _camera;
//        //private PlayerController _playerController;
//        //private Vector3 _axisInput;
//        //private float _forwardAmount;
//        //private float _turnAmount;
//        //private PhysicsComponent _physicsComponent;
//        //protected Animator _animator;

//        //private OverShoulderMovementLogic _overShoulderLogic;

//        public override void Start(IEntity entity)
//        {
//            base.Start(entity);
//            if (_playerController == null)            
//                _playerController = entity.Components.GetComponent<PlayerController>();
            
//            if (_animator == null)         
//                _animator = entity.transform.GetComponent<Animator>();
            
//            //if (_overShoulderLogic == null)
//            //{
//            //    _overShoulderLogic = new OverShoulderMovementLogic(_playerController);
//            //}
//            if (_thirdPersonUserControl == null)
//                _thirdPersonUserControl = entity.Components.GetComponent<ThirdPersonUserControl>();
//            if (_physicsComponent == null)
//                _physicsComponent = entity.Components.GetComponent<PhysicsComponent>();
//            if (_animationComponent == null)
//                _animationComponent = entity.Components.GetComponent<AnimationComponent>();
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

//        public void ProcessInput(MovementInputData inputData)
//        {
//            if (inputData.DirectionalInput_Raw.magnitude > 1f) inputData.DirectionalInput_Raw.Normalize();

//            var axisInput = inputData.DirectionalInput_Raw;
//            if (axisInput.sqrMagnitude > 0.1f)
//            {
//                int i = 1;
//            }

//            // _forwardAmount = axisInput.magnitude;
//            var forwardAmount = axisInput.y;
//            var _sideAmount = axisInput.x;
//            float _turnAmount = 0f;

//            //_playerController.UpdateAnimator(_forwardAmount, _turnAmount, _sideAmount);
//            _animationComponent.SetForwardSpeed(forwardAmount);
//            _animationComponent.SetTurnSpeed(_turnAmount);
//            _animationComponent.SetSideSpeed(_sideAmount);
//            if (_physicsComponent.Controller.GetIsGrounded())
//                _animationComponent.SetVerticalSpeed(0f);
//            else
//                _animationComponent.SetVerticalSpeed(_physicsComponent.GetVelocity3().y);

//            var playerRelativeInputDirection = _playerController.transform.TransformDirection(axisInput.xz());
//            _playerController.SetTargetVelocity(playerRelativeInputDirection * _physicsComponent.GetPhysicsData().MaxSpeed);
//            if (inputData.TurnCrouchOn)
//                _playerController.SetCrouch(true);
//            if (inputData.TurnCrouchOff)
//                _playerController.SetCrouch(false);

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
