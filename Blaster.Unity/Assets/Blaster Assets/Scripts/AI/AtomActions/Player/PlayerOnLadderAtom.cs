using Rewired;
using BlueOrb.Common.Container;
using BlueOrb.Controller.Ladder;
using BlueOrb.Controller.Player;
using BlueOrb.Physics;
using UnityEngine;

namespace BlueOrb.Scripts.AI.AtomActions.Player
{
    public class PlayerOnLadderAtom : AtomActionBase
    {
        public string _axisAction;
        public float _climbSpeed;
        public string _verticalSpeedAnim;
        public string _forwardSpeedAnim;
        public float _distanceBuffer;

        private Rewired.Player _rewiredPlayer;
        private UserLadderComponent _userLadderComponent;
        private LadderComponent _ladderComponent;
        private Animator _animator;
        private PhysicsComponent _physicsComponent;
        private ThirdPersonUserControl _thirdPersonUserControl;
        private Controller.Player.PlayerController _playerController;
        private bool _reachedTop;
        private bool _reachedBottom;
        //private Collider collider;

        public override void Start(IEntity entity)
        {
            base.Start(entity);
            _rewiredPlayer = ReInput.players.GetPlayer(0);
            if (_userLadderComponent == null)
                _userLadderComponent = entity.Components.GetComponent<UserLadderComponent>();
            if (_animator == null)
                _animator = entity.GetComponent<Animator>();
            if (_physicsComponent == null)
                _physicsComponent = entity.Components.GetComponent<PhysicsComponent>();
            if (_thirdPersonUserControl == null)
                _thirdPersonUserControl = entity.Components.GetComponent<ThirdPersonUserControl>();
            if (_playerController == null)
                _playerController = entity.Components.GetComponent<Controller.Player.PlayerController>();
            //if (collider == null)
            //    collider = entity.GetComponents<Collider>().FirstOrDefault();

            _reachedTop = false;
            _reachedBottom = false;
            // Go backwards a bit from the ladder
            _ladderComponent = _userLadderComponent.GetLadderComponent();
            // Place the player a little behind the ladder
            var offset = _ladderComponent.transform.TransformDirection(new Vector3(0f, 0f, -0.5f));
            _entity.transform.position = new Vector3(_ladderComponent.transform.position.x, _entity.transform.position.y,
                _ladderComponent.transform.position.z) + offset; 
            // Face in the direction of the ladder
            _entity.transform.rotation = _ladderComponent.transform.rotation;
            _physicsComponent.Controller.GetPhysicsData().ApplyGravity = false;
            //_physicsComponent.Controller.GetOriginalPhysicsData().ApplyGravity = false;
            _thirdPersonUserControl.SetEnablePlayerInput(false);
            _playerController.SetMovementActive(false);
            _animator.SetFloat(_forwardSpeedAnim, 0f);
            ClampEntityOnLadder();
            // Make sure the collider does not interfere with the ladder climbing
            _physicsComponent.EnableCollider(false);
            _physicsComponent.Stop();
        }

        private void ClampEntityOnLadder()
        {
            if (_entity.GetHeadPosition().y > _ladderComponent.GetTopOfLadderHeight() - _distanceBuffer)
            {
                var y = _ladderComponent.GetTopOfLadderHeight() - _distanceBuffer - _entity.Height - 0.01f;
                _entity.transform.position = new Vector3(_entity.transform.position.x, y, _entity.transform.position.z);
            }
            if (_entity.GetFootPosition().y < _ladderComponent.GetBottomOfLadderHeight() + _distanceBuffer)
            {
                var y = _ladderComponent.GetBottomOfLadderHeight() + _distanceBuffer + 0.01f;
                _entity.transform.position = new Vector3(_entity.transform.position.x, y, _entity.transform.position.z);
            }
        }

        public override void End()
        {
            base.End();
            _physicsComponent.Controller.GetPhysicsData().ApplyGravity = true;
            //_physicsComponent.Controller.GetOriginalPhysicsData().ApplyGravity = true;
            _thirdPersonUserControl.SetEnablePlayerInput(true);
            _playerController.SetMovementActive(true);
            if (_reachedTop)
            {
                _entity.transform.position = _ladderComponent.TopOfLadder.transform.position;
            }
            else if (_reachedBottom)
            {
                // Place the player a little bit away from the ladder
                _entity.transform.position = _ladderComponent.transform.TransformPoint(new Vector3(0f, 0f, -0.5f));
            }
            _physicsComponent.EnableCollider(true);
        }

        public override void OnUpdate()
        {
            base.OnUpdate();
            float axis = _rewiredPlayer.GetAxis(_axisAction);
            float verticalSpeed = axis * _climbSpeed * Time.deltaTime;
            _entity.transform.Translate(new Vector3(0f, verticalSpeed, 0f));
            _animator.SetFloat(_verticalSpeedAnim, verticalSpeed);
            if (axis > 0f)
                MoveUp();
            if (axis < 0f)
                MoveDown();
        }

        private void MoveUp()
        {
            var distanceFromHeadToTopOfLadder = _ladderComponent.GetTopOfLadderHeight() - _entity.GetHeadPosition().y;
            if (Mathf.Abs(distanceFromHeadToTopOfLadder) < _distanceBuffer)
            {
                _reachedTop = true;
                Finish();
            }
        }

        private void MoveDown()
        {
            var distanceFromFootToBottomOfLadder = _ladderComponent.GetBottomOfLadderHeight() - _entity.GetFootPosition().y;
            if (Mathf.Abs(distanceFromFootToBottomOfLadder) < _distanceBuffer)
            {
                _reachedBottom = true;
                Finish();
            }
        }
    }
}
