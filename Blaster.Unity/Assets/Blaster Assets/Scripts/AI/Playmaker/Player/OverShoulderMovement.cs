using HutongGames.PlayMaker;
using BlueOrb.Common.Container;
using UnityEngine;
using BlueOrb.Physics;
using BlueOrb.Messaging;
using BlueOrb.Controller.Player;
using BlueOrb.Controller;
using BlueOrb.Scripts.AI.Playmaker;

namespace BlueOrb.Scripts.AI.PlayMaker.Attack
{
    [ActionCategory("BlueOrb.Player")]
    public class OverShoulderMovement : BasePlayMakerAction
    {
        [RequiredField]
        public FsmOwnerDefault gameObject;
        public string InputMessage;
        private Animator _animator;
        private IPhysicsComponent _physicsComponent;
        private AnimationComponent _animationComponent;
        private long _inputMessageIndex;
        private IEntity _entity;

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

            _entity = base.GetEntityBase(go);
            if (_animator == null)
                _animator = _entity.transform.GetComponent<Animator>();
            if (_physicsComponent == null)
                _physicsComponent = _entity.Components.GetComponent<IPhysicsComponent>();
            if (_animationComponent == null)
                _animationComponent = _entity.Components.GetComponent<AnimationComponent>();
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

            var forwardAmount = axisInput.y;
            var _sideAmount = axisInput.x;
            float _turnAmount = 0f;

            _animationComponent?.SetForwardSpeed(forwardAmount);
            _animationComponent?.SetTurnSpeed(_turnAmount);
            _animationComponent?.SetSideSpeed(_sideAmount);
            if (_physicsComponent.Controller.GetIsGrounded())
                _animationComponent?.SetVerticalSpeed(0f);
            else
                _animationComponent?.SetVerticalSpeed(_physicsComponent.GetVelocity3().y);

            var playerRelativeInputDirection = _entity.transform.TransformDirection(new Vector3(axisInput.x, 0, axisInput.y));
            _physicsComponent.Move(playerRelativeInputDirection.normalized * _physicsComponent.GetPhysicsData().MaxSpeed);
        }

        private void StopListening(IEntity entity)
        {
            MessageDispatcher.Instance.StopListening(InputMessage, entity.GetId(), _inputMessageIndex);
        }

        public override void OnExit()
        {
            base.OnExit();
            StopListening(_entity);
        }
    }
}
