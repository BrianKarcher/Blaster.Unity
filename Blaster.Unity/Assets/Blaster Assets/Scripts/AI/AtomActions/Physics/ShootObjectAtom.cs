using BlueOrb.Common.Container;
using BlueOrb.Controller.Camera;
using BlueOrb.Controller.Player;
using BlueOrb.Physics;
using System;
using System.Collections;
using UnityEngine;

namespace BlueOrb.Scripts.AI.AtomActions
{
    public enum ShootTarget
    {
        StraightShot = 0,
        ToTarget = 1,
        Random = 2,
        ToLocation = 3,
        CameraRaycast = 4,
        ShooterDirection = 5,
        None = 6
    }

    public class ShootObjectAtom : AtomActionBase
    {
        private GameObject _objectToShoot = null;
        private GameObject _spawnPoint;

        public Vector3 _offset;
        private float _delay = 0f;
        public ShootTarget _shootTarget = ShootTarget.Random;
        public Vector3 _shootToLocation;
        public bool LookToVelocity = false;
        public Vector3 _rotation;
        [SerializeField]
        [HideInInspector]
        public float _minSpeed = 0f;
        [SerializeField]
        [HideInInspector]
        public float _maxSpeed = 0f;
        public bool _createAsChild = false;

        private int _layerMask = 0;
        //private PhysicsComponent _physicsComponent;
        private Vector3 _spawnPointPosition;
        //private PlayerController _playerController;
        //private ICameraController _camera;
        private UnityEngine.Camera _camera;
        //private AIComponent _aIComponent;
        //private CollisionComponent _collisionComponent;
        //private FloorComponent _floorComponent;
        //private long _shoootObjectMessageIndex;
        //private IComponentRepository _entity;

        public override void Start(IEntity entity)
        {
            base.Start(entity);
            _entity = entity;

            //_animationComponent = entity.Components.GetComponent<AnimationComponent>();
            //if (_physicsComponent == null)
            //    _physicsComponent = entity.Components.GetComponent<PhysicsComponent>();
            _spawnPointPosition = GetSpawnpointWithOffset();

            if (_camera == null)
            {
                var cameraGo = UnityEngine.Camera.main;
                _camera = cameraGo;
                //_camera = _entity.Components.GetComponent<ICameraController>();
            }
            //_camera = UnityEngine.Camera.main.transform.parent.GetComponent<ThirdPersonCameraController>();
            //if (_playerController == null)
            //    _playerController = entity.Components.GetComponent<PlayerController>();
            //_aIComponent = entity.Components.GetComponent<AIComponent>();
            //_collisionComponent = entity.Components.GetComponent<CollisionComponent>();
            //_floorComponent = entity.Components.GetComponent<FloorComponent>();
            ProcessShoot(_entity);
            Finish();
        }

        public IEnumerator ShootObject()
        {
            yield return new WaitForSeconds(_delay);
            ProcessShoot(_entity);
            Finish();
        }

        private Vector3 GetSpawnpointWithOffset()
        {
            if (_spawnPoint != null)
                return _spawnPoint.transform.TransformPoint(_offset);
            else
                return _entity.transform.TransformPoint(_offset);
        }

        private void ProcessShoot(IEntity entity)
        {
            var velocity = CalculateVelocity();

            //var angleToRotate = _animationComponent.GetFacingDirection().GetDirectionAngle();

            //var position = _physicsComponent.GetWorldPos() + RotateAroundAxis(new Vector3(_offset.x, _offset.y, 0f), angleToRotate, new Vector3(0, 0, 1));

            Vector3 position = GetSpawnpointWithOffset();

            //var newObject = (GameObject.Instantiate(_objectToShoot, position, entity.transform.rotation) as Transform).GetComponent<IComponentRepository>();
            //IComponentRepository newObject = null;
            //if (string.IsNullOrEmpty(ObjectPoolName))
            //{
            //    newObject = (GameObject.Instantiate(_objectToShoot, position, entity.transform.rotation) as Transform).GetComponent<IComponentRepository>();
            //}
            //else
            //{
            //    var newGO = ObjectPool.Instance.PullGameObjectFromPool(ObjectPoolName, position, Quaternion.identity);
            //    newObject = newGO.GetComponent<IComponentRepository>();
            //    newObject.Reset();
            //}
            //GameObject newObject = null;
            Quaternion rotation;
            if (LookToVelocity)
            {
                rotation = Quaternion.LookRotation(velocity);
            }
            else
            {
                rotation = entity.transform.rotation * Quaternion.Euler(_rotation);
            }
            var newObject = GameObject.Instantiate(_objectToShoot, position, rotation) as GameObject;
            if (_createAsChild)
            {
                newObject.transform.parent = _entity.transform;
            }            

            IEntity newEntity = null;
            if (newObject != null)
                newEntity = newObject.GetComponent<IEntity>();
            IPhysicsComponent newPhysicsComponent;
            if (newEntity == null)
            {
                // Not an Entity Common Component
                newPhysicsComponent = newObject.GetComponent<IPhysicsComponent>();
            }
            else
            {
                // Get the Physics Component through the Entity Common Component
                newPhysicsComponent = newEntity.Components.GetComponent<IPhysicsComponent>();
            }

            if (newPhysicsComponent == null)
            {
                var rigidBody = newObject.GetComponent<Rigidbody>();
                if (rigidBody == null)
                    Debug.LogError($"Could not locate PhysicsComponent or RigidBody on {newObject.name}!");
                else
                {
                    //throw new Exception("Could not locate PhysicsComponent or RigidBody!");
                    rigidBody.velocity = velocity;
                }
            }
            else
            {
                newPhysicsComponent?.SetVelocity3(velocity);
            }
            //var thisLevel = GetLevel();
            
            //newPhysicsComponent.GetPhysicsAffector("Steering").Velocity = velocity.ToVector3(0);
            //newPhysicsComponent.SetVelocity(velocity.ToVector3(0));
            // Set the new object to the same level as this one
            //newObject.SendMessageToComponents<CollisionComponent>(0f, this._uniqueId, Telegrams.SetLevelHeight,
            //    thisLevel);
            //var otherFloorComponent = repo.Components.GetComponent<FloorComponent>();
            //otherFloorComponent?.SetFloor((int)thisLevel);
            Finish();
        }

