using BlueOrb.Common.Container;
using BlueOrb.Controller;
using BlueOrb.Physics;
using UnityEngine;

namespace BlueOrb.Scripts.AI.AtomActions
{
    public class CircleEntityAtom : AtomActionBase
    {
        public float Radius;

        [Tooltip("Speed, in radians")]
        public float RotateSpeed;

        private GameObject _target;
        private PhysicsComponent _physicsComponent;
        private AnimationComponent _animationComponent;
        private int _direction = 1;
        //private float _angle;
        //private Vector2 _dragForce;

        public override void Start(IEntity entity)
        {
            base.Start(entity);
            if (_physicsComponent == null)
                _physicsComponent = entity.Components.GetComponent<PhysicsComponent>();
            if (_animationComponent == null)
                _animationComponent = entity.Components.GetComponent<AnimationComponent>();

            //_animationComponent.SetBool(_animationComponent.StrafeAnim, true);
            _animationComponent.AddLayerWeightLerp("Strafe Movement", 1f, 0.25f);

            //_angle = Vector2.Angle(_target.transform.position, entity.transform.position);
            //var signedAngle = Vector2.SignedAngle(entity.transform.position, _target.transform.position);

            //_angle = Vector2.Angle(entity.transform.position, _target.transform.position);
            //Vector3.Cross(entity.transform.position, _target.transform.position);
            //_physicsComponent.GetSteering().TurnOn(behavior_type.seek);
        }

        private float GetCurrentAngle()
        {
            var dir = (_entity.transform.position - _target.transform.position);
            var signedAngle = Vector2.SignedAngle(Vector2.right, new Vector2(dir.x, dir.z));
            //_angle =  * Mathf.Deg2Rad;
            return signedAngle * Mathf.Deg2Rad;
        }

        public void OnCollisionEnter()
        {
            _direction = _direction * -1;
        }

        public override void OnUpdate()
        {
            base.OnUpdate();
            //var time = Time.deltaTime;
            //var _angle = GetCurrentAngle() + RotateSpeed * time * _direction;
            ////Debug.Log("Time: " + time + ", angle = " + _angle);
            //var unitVector = new Vector3(Mathf.Cos(_angle), 0f, Mathf.Sin(_angle));

            //var offset = unitVector * Radius;
            //var newPosition = _target.transform.position + offset;
            _entity.transform.LookAt(_target.transform.position);
            //_entity.transform.position = new Vector3(newPosition.x, _entity.transform.position.y, newPosition.z);
            
            _physicsComponent.SetVelocity3(_entity.transform.TransformDirection(3f, 0f, 0f));
            _animationComponent.SetSideSpeed(3f);

            //////_physicsComponent.GetRidigbody().MovePosition(new Vector3(newPosition.x, _entity.transform.position.y, newPosition.z));

            //Value = GetValue();
            //if (InvertValue)
            //    Value = !Value;
        }

        public override void End()
        {
            base.End();
            _physicsComponent.Stop();
            //_animationComponent.SetBool(_animationComponent.StrafeAnim, false);
            _animationComponent.AddLayerWeightLerp("Strafe Movement", 0f, 0.25f);
            //_physicsComponent.GetSteering().TurnOff(behavior_type.seek);
        }

        public void SetTarget(GameObject target)
        {
            _target = target;
        }
    }
}
