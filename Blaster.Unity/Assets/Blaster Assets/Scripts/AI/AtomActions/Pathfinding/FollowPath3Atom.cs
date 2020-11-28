using BlueOrb.Common.Container;
using BlueOrb.Controller.Physics;
using BlueOrb.Messaging;
using BlueOrb.Physics;
using System.Collections.Generic;
using UnityEngine;

namespace BlueOrb.Scripts.AI.AtomActions
{
    public class FollowPath3Atom : AtomActionBase
    {
        private SteeringBehaviorManager _steering;
        public string _physicsComponentName;
        //private System.Action Complete;
        private BlueOrb.Physics.SteeringBehaviors3D.FollowPath3 _behavior;
        private PhysicsComponent _physicsComponent;
        private List<Vector3> Path;
        public Vector3 _offset;
        private long newPathId;
        private WaypointComponent _waypointComponent;

        public override void Start(IEntity entity)
        {
            base.Start(entity);
            if (_physicsComponent == null)
                _physicsComponent = entity.Components.GetComponent<PhysicsComponent>(_physicsComponentName);
            if (_waypointComponent == null)
                _waypointComponent = entity.Components.GetComponent<WaypointComponent>();
            if (_physicsComponent == null)
            {
                Debug.Log("(SeekAtom) Unable to locate physics component " + _physicsComponentName);
                return;
            }
            _steering = _physicsComponent.GetSteering() as SteeringBehaviorManager;
            _physicsComponent.GetSteering().TurnOn(behavior_type.follow_path3d);
            _behavior = _steering.GetSteeringBehavior(behavior_type.follow_path3d) as BlueOrb.Physics.SteeringBehaviors3D.FollowPath3;            
            _behavior.WaypointChanged = WaypointChanged;
            if (Path.Count == 0)
                return;
            var isSame = _behavior.ComparePath(Path);
            if (!isSame)
                _behavior.SetPath(Path);
            _behavior.Path.IsFinished = false;
            if (_waypointComponent != null)
            {
                _behavior.Path.PathWalkingDirection = _waypointComponent.FollowPathData.PathWalkingDirection;

                _behavior.Path.PathType = _waypointComponent.FollowPathData.PathType;
            }
            else
            {
                _behavior.Path.PathWalkingDirection = BlueOrb.Physics.Enums.PathWalkingDirection.Forwards;

                _behavior.Path.PathType = BlueOrb.Physics.Enums.PathType.Once;
            }

            //_behavior.Path



            WaypointChanged(Vector3.zero);
        }

        private void WaypointChanged(Vector3 force)
        {
            Vector3 wayPoint;
            if (!_behavior.Path.CurrentWaypoint(out wayPoint))
                return;
            //MessageDispatcher2.Instance.DispatchMsg("VelocityChanged", 0f, string.Empty, _entity.UniqueId, (Vector2D)(wayPoint - GetFeetPosition()));
            //SendMessageToSpriteBase(0f, Telegrams.VelocityChanged, _behavior.Path.CurrentWaypoint() - GetFeetPosition());
            //_physicsComponent.SendLocalMessageToAll(0f, Enums.Telegrams.VelocityChanged, _behavior.Path.CurrentWaypoint() - GetFeetPosition());
            //_sprite.SetFacingDirectionBasedOnVelocity(_behavior.Path.CurrentWaypoint() - GetFeetPosition());
            if (_behavior.Path.IsFinished)
            {
                Finish();
                //Complete?.Invoke();
                //_isRunning = false;
                //StateMachine.GetStateInfo().IsComplete = true;
            }
        }

        public void SetPath(List<Vector3> path)
        {
            Path = path;
        }

