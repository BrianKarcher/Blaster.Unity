using BlueOrb.Common.Container;
using BlueOrb.Physics;
using UnityEngine;

namespace BlueOrb.Scripts.AI.AtomActions.Physics
{
    public class SetSteeringTargetToVectorAtom : AtomActionBase
    {
        public enum GoToType
        {
            Target = 0,
            //Random = 1,
            //RandomLocation = 2,
            //RandomSubLocation = 3,
            //CurrentDestinationLocation = 4,
            Waypoint = 5,
            NextWaypoint = 6,
            //CustomVelocity = 6,
            Self = 7,
            Home = 8,
            Player = 9,
            GameObject = 10
        }

        public GoToType _goToType;
        public int WaypointIndex;
        
        //public string[] _messageReceivers;
        public bool sendToSelf = true;
        public string physicsComponentName;
        public string waypointName;

        private IPhysicsComponent _physicsComponent;
        private GameObject _goToGameObject;
        //private AIComponent _aIComponent;
        //private SteeringBehaviorManager _steering;

        public override void Start(IEntity entity)
        {
            base.Start(entity);
            if (_physicsComponent == null)
                _physicsComponent = entity.Components.GetComponent<IPhysicsComponent>(physicsComponentName);
            //if (_aIComponent == null)
            //    _aIComponent = entity.Components.GetComponent<AIComponent>();
            Tick();
            //_steering = _physicsComponent.GetSteering();
            //if (_steering == null)
            //    return;
            //_steering.Target = goToVector;
        }

        private void Tick()
        {
            if (_goToType == GoToType.Player)
            {
                int i = 0;
            }
            var goToVector = GetTargetLocation();
            if (goToVector == Vector3.negativeInfinity)
                return;
            if (sendToSelf)
            {
                _physicsComponent.GetSteering().SetTarget3(goToVector);
                //SendMessage(_entity.UniqueId, goToVector);
            }
        }

        //public void SendMessage(string messageReceiver)
        //{
        //    var goToVector = GetTargetLocation();
        //    if (goToVector == null)
        //        return;
        //    //for (int i = 0; i < messageReceiver.Length; i++)
        //    //{
        //    //    var receiver = messageReceiver[i];
        //    MessageDispatcher2.Instance.DispatchMsg("SetSteeringTarget", 0f, _entity.UniqueId, messageReceiver,
        //        goToVector);
        //    //}
        //}

        //private void SendMessage(string messageReceiver, Vector2D goToVector)
        //{
        //    MessageDispatcher2.Instance.DispatchMsg("SetSteeringTarget", 0f, _entity.UniqueId, messageReceiver, goToVector);
        //}

        public Vector3 GetTargetLocation()
        {
            switch (_goToType)
            {
                case GoToType.Target:
                    if (_entity.Target == null)
                        return Vector3.negativeInfinity;
                    return _entity.Target.transform.position;
                case GoToType.Self:
                    return _physicsComponent.GetWorldPos3();
                //case GoToType.RandomLocation:
                //    MessageDispatcher.Instance.DispatchMsg(0f, this.UniqueId, _aiComponent.UniqueId,
                //        Telegrams.ChooseRandomLocation, feetPos, (location) =>
                //        {
                //            _targetLocation = (Location)location;
                //        });
                //    return _targetLocation.transform.position;
                //case GoToType.RandomSubLocation:
                //    MessageDispatcher.Instance.DispatchMsg(0f, this.UniqueId, _aiComponent.UniqueId,
                //        Telegrams.ChooseRandomSubLocation, feetPos, (location) =>
                //        {
                //            _targetLocation = (Location)location;
                //        });
                //    return _targetLocation.transform.position;
                //case GoToType.Random:
                //    _targetLocation = null;
                //    throw new NotImplementedException("Random not implemented in GoTo");
                //case GoToType.CurrentDestinationLocation:
                //    MessageDispatcher.Instance.DispatchMsg(0f, this.UniqueId, _aiComponent.UniqueId,
                //        Telegrams.GetCurrentDestinationLocation, null, (location) =>
                //        {
                //            _targetLocation = (Location)location;
                //        });
                //    return _targetLocation.transform.position;
                //case GoToType.Waypoint:
                //    var waypointComponent = _entity.Components.GetComponent<WaypointComponent>(waypointName);
                //    if (waypointComponent == null)
                //        return Vector2D.Zero();
                //    if (waypointComponent._waypoints.Count == 0)
                //        return waypointComponent.transform.position;
                //    return waypointComponent._waypoints[WaypointIndex].transform.position;
                //return _waypoint.transform.position;
                //case GoToType.CustomVelocity:
                //    return _velocity;
                //case GoToType.NextWaypoint:
                //    var nextWaypointComponent = _entity.Components.GetComponent<WaypointComponent>(waypointName);
                //    if (nextWaypointComponent == null)
                //        return Vector3.negativeInfinity
                //    if (nextWaypointComponent._waypoints.Count == 0)
                //        return nextWaypointComponent.transform.position;
                //    return nextWaypointComponent.NextWaypoint();
                case GoToType.Home:
                    return _entity.HomePosition;
                case GoToType.Player:
                    return EntityContainer.Instance.GetMainCharacter().GetFootPosition();
                case GoToType.GameObject:
                    return _goToGameObject.transform.position;
            }
            return Vector3.negativeInfinity;
        }

        //public override void StartListening(IComponentRepository entity)
        //{
        //    _killSelfIndex = MessageDispatcher2.Instance.StartListening("AnimationComplete", entity.UniqueId, (data) =>
        //    {
        //        //var animation = _animComponent.Get
        //        if ((string)data.ExtraInfo != AnimationType)
        //            return;
        //        _isRunning = false;
        //    });
        //}

        //public override void StopListening(IComponentRepository entity)
        //{
        //    //MessageDispatcher2.Instance.StopListening("AnimationComplete", entity.UniqueId, _animationCompleteIndex);
        //}

        //public override void End()
        //{
        //}

        public override void OnUpdate()
        {
            Tick();
        }

        public void SetGameObject(GameObject gameObject)
        {
            _goToGameObject = gameObject;
        }
    }
}
