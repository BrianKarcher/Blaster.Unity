using BlueOrb.Scripts.AI.AtomActions;
using HutongGames.PlayMaker;
using BlueOrb.Common.Container;
using BlueOrb.Controller.Component;

namespace BlueOrb.Scripts.AI.PlayMaker.Attack
{
    [ActionCategory("RQ.Shooter")]
    //[Tooltip("Returns Success if Button is pressed.")]
    public class ShooterShoot : FsmStateAction
    {
        //public ShooterShootAtom _atom;

        //[HutongGames.PlayMaker.Tooltip("Event to send if the trigger event is detected.")]
        //public FsmEvent HangEvent;
        private PlayerShooterComponent _playerShooterComponent;

        public FsmBool ShootMainProjectile;
        public FsmBool ShootSecondaryProjectile;

        public override void OnEnter()
        {
            var entity = Owner.GetComponent<IEntity>();
            if (_playerShooterComponent == null)
            {
                _playerShooterComponent = entity.Components.GetComponent<PlayerShooterComponent>();
            }
            if (ShootMainProjectile.Value)
            {
                _playerShooterComponent.ShootMainProjectile();
            }
            if (ShootSecondaryProjectile.Value)
            {
                _playerShooterComponent.ShootSecondaryProjectile();
            }
            Finish();
        }

        public override void OnExit()
        {
            base.OnExit();
        }

        //public override void OnUpdate()
        //{
        //    Tick();
        //}

        //private void Tick()
        //{

        //    //_atom.OnUpdate();
        //    //if (_atom.IsFinished)
        //    //    Finish();
        //}
    }
}
