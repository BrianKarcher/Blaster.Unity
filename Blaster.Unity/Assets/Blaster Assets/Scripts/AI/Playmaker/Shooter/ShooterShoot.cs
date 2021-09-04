using BlueOrb.Scripts.AI.AtomActions;
using HutongGames.PlayMaker;
using BlueOrb.Common.Container;
using BlueOrb.Controller.Component;

namespace BlueOrb.Scripts.AI.PlayMaker.Attack
{
    [ActionCategory("BlueOrb.Shooter")]
    //[Tooltip("Returns Success if Button is pressed.")]
    public class ShooterShoot : FsmStateAction
    {
        [RequiredField]
        public FsmOwnerDefault gameObject;
        //public ShooterShootAtom _atom;

        //[HutongGames.PlayMaker.Tooltip("Event to send if the trigger event is detected.")]
        //public FsmEvent HangEvent;
        private PlayerShooterComponent _playerShooterComponent;

        public FsmBool ShootMainProjectile;
        public FsmBool ShootSecondaryProjectile;

        public override void OnEnter()
        {
            var go = Fsm.GetOwnerDefaultTarget(gameObject);
            if (go == null)
            {
                return;
            }
            var entity = go.GetComponent<IEntity>();
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
            // Todo: Create a projectile-specific delay so it doesn't finish right away
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