        public Vector3 RotateAroundAxis(Vector3 v, float a, Vector3 axis)
        {
            var q = Quaternion.AngleAxis(a, axis);
            return q * v;
        }

        private Vector3 CalculateVelocity()
        {
            var speed = UnityEngine.Random.Range(_minSpeed, _maxSpeed);

            Vector3 direction = Vector3.zero;

            switch (_shootTarget)
            {
                case ShootTarget.StraightShot:
                    direction = _entity.transform.forward; //_animationComponent.GetFacingDirectionVector();
                    break;
                case ShootTarget.Random:
                    //targetVector = new Vector2D(UnityEngine.Random.Range(-1, 1), UnityEngine.Random.Range(-1, 1));
                    var angle = UnityEngine.Random.Range(0f, 360f);
                    var x = Mathf.Cos(angle);
                    var y = Mathf.Sin(angle);
                    direction = new Vector3(x, 0, y);
                    break;
                case ShootTarget.ToLocation:
                    direction = _shootToLocation - _spawnPointPosition;
                    break;
                case ShootTarget.ToTarget:
                    var targetEntity = _entity.Target.GetComponent<IEntity>();
                    if (targetEntity != null)
                        direction = (targetEntity.GetPosition() + _shootToLocation) - _spawnPointPosition;
                    else
                        direction = (_entity.Target.transform.position + _shootToLocation) - _spawnPointPosition;
                    break;
                case ShootTarget.CameraRaycast:
                    Ray ray = _camera.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0));
                    // Create a vector at the center of our camera's viewport
                    // Vector3 rayOrigin = _camera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0.0f));

                    // var rtn = UnityEngine.Physics.Raycast(rayOrigin, _camera.transform.forward, out hitInfo, maxDistance, layerMask);
                    var maxDistance = 1000f;
                    var rtn = UnityEngine.Physics.Raycast(ray, out var hitInfo, maxDistance, _layerMask);
                    //if (!_camera.Raycast(1000f, _layerMask, out var hitInfo))
                    if (!rtn)
                    {
                        // If raycast no-hit, just point down the camera forward direction very far
                        direction = _camera.transform.TransformPoint(new Vector3(0f, 0f, 1000f)) - _spawnPointPosition;
                    }
                    else
                    {
                        direction = hitInfo.point - _spawnPointPosition;
                    }
                    break;
                case ShootTarget.ShooterDirection:
                    throw new NotImplementedException("Shooter Direction not implemented");
                    //direction = _playerController.Shooter.transform.forward;
                    break;
                case ShootTarget.None:
                    return Vector3.zero;
            }

            var velocity = direction.normalized * speed;

            return velocity;
        }

        public override void OnUpdate()
        {
            //return _isRunning ? AtomActionResults.Running : AtomActionResults.Success;
        }

        public void SetLayerMask(int layerMask)
        {
            _layerMask = layerMask;
        }

        public void SetObjectToShoot(GameObject objectToShoot)
        {
            _objectToShoot = objectToShoot;
        }

        public void SetDelay(float delay)
        {
            _delay = delay;
        }

        public void SetSpawnPoint(GameObject spawnPoint)
        {
            _spawnPoint = spawnPoint;
        }
    }
}
