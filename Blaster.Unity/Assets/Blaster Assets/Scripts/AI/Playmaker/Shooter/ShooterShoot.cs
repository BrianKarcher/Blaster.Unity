using HutongGames.PlayMaker;
using BlueOrb.Controller.Component;
using BlueOrb.Scripts.AI.Playmaker;
using UnityEngine;
using BlueOrb.Base.Manager;

namespace BlueOrb.Scripts.AI.PlayMaker.Attack
{
    [ActionCategory("BlueOrb.Shooter")]
    //[Tooltip("Returns Success if Button is pressed.")]
    public class ShooterShoot : BasePlayMakerAction
    {
        [RequiredField]
        public FsmOwnerDefault gameObject;
        //public ShooterShootAtom _atom;

        //[HutongGames.PlayMaker.Tooltip("Event to send if the trigger event is detected.")]
        //public FsmEvent HangEvent;
        private PlayerShooterComponent _playerShooterComponent;

        public FsmBool ShootMainProjectile;
        public FsmBool ShootSecondaryProjectile;
        private float endTime;

        public override void OnEnter()
        {
            var go = Fsm.GetOwnerDefaultTarget(gameObject);
            if (go == null)
            {
                return;
            }

            var entity = base.GetEntityBase(go);
            if (_playerShooterComponent == null)
            {
                _playerShooterComponent = entity.Components.GetComponent<PlayerShooterComponent>();
            }
            if (ShootMainProjectile.Value)
            {
                _playerShooterComponent.ShootMainProjectile();
                endTime = Time.time + GameStateController.Instance.LevelStateController.ShooterComponent.CurrentMainProjectileConfig.Cooldown;
            }
            if (ShootSecondaryProjectile.Value)
            {
                _playerShooterComponent.ShootSecondaryProjectile();
                endTime = Time.time + GameStateController.Instance.LevelStateController.ShooterComponent.GetSecondaryProjectile().ProjectileConfig.Cooldown;
            }
        }

        public override void OnExit()
        {
            base.OnExit();
        }

        public override void OnUpdate()
        {
            if (Time.time > endTime)
            {
                Finish();
            }
        }

        //private void Tick()
        //{

        //    //_atom.OnUpdate();
        //    //if (_atom.IsFinished)
        //    //    Finish();
        //}
    }
}