        public override void StartListening(IEntity entity)
        {
            base.StartListening(entity);
            newPathId = MessageDispatcher.Instance.StartListening("NewPath", entity.GetId(), (data) =>
            {
                //var newPath = (PathfindingData)data.ExtraInfo;
                var newPath = ((List<Vector3> path, int waypoint))data.ExtraInfo;
                Path = newPath.path;
                _behavior.SetPath(Path, newPath.waypoint);
                //AdjustWwaypointsForCurrentPosition(newPath);
            });
        }

//        private void AdjustWwaypointsForCurrentPosition(PathfindingData newPath)
//        {
//            //if (closestOnPathCheck)
//            //{
//            // Simulate movement from the point where the path was requested
//            // to where we are right now. This reduces the risk that the agent
//            // gets confused because the first point in the path is far away
//            // from the current position (possibly behind it which could cause
//            // the agent to turn around, and that looks pretty bad).
//            //Vector3 p1 = Time.time - lastFoundWaypointTime < 0.3f ? lastFoundWaypointPosition : p.originalStartPoint;
//            Vector2 p1 = newPath.OriginalStartPoint;
//            Vector2 p2 = _physicsComponent.GetFeetWorldPosition();
//            Vector2 dir = p2 - p1;
//            float magn = dir.magnitude;


//            int steps = (int)(magn / _behavior.WaypointSeekDist);
//            dir /= steps;

//#if ASTARDEBUG
//			Debug.DrawLine(p1, p2, Color.red, 1);
//#endif

//            // Iterate from the original request start point to the current position to see if we crossed any waypoints
//            // If any were crossed, increment the waypoint counter past them so we don't walk backwards
//            for (int i = 0; i <= steps; i++)
//            {
//                _behavior.CheckAndUpdateWaypoint(p1);
//                //CalculateVelocity(p1);
//                p1 += dir;
//            }
//        }

        public override void StopListening(IEntity entity)
        {
            base.StopListening(entity);
            MessageDispatcher.Instance.StopListening("NewPath", entity.GetId(), newPathId);
        }

        public override void End()
        {
            base.End();
            _physicsComponent.GetSteering().TurnOff(behavior_type.follow_path3d);
            //_steering.TurnOff(Physics.behavior_type.);
            //_behavior.TurnOff();
            // Persist the walking direction
            if (_waypointComponent != null)
            {
                _waypointComponent.FollowPathData.PathWalkingDirection = _behavior.Path.PathWalkingDirection;
            }
            //_steering.GetSteeringBehavior(behavior_type.wall_avoidance).TurnOff();
            //Release the previous path
            //path.Release(this);
            Path = null;
        }

        //public override void OnUpdate()
        //{
        //    if (Path == null)
        //        return;
        //    // TODO Should be in OnDrawGizmos
        //    //for (int i = 0; i < Path.Length - 1; i++)
        //    //{
        //    //    Debug.DrawLine(Path[i], Path[i + 1], Color.red, 10, false);
        //    //}
        //    return _isRunning ? AtomActionResults.Running : AtomActionResults.Success;
        //}

        //private SmartFollowData Data { get; set; }
        //[SerializeField]
        //private Transform _objectToShoot;

        //[SerializeField]
        //private float _delay;
        //[SerializeField]
        //private bool _stopMoving;
        //[SerializeField]
        //private ShootTarget _shootTarget;
        //[SerializeField]
        //private Transform _target;
        //[SerializeField]
        //private Vector2D _location;
        //[SerializeField]
        //private float _minSpeed;
        //[SerializeField]
        //private float _maxSpeed;

        // This helps to prevent sprite jitter by not letting them follow a path that is too short

        //[SerializeField]
        //private float _refreshTimer = 1f;

        //private Seeker _seeker;
        /** Current path which is followed */
        //protected Path path;

        //private ISprite _sprite;
        //private Transform _followTransform;
        //private PhysicsComponent _followPhysicsComponent;
        //private CollisionComponent _followCollisionComponent;
        //private bool _calculatingPath = false;

        //private float _nextStuckCheck;
        //private Vector2D _lastCheckedPosition;
        //private int _graphMask;
        //
        //public override void Awake()
        //{
        //    base.Awake();
        //}

        //public override void SetupState()
        //{
        //    base.SetupState();
        //    if (!Application.isPlaying)
        //        return;
        //    //_sprite = EntityUIBase.GetEntity(entity);
        //    if (_physicsComponent == null)
        //        throw new Exception("FSM - Sprite not set.");
        //    //_aiComponent = _spriteBase.Components.GetComponent<AIComponent>();
        //    //_seeker = _aiComponent.GetSeeker();



        //    //_nextStuckCheck = 0f;
        //}

        //private const string StateName = "ShootObject";

        //public override void Enter()
        //{
        //    base.Enter();


        //    //_calculatingPath = false;



