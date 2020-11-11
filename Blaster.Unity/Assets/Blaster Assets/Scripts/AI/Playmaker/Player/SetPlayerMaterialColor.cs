using HutongGames.PlayMaker;
using BlueOrb.Common.Container;
using BlueOrb.Controller.Player;
using UnityEngine;

namespace BlueOrb.Scripts.AI.Playmaker
{
    [ActionCategory("RQ.Player")]
    [HutongGames.PlayMaker.Tooltip("Set player material color.")]
    public class SetPlayerMaterialColor : FsmStateAction
    {
        [UIHint(UIHint.Variable)]
        [HutongGames.PlayMaker.Tooltip("Store the GameObject that collided with the Owner of this FSM.")]
        public FsmGameObject hangCollider;
        public string MaterialColorName;
        [ColorUsage(true, true)]
        public Color _tint;
        private Color _originalColor;

        //public PlayerHangAtom _atom;
        private PlayerController _playerController;

        public override void OnEnter()
        {
            //var rqSM = Owner.GetComponent<PlayMakerStateMachineComponent>();
            //_entity = rqSM.GetComponentRepository();
            var entity = Owner.GetComponent<IEntity>();
            if (_playerController == null)
                _playerController = entity.Components.GetComponent<PlayerController>();
            _originalColor = _playerController.GetTint(MaterialColorName);
            _playerController.SetTint(MaterialColorName, _tint);
            //_atom.SetHangCollider(hangCollider.Value);
            //_atom.Start(entity);
            Tick();
        }

        public override void OnExit()
        {
            base.OnExit();
            //_atom.End();
            _playerController.SetTint(MaterialColorName, _originalColor);
        }

        public override void OnUpdate()
        {
            base.OnUpdate();
            Tick();
            //if (_atom.IsFinished)
            //    Finish();
        }

        private void Tick()
        {
            //_atom.OnUpdate();
        }
    }
}
