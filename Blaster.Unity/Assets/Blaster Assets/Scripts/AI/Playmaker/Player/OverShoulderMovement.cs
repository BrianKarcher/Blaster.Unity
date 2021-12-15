using BlueOrb.Scripts.AI.AtomActions;
using HutongGames.PlayMaker;
using BlueOrb.Common.Container;
using UnityEngine;
using BlueOrb.Physics;
using BlueOrb.Messaging;
using BlueOrb.Controller.Player;
using BlueOrb.Controller;

namespace BlueOrb.Scripts.AI.PlayMaker.Attack
{
    [ActionCategory("BlueOrb.Player")]
    //[Tooltip("Returns Success if Button is pressed.")]
    public class OverShoulderMovement : FsmStateAction
    {
        [RequiredField]
        public FsmOwnerDefault gameObject;
        public string InputMessage;
        private Animator _animator;
        private PhysicsComponent _physicsComponent;
        private AnimationComponent _animationComponent;
        private long _inputMessageIndex;
        private IEntity _entity;

        //        public OverShoulderMovementAtom _atom;

        //        //[HutongGames.PlayMaker.Tooltip("Event to send if the trigger event is detected.")]
        //        //public FsmEvent HangEvent;

        public override void Reset()
        {
            gameObject = null;
        }

        public override void OnEnter()
        {
            var go = Fsm.GetOwnerDefaultTarget(gameObject);
            if (go == null)
            {
                return;
            }
            _entity = go.GetComponent<IEntity>();
            if (_animator == null)
                _animator = _entity.transform.GetComponent<Animator>();
            if (_physicsComponent == null)
                _physicsComponent = _entity.Components.GetComponent<PhysicsComponent>();
            if (_animationComponent == null)
                _animationComponent = _entity.Components.GetComponent<AnimationComponent>();
            //_atom.Start(entity);
            StartListening();
        }

        public void StartListening()
        {
            _inputMessageIndex = MessageDispatcher.Instance.StartListening(InputMessage, _entity.GetId(), (data) =>
            {
                var inputData = (MovementInputData)data.ExtraInfo;
                ProcessInput(inputData);
            });
        }

        public void ProcessInput(MovementInputData inputData)
        {
            if (inputData.DirectionalInput_Raw.magnitude > 1f) inputData.DirectionalInput_Raw.Normalize();

            var axisInput = inputData.DirectionalInput_Raw;
            if (axisInput.sqrMagnitude > 0.1f)
            {
                int i = 1;
            }

            // _forwardAmount = axisInput.magnitude;
            var forwardAmount = axisInput.y;
            var _sideAmount = axisInput.x;
            float _turnAmount = 0f;

            //_playerController.UpdateAnimator(_forwardAmount, _turnAmount, _sideAmount);
            _animationComponent?.SetForwardSpeed(forwardAmount);
            _animationComponent?.SetTurnSpeed(_turnAmount);
            _animationComponent?.SetSideSpeed(_sideAmount);
            if (_physicsComponent.Controller.GetIsGrounded())
                _animationComponent?.SetVerticalSpeed(0f);
            else
                _animationComponent?.SetVerticalSpeed(_physicsComponent.GetVelocity3().y);

            //var playerRelativeInputDirection = _playerController.transform.TransformDirection(axisInput.xz());
            var playerRelativeInputDirection = _entity.transform.TransformDirection(new Vector3(axisInput.x, 0, axisInput.y));
            _physicsComponent.SetVelocity3(playerRelativeInputDirection.normalized * _physicsComponent.GetPhysicsData().MaxSpeed);
            //_playerController.SetTargetVelocity(playerRelativeInputDirection * _physicsComponent.GetPhysicsData().MaxSpeed);
            //if (inputData.TurnCrouchOn)
            //    _playerController.SetCrouch(true);
            //if (inputData.TurnCrouchOff)
            //    _playerController.SetCrouch(false);

        }

        private void StopListening(IEntity entity)
        {
            MessageDispatcher.Instance.StopListening(InputMessage, entity.GetId(), _inputMessageIndex);
        }

        public override void OnExit()
        {
            base.OnExit();
            //_atom.End();
            StopListening(_entity);
        }

        //        public override void OnUpdate()
        //        {
        //            Tick();
        //        }

        //        private void Tick()
        //        {
        //            _atom.OnUpdate();
        //            if (_atom.IsFinished)
        //                Finish();
        //        }
    }
}