        //    //_seeker.
        //    //We should search from the current position
        //    //Enables the first and third graphs to be included, but not the rest
        //    // Graph 1
        //    //int graphMask = (1 << 0);// | (1 << 2);
        //    //int graphMask = (1 << 0) | (1 << 1);
        //    // Graph 2
        //    //int graphMask = (1 << 1);
        //    // Graph 3
        //    //StateMachine.GetStateInfo().IsStuck = false;
        //    //var followEntity = EntityUIBase.GetEntity(_followTransform.gameObject);


        //    //if (_stopMoving)
        //    //    _sprite.Stop();
        //    //MessageDispatcher.Instance.DispatchMsg(_delay, _sprite.ID(), _sprite.ID(),
        //    //    Enums.Telegrams.ProcessStateEvent, ID, Enums.TelegramEarlyTermination.ChangeScenes);
        //    //if (_refreshTimer != 0f)
        //    //{
        //    //    MessageDispatcher2.Instance.DispatchMsg("Refresh", _refreshTimer, this.UniqueId, this.UniqueId, null);
        //    //}
        //}

        //public override void Exit()
        //{
        //    base.Exit();

        //    //_seeker.pathCallback -= OnPathComplete;
        //    //StateMachine.GetStateInfo().IsStuck = false;
        //    //MessageDispatcher2.Instance.RemoveMessages("Refresh", this.UniqueId);
        //}

        //public override void StartListening()
        //{
        //    base.StartListening();
        //    MessageDispatcher2.Instance.StartListening("Refresh", this.UniqueId, (data) =>
        //    {
        //        InitiateFollow();
        //        if (_refreshTimer != 0f)
        //        {
        //            MessageDispatcher2.Instance.DispatchMsg("Refresh", _refreshTimer,
        //                this.UniqueId, this.UniqueId, null);
        //        }
        //    });
        //}

        //public override void StopListening()
        //{
        //    base.StopListening();
        //    MessageDispatcher2.Instance.StopListening("Refresh", this.UniqueId, -1);
        //}







        //private PhysicsComponent GetFollowEntity()
        //{            
        //    //return EntityUIBase.GetEntity(_followTransform.gameObject);
        //    return _followTransform.GetComponent<PhysicsComponent>();
        //}

        //public override void FixedUpdate()
        //{
        //    if (!_isSetup)
        //        return;
        //    if (_calculatingPath)
        //        return;

        //    base.FixedUpdate();
        //    //if (_behavior.Path.IsFinished)
        //    //{
        //    //    // Reiniate the follow to keep close to the followee
        //    //    InitiateFollow();
        //    //    return;
        //    //    //_behavior.TurnOff();
        //    //    //_sprite.Stop();
        //    //}

        //    //if (Time.time > _nextStuckCheck)
        //    //{
        //    //    if (Vector2D.Vec2DDistance(GetFeetPosition(), _lastCheckedPosition) < .16f)
        //    //    {
        //    //        // stuck, recalculate position
        //    //        //InitiateFollow();
        //    //        StateMachine.GetStateInfo().IsStuck = true;
        //    //        return;
        //    //    }
        //    //    _lastCheckedPosition = GetFeetPosition();
        //    //    _nextStuckCheck = Time.time + 1;
        //    //}



        //    //foreach (var vector in _path)
        //    //{

        //    //}
        //}

        private Vector3 GetFeetPosition()
        {
            return _entity.GetFootPosition() + _offset;
        }

        //private Vector3 GetFeetPosition3D()
        //{
        //    return _physicsComponent.GetFeetPosition3D() + _offset.ToVector3(0);
        //}



        //public override void Serialize(Serialization.EntitySerializedData entitySerializedData)
        //{
        //    base.Serialize(entitySerializedData);
        //    //if (!entitySerializedData.Datas.ContainsKey(GetName()))
        //    base.SerializeComponent(entitySerializedData, Data);
        //}

        //public override void Deserialize(Serialization.EntitySerializedData entitySerializedData)
        //{
        //    base.Deserialize(entitySerializedData);
        //    Data = base.DeserializeComponent<SmartFollowData>(entitySerializedData);

        //    //Data = Persistence.DeserializeObject<SmartFollowData>(entitySerializedData.Datas[GetName()]);
        //}

        //public void OnComplete(System.Action complete)
        //{
        //    Complete = complete;
        //}
    }
}
