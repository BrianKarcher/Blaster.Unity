//using BlueOrb.Base.Extensions;
//using BlueOrb.Common.Container;
//using BlueOrb.Controller;
//using BlueOrb.Controller.Damage;
//using BlueOrb.Controller.Player;
//using BlueOrb.Messaging;
//using BlueOrb.Physics;

//namespace BlueOrb.Scripts.AI.AtomActions
//{
//    public class LockedOnMovementAtom : AtomActionBase
//    {
//        private ThirdPersonUserControl _thirdPersonUserControl;
//        private PlayerController _playerController;
//        private PhysicsComponent _physicsComponent;
//        private AnimationComponent _animationComponent;
//        private EntityStatsComponent _entityStatsComponent;
//        //private float _turnAmount;
//        //private float _forwardAmount;
//        //private LockOnMovementLogic _lockOnMovementLogic;

//        public override void Start(IEntity entity)
//        {
//            base.Start(entity);
//            if (_playerController == null)
//            {
//                _playerController = entity.Components.GetComponent<PlayerController>();
//            }

//            //if (_lockOnMovementLogic == null)
//            //{
//            //    var playerController = entity.Components.GetComponent<PlayerController>();
//            //    _lockOnMovementLogic = new LockOnMovementLogic(playerController);
//            //}
//            if (_physicsComponent == null)
//                _physicsComponent = entity.Components.GetComponent<PhysicsComponent>();
//            if (_animationComponent == null)
//                _animationComponent = entity.Components.GetComponent<AnimationComponent>();
//            if (_thirdPersonUserControl == null)
//                _thirdPersonUserControl = entity.Components.GetComponent<ThirdPersonUserControl>();
//            if (_entityStatsComponent == null)
//                _entityStatsComponent = entity.Components.GetComponent<EntityStatsComponent>();
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

//            var _forwardAmount = axisInput.y;
//            var sideAmount = axisInput.x;

//            //_playerController.UpdateAnimator(_forwardAmount, _turnAmount, _sideAmount);
//            float _turnAmount = 0;
//            _animationComponent.SetForwardSpeed(_forwardAmount);
//            _animationComponent.SetTurnSpeed(_turnAmount);
//            _animationComponent.SetSideSpeed(sideAmount);
//            if (_physicsComponent.Controller.GetIsGrounded())
//                _animationComponent.SetVerticalSpeed(0f);
//            else
//                _animationComponent.SetVerticalSpeed(_physicsComponent.GetVelocity3().y);

//            var playerRelativeInputDirection = _playerController.transform.TransformDirection(axisInput.xz());

//            _playerController.SetTargetVelocity(playerRelativeInputDirection * _physicsComponent.GetPhysicsData().MaxSpeed);

//            SendInputMessage(inputData, sideAmount, _forwardAmount);

//            if (inputData.TurnCrouchOn)
//                _playerController.SetCrouch(true);
//            if (inputData.TurnCrouchOff)
//                _playerController.SetCrouch(false);

//        }

//        private void SendInputMessage(MovementInputData inputData, float sideAmount, float forwardAmount)
//        {


//            if (inputData.JumpPressed && _physicsComponent.Controller.GetIsGrounded())
//            {
//                if (sideAmount > 0.75f)
//                {
//                    PressingJumpRight();
//                    return;
//                }
//                if (sideAmount < -0.75f)
//                {
//                    PressingJumpLeft();
//                    return;
//                }
//                if (forwardAmount > 0.75f)
//                {
//                    PressingJumpForward();
//                    return;
//                }
//                if (forwardAmount < -0.75f)
//                {
//                    PressingJumpBackward();
//                    return;
//                }

//                _playerController.ProcessJump();
//                return;
//            }
//        }

//        private void PressingJumpRight()
//        {
//            //if (_playerParryComponent.CanParry)
//            //{
//            //    MessageDispatcher.Instance.DispatchMsg("DodgeRight", 0f, _playerController.GetComponentRepository().GetId(), _playerController.GetComponentRepository().GetId(), null);
//            //    return;
//            //}

//            if (CheckStamina("Sidehop"))
//            {
//                MessageDispatcher.Instance.DispatchMsg("SidehopRight", 0f, _playerController.GetComponentRepository().GetId(), _playerController.GetComponentRepository().GetId(), null);
//                return;
//            }
//        }

//        private void PressingJumpLeft()
//        {
//            //if (_playerParryComponent.CanParry)
//            //{
//            //    MessageDispatcher.Instance.DispatchMsg("DodgeLeft", 0f, _playerController.GetComponentRepository().GetId(), _playerController.GetComponentRepository().GetId(), null);
//            //    return;
//            //}

//            if (CheckStamina("Sidehop"))
//            {
//                MessageDispatcher.Instance.DispatchMsg("SidehopLeft", 0f, _playerController.GetComponentRepository().GetId(), _playerController.GetComponentRepository().GetId(), null);
//                return;
//            }
//        }

//        private void PressingJumpForward()
//        {
//            //if (_playerParryComponent.CanParry)
//            //{
//            //    MessageDispatcher.Instance.DispatchMsg("ParryLeft", 0f, _playerController.GetComponentRepository().GetId(), _playerController.GetComponentRepository().GetId(), null);
//            //    return;
//            //}

//            if (CheckStamina("Backflip"))
//            {
//                MessageDispatcher.Instance.DispatchMsg("Forwardhop", 0f, _playerController.GetComponentRepository().GetId(), _playerController.GetComponentRepository().GetId(), null);
//                return;
//            }
//        }

//        private void PressingJumpBackward()
//        {
//            //if (_playerParryComponent.CanParry)
//            //{
//            //    MessageDispatcher.Instance.DispatchMsg("DodgeBack", 0f, _playerController.GetComponentRepository().GetId(), _playerController.GetComponentRepository().GetId(), null);
//            //    return;
//            //}

//            if (CheckStamina("Backflip"))
//            {
//                MessageDispatcher.Instance.DispatchMsg("Backflip", 0f, _playerController.GetComponentRepository().GetId(), _playerController.GetComponentRepository().GetId(), null);
//                return;
//            }
//        }

//        private bool CheckStamina(string name)
//        {
//            return _entityStatsComponent.CheckStamina(_entityStatsComponent.GetActionStamina(name));
//        }

//        public void LateUpdate()
//        {
//            _playerController.LookAtTarget();
//        }
//    }
//}
