//using BlueOrb.Common.Container;
//using BlueOrb.Controller;
//using BlueOrb.Controller.Player;
//using BlueOrb.Messaging;
//using BlueOrb.Physics;
//using System.Runtime.CompilerServices;
//using UnityEngine;

//namespace BlueOrb.Scripts.AI.AtomActions
//{
//    public class ThirdPersonMovementAtom : AtomActionBase
//    {
//        private ThirdPersonUserControl _thirdPersonUserControl;
//        private PlayerController _playerController;
//        private Vector3 _axisInput;
//        private float _forwardAmount;
//        private float _turnAmount;
//        private PhysicsComponent _physicsComponent;
//        private AnimationComponent _animationComponent;
//        //private ThirdPersonMovementLogic _thirdPersonLogic;

//        public override void Start(IEntity entity)
//        {
//            base.Start(entity);
//            if (_playerController == null)
//            {
//                _playerController = entity.Components.GetComponent<PlayerController>();
//            }
//            //if (_thirdPersonLogic == null)
//            //{
                
//            //    _thirdPersonLogic = new ThirdPersonMovementLogic(playerController);
//            //}
//            if (_thirdPersonUserControl == null)
//                _thirdPersonUserControl = entity.Components.GetComponent<ThirdPersonUserControl>();
//            if (_physicsComponent == null)
//                _physicsComponent = entity.Components.GetComponent<PhysicsComponent>();
//            if (_animationComponent == null)
//                _animationComponent = entity.Components.GetComponent<AnimationComponent>();
//            //_playerController.SetMovementActive(true);
//            _playerController.SetSkillMovementLogic(PlayerController.MovementLogic.ThirdPerson);
//            //if (_playerController == null)
//            //    _playerController = entity.Components.GetComponent<PlayerController>();
//            //if (_physicsComponent == null)
//            //    _physicsComponent = entity.Components.GetComponent<PhysicsComponent>();
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

//        private void ProcessInput(MovementInputData inputData)
//        {
//            // convert the world relative moveInput vector into a local-relative
//            // turn amount and forward amount required to head in the desired
//            // direction.
//            if (inputData.DirectionalInput_CameraRelative.magnitude > 1f) inputData.DirectionalInput_CameraRelative.Normalize();

//            _axisInput = inputData.DirectionalInput_CameraRelative;
//            if (_axisInput.sqrMagnitude > 0.1f)
//            {
//                int i = 1;
//            }
//            var forwardAndTurnAmount = _physicsComponent.CalculateForwardMovementAndTurnAmount(_axisInput);
//            // Can't rotate while airborn
//            if (_physicsComponent.Controller.GetIsGrounded())
//                _physicsComponent.ApplyRotation(forwardAndTurnAmount.forwardAmount, forwardAndTurnAmount.turnAmount);
//            _forwardAmount = forwardAndTurnAmount.forwardAmount;
//            _turnAmount = forwardAndTurnAmount.turnAmount;

//            //(_forwardAmount, null, _turnAmount) = _physicsComponent.CalculateForwardMovementAndTurnAmount(_axisInput);
//            //_playerController.UpdateAnimator(_forwardAmount, _turnAmount, 0f);
//            _animationComponent.SetForwardSpeed(_forwardAmount);
//            _animationComponent.SetTurnSpeed(_turnAmount);
//            _animationComponent.SetSideSpeed(0f);
//            if (_physicsComponent.Controller.GetIsGrounded())
//                _animationComponent.SetVerticalSpeed(0f);
//            else
//                _animationComponent.SetVerticalSpeed(_physicsComponent.GetVelocity3().y);

//            var targetVelocity = _axisInput * _forwardAmount * _physicsComponent.GetPhysicsData().MaxSpeed;
//            _playerController.SetTargetVelocity(targetVelocity);

//            if (inputData.JumpPressed)
//            {
//                MessageDispatcher.Instance.DispatchMsg("Jump", 0f, _playerController.GetComponentRepository().GetId(),
//                    _playerController.GetComponentRepository().GetId(), null);
//                //_playerController.ProcessJump();
//            }
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
//        }

//        //public override void OnUpdate()
//        //{
//        //    base.OnUpdate();
//        //    var movementData = _thirdPersonUserControl.GetInputData();
//        //    _thirdPersonLogic.ProcessInput(movementData);
//        //    //ProcessInput(movementData);
//        //}

//        //public void ProcessInput(MovementInputData inputData)
//        //{
//        //    // convert the world relative moveInput vector into a local-relative
//        //    // turn amount and forward amount required to head in the desired
//        //    // direction.
//        //    if (inputData.Move.magnitude > 1f) inputData.Move.Normalize();

//        //    _axisInput = inputData.Move;
//        //    if (_axisInput.sqrMagnitude > 0.1f)
//        //    {
//        //        int i = 1;
//        //    }

//        //    var localMove = _playerController.transform.InverseTransformDirection(inputData.Move);
//        //    localMove = Vector3.ProjectOnPlane(localMove, _physicsComponent.Controller.GroundNormal);
//        //    _turnAmount = Mathf.Atan2(localMove.x, localMove.z);
//        //    _forwardAmount = localMove.z;

//        //    ApplyRotation();
//        //    _playerController.UpdateAnimator(_forwardAmount, _turnAmount);

//        //    var targetVelocity = _axisInput * _forwardAmount * _physicsComponent.GetPhysicsData().MaxSpeed;
//        //    _playerController.SetTargetVelocity(targetVelocity);

//        //    if (inputData.JumpPressed && _physicsComponent.Controller.GetIsGrounded())
//        //    {
//        //        _playerController.ProcessJump();
//        //    }
//        //    if (inputData.TurnCrouchOn)
//        //        _playerController.SetCrouch(true);
//        //    if (inputData.TurnCrouchOff)
//        //        _playerController.SetCrouch(false);

//        //}

//        //private void ApplyRotation()
//        //{
//        //    float turnSpeed = Mathf.Lerp(_playerController.StationaryTurnSpeed, _playerController.MovingTurnSpeed, _forwardAmount);
//        //    _playerController.transform.Rotate(0, _turnAmount * turnSpeed * Time.deltaTime, 0);
//        //}
//    }
//}
