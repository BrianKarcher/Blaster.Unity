using HutongGames.PlayMaker;
using BlueOrb.Common.Container;
using UnityEngine;
using BlueOrb.Physics;
using BlueOrb.Controller;

namespace BlueOrb.Scripts.AI.Playmaker
{
    [ActionCategory("RQ.Physics")]
    [HutongGames.PlayMaker.Tooltip("Circle an entity.")]
    public class CircleEntity : BasePlayMakerAction
    {
        [RequiredField]
        [CheckForComponent(typeof(Rigidbody))]
        public FsmOwnerDefault gameObject;

        [UIHint(UIHint.Variable)]
        [HutongGames.PlayMaker.Tooltip("Game Object to circle. If left None, Entity Target is used.")]
        public FsmGameObject Target;
        //public float Radius;

        [HutongGames.PlayMaker.Tooltip("Speed, in radians")]
        public FsmFloat RotateSpeed = 3f;

        //[HutongGames.PlayMaker.Tooltip("Name of strafing velocity animation variable")]
        //public FsmString AnimVelX;

        private GameObject target;
        private IPhysicsComponent physicsComponent;
        private AnimationComponent animationComponent;
        private int direction = 1;
        private IEntity entity;
        private bool oldAutoApplyToAnimator;

        public override void Reset()
        {
            gameObject = null;
        }

        public override void OnPreprocess()
        {
            base.OnPreprocess();
            Fsm.HandleFixedUpdate = true;
            Fsm.HandleCollisionEnter = true;
        }

        public override void OnEnter()
        {
            var go = Fsm.GetOwnerDefaultTarget(gameObject);
            if (go == null)
            {
                return;
            }
            this.entity = base.GetEntityBase(go);
            if (physicsComponent == null)
                physicsComponent = entity.Components.GetComponent<IPhysicsComponent>();
            if (animationComponent == null)
                animationComponent = entity.Components.GetComponent<AnimationComponent>();

            if (this.Target.IsNone)
            {
                this.target = this.entity.Target;
            }
            else
            {
                this.target = this.Target.Value;
            }

            this.direction = Random.value > 0.5f ? 1 : -1;
            animationComponent.SetForwardSpeed(0f);
            this.oldAutoApplyToAnimator = this.physicsComponent.AutoApplyToAnimator;
            this.physicsComponent.AutoApplyToAnimator = false;
            //_angle = Vector2.Angle(_target.transform.position, entity.transform.position);
            //var signedAngle = Vector2.SignedAngle(entity.transform.position, _target.transform.position);

            //_angle = Vector2.Angle(entity.transform.position, _target.transform.position);
            //Vector3.Cross(entity.transform.position, _target.transform.position);
            //_physicsComponent.GetSteering().TurnOn(behavior_type.seek);

            //_animationComponent.SetBool(_animationComponent.StrafeAnim, true);
            //_animationComponent.AddLayerWeightLerp("Strafe Movement", 1f, 0.25f);
        }

        //private float GetCurrentAngle()
        //{
        //    var dir = (_entity.transform.position - _target.transform.position);
        //    var signedAngle = Vector2.SignedAngle(Vector2.right, new Vector2(dir.x, dir.z));
        //    //_angle =  * Mathf.Deg2Rad;
        //    return signedAngle * Mathf.Deg2Rad;
        //}

        //public void OnCollisionEnter()
        //{
        //    
        //}

        public override void OnExit()
        {
            base.OnExit();
            physicsComponent.Stop();
            //_animationComponent.SetBool(_animationComponent.StrafeAnim, false);
            animationComponent.SetSideSpeed(0f);
            animationComponent.AddLayerWeightLerp("Strafe Movement", 0f, 0.25f);
            this.physicsComponent.AutoApplyToAnimator = oldAutoApplyToAnimator;
            //_physicsComponent.GetSteering().TurnOff(behavior_type.seek);
        }

        public override void OnFixedUpdate()
        {
            base.OnFixedUpdate();
            //var time = Time.deltaTime;
            //var _angle = GetCurrentAngle() + RotateSpeed * time * _direction;
            ////Debug.Log("Time: " + time + ", angle = " + _angle);
            //var unitVector = new Vector3(Mathf.Cos(_angle), 0f, Mathf.Sin(_angle));

            //var offset = unitVector * Radius;
            //var newPosition = _target.transform.position + offset;
            // TODO : This is bad. Fix!
            this.entity.transform.LookAt(target.transform.position);
            //_entity.transform.position = new Vector3(newPosition.x, _entity.transform.position.y, newPosition.z);

            float speed = RotateSpeed.Value * direction;

            physicsComponent.SetVelocity3(this.entity.transform.TransformDirection(speed, 0f, 0f));
            animationComponent.SetSideSpeed(speed);

            //////_physicsComponent.GetRidigbody().MovePosition(new Vector3(newPosition.x, _entity.transform.position.y, newPosition.z));

            //Value = GetValue();
            //if (InvertValue)
            //    Value = !Value;
        }

        //public override void DoCollisionEnter(Collision collisionInfo)
        //{
        //    base.DoCollisionEnter(collisionInfo);
        //    direction = direction * -1;
        //}

        //public override void OnUpdate()
        //{
        //    DoGetValue();
        //}

        //void DoGetValue()
        //{           
        //    _atom.OnUpdate();

        //}
    }
}
