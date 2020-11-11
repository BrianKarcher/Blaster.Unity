using BlueOrb.Scripts.AI.AtomActions;
using BlueOrb.Scripts.AI.AtomActions.Player;
using HutongGames.PlayMaker;
using BlueOrb.Common.Container;
using BlueOrb.Controller.Camera;
using UnityEngine;

namespace BlueOrb.Scripts.AI.PlayMaker.Attack
{
    [ActionCategory("RQ.Shooter")]
    //[Tooltip("Returns Success if Button is pressed.")]
    public class ShooterMain : FsmStateAction
    {
        [RequiredField]
        public FsmOwnerDefault gameObject;
        private ThirdPersonCameraController _camera;
        private Controller.Player.PlayerController _playerController;
        [UIHint(UIHint.Layer)]
        [HutongGames.PlayMaker.Tooltip("Pick only from these layers.")]
        public FsmInt[] raycastLayerMask;

        private int _layerMask = 0;
        private IEntity _entity;
        //private PhysicsComponent;
        //public ShooterShootAtom _atom;

        //[HutongGames.PlayMaker.Tooltip("Event to send if the trigger event is detected.")]
        //public FsmEvent HangEvent;

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
            Camera.main.transform.parent.GetComponent<ThirdPersonCameraController>();
            if (_camera == null)
                _camera = Camera.main.transform.parent.GetComponent<ThirdPersonCameraController>();
            //_camera = _entity.Components.GetComponent<ThirdPersonCameraController>();
            if (_playerController == null)
                _playerController = _entity.Components.GetComponent<BlueOrb.Controller.Player.PlayerController>();
            _playerController.SetSkillMovementLogic(BlueOrb.Controller.Player.PlayerController.MovementLogic.OverShoulder);
            _playerController.CalculateMovementType();
            //_playerController.SetCameraBasedOnMovementType();
            //_playerController = EntityContainer.Instance.GetMainCharacter().Components.GetComponent<RQ.Controller.Player.PlayerController>();
            _layerMask = ActionHelpers.LayerArrayToLayerMask(raycastLayerMask, false);
            //_atom.Start(entity);
        }

        public override void OnExit()
        {
            base.OnExit();
            _playerController.SetSkillMovementLogic(BlueOrb.Controller.Player.PlayerController.MovementLogic.ThirdPerson);
            //_playerController.SetCameraBasedOnMovementType();
            //_atom.End();
        }

        //public override void OnUpdate()
        //{
        //    Tick();
        //}

        public override void OnPreprocess()
        {
            base.OnPreprocess();
            Fsm.HandleLateUpdate = true;
        }

        public override void OnLateUpdate()
        {
            base.OnLateUpdate();
            Vector3 hitPoint;
            if (_camera.AutoLockOntoTarget)
            {
                hitPoint = _entity.Target.GetComponent<IEntity>().GetPosition();
            }
            else if (!_camera.Raycast(1000f, _layerMask, out var hitInfo))
            {
                // If raycast no-hit, just point down the camera forward direction very far
                hitPoint = _camera.transform.TransformPoint(new Vector3(0f, 0f, 1000f));
            }
            else
            {
                hitPoint = hitInfo.point;
            }

            _playerController.RightArmLookAt(hitPoint);
        }

        //private void Tick()
        //{
        //    //_atom.OnUpdate();
        //    //if (_atom.IsFinished)
        //    //    Finish();
        //}
    }
}
