using BlueOrb.Base.Extensions;
using BlueOrb.Common.Container;
using BlueOrb.Controller.Damage;
using BlueOrb.Physics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using BlueOrb.Controller.Player;

namespace BlueOrb.Scripts.AI.AtomActions
{
    [Serializable]
    public class DamageBounceAtom : AtomActionBase
    {
        //[SerializeField]
        //private float _bounceTime;
        private DamageEntityInfo _damageInfo;
        private PhysicsComponent _physicsComponent;
        private ThirdPersonUserControl _thirdPersonUserControl;
        private PlayerController _playerController;

        private DamageComponent _damageComponent;
        //private AnimationComponent _animationComponent;
        public bool UseDamageColor = true;
        private Vector2 _damageBounceDragForce;
        private long _damageBounceCompleteIndex;
        //private IPhysicsAffector _damageBounceAffector;
        private bool _firstRun = true;

        public override void Start(IEntity entity)
        {
            base.Start(entity);

            _physicsComponent = entity.Components.GetComponent<PhysicsComponent>();
            _damageComponent = entity.Components.GetComponent<DamageComponent>();
            _damageInfo = _damageComponent.GetDamageInfo();

            if (_thirdPersonUserControl == null)
                _thirdPersonUserControl = entity.Components.GetComponent<ThirdPersonUserControl>();
            if (_playerController == null)
                _playerController = entity.Components.GetComponent<PlayerController>();
            // _previousValue = _thirdPersonUserControl.GetEnablePlayerInput();
            _thirdPersonUserControl?.SetEnablePlayerInput(false);
            _playerController?.SetMovementActive(false);

            //_animationComponent = entity.Components.GetComponent<AnimationComponent>();

            //Debug.Log("Ouch! That hurt!");
            _physicsComponent.SetEnabled(false);
            _physicsComponent.Stop();
            //MessageDispatcher.Instance.DispatchMsg(0f, _entity.UniqueId, _physicsComponent.UniqueId,
            //    Enums.Telegrams.StopMovement, null);

            //MessageDispatcher.Instance.DispatchMsg(0f, _entity.UniqueId, _damageComponent.UniqueId,
            //    Enums.Telegrams.GetDamageEntityInfo, null, (damageInfo) => _damageInfo = damageInfo as DamageEntityInfo);

            

            _firstRun = true;

            //if (_damageComponent.UseDamageColor && UseDamageColor)
            //{
            //    _animationComponent.GetSpriteAnimator().SetColor("Damage", _damageComponent.DamageColor);
            //}
        }

        //public override void StartListening(IComponentRepository entity)
        //{
        //    base.StartListening(entity);
        //    //_damageBounceCompleteIndex = MessageDispatcher2.Instance.StartListening("DamageBounceComplete", entity.UniqueId, (data) =>
        //    //{
        //    //    _isRunning = false;
        //    //});
        //}

        //public override void StopListening(IComponentRepository entity)
        //{
        //    base.StopListening(entity);
        //    //MessageDispatcher2.Instance.StopListening("DamageBounceComplete", entity.UniqueId, _damageBounceCompleteIndex);
        //}

        public override void End()
        {
            base.End();
            //sprite.GetSteering().TurnOff(behavior_type.wander);
            //_damageBounceAffector.Force = Vector2D.Zero();
            //_damageBounceAffector.Velocity = Vector2D.Zero();
            //_damageBounceAffector.Stop();
            _physicsComponent.Stop();
            _physicsComponent.SetEnabled(true);
            _thirdPersonUserControl?.SetEnablePlayerInput(true);
            _playerController?.SetMovementActive(true);
            //MessageDispatcher.Instance.DispatchMsg(0f, _entity.UniqueId, _damageComponent.UniqueId,
            //    Enums.Telegrams.GetDamageEntityInfo, null, (damageInfo) => _damageInfo = damageInfo as DamageEntityInfo);
            //_damageInfo.IsDamaged = false;
            //StateMachine.GetStateInfo().IsDamaged = false;
            //MessageDispatcher.Instance.DispatchMsg(0f, this.UniqueId, _physicsComponent.UniqueId,
            //    Enums.Telegrams.SetDamageInfo, null);
            //_physicsComponent.DamageInfo = null;
            //if (_damageComponent.UseDamageColor && UseDamageColor)
            //{
            //    _animationComponent.GetSpriteAnimator().RemoveColor("Damage");
            //    //_animationComponent.GetSpriteAnimator().SetColor(Color.white);
            //}
        }

        public override void OnUpdate()
        {
            // Wait a frame for all of the "stops" to take effect.
            // Starts on a fresh frame with no forces applied in this Update cycle.
            if (_firstRun)
            {
                var physicsData = _physicsComponent.GetPhysicsData();

                //physicsData.InputForce.SetToZero();
                //_physicsComponent.GetPhysicsAffector("Input").Stop();

                var force = _damageComponent.GetDamageComponentData().DamagedBounceForce;

                //if (_damageInfo == null)
                //    throw new Exception("DamageInfo is null");

                var bounceFromLocation = _physicsComponent.Controller.BounceFromLocation;
                var ourPosition = _physicsComponent.GetWorldPos3();
                Debug.Log($"Bounce From {bounceFromLocation}");
                Debug.Log($"Our Position: {ourPosition}");

                var bounceDir = ourPosition - bounceFromLocation;
                var bounceDirNormalized = bounceDir.xz().normalized;
                //var bounceDir2D = bounceDirNormalized.xz();
                var initialBounceForce = bounceDirNormalized * force;
                //_damageBounceAffector = _physicsComponent.GetPhysicsAffector("DamageBounce");

                // Bounces are an initial velocity
                //_physicsComponent.AddVelocity(initialBounceForce);
                var localInitialBounceForce = _entity.transform.InverseTransformDirection(initialBounceForce.xz());
                Debug.Log($"Initial Local Bounce Force: {localInitialBounceForce}");
                _physicsComponent.AddForce2(initialBounceForce, ForceMode.Impulse);
                //_physicsComponent.AddForce(initialBounceForce);
                //_damageBounceAffector.Velocity = initialBounceForce;
                // Create a constant force in the opposite direction in the force of the drag
                _damageBounceDragForce = bounceDirNormalized * (_damageComponent.GetDamageComponentData().DamageDrag * -1f);

                var localBounceDragForce = _entity.transform.InverseTransformDirection(_damageBounceDragForce.xz());
                Debug.Log($"Continuous Local Bounce Force: {localBounceDragForce}");
                    _firstRun = false;
            }
            _physicsComponent.AddForce2(_damageBounceDragForce);
            //_physicsComponent.AddForce2(_damageBounceDragForce * Time.deltaTime);
            //return _isRunning ? AtomActionResults.Running : AtomActionResults.Success;
        }
    }
}
