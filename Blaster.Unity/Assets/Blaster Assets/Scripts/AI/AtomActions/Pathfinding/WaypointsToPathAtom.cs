using BlueOrb.Common.Container;
using BlueOrb.Controller.Physics;
using System;
using UnityEngine;

namespace BlueOrb.Scripts.AI.AtomActions.Physics
{
    [Serializable]
    public class WaypointsToPathAtom : AtomActionBase
    {
        private Vector3[] _path;
        public Vector3[] Path { get { return _path; } }
        private WaypointComponent _waypointComponent;

        public override void Start(IEntity entity)
        {
            base.Start(entity);
            if (_waypointComponent == null)
                _waypointComponent = entity.Components.GetComponent<WaypointComponent>();
            var waypointsList = _waypointComponent.GetWaypoints3();
            _path = new Vector3[waypointsList.Count];
            for (int i = 0; i < waypointsList.Count; i++)
            {
                _path[i] = waypointsList[i];
            }
        }
    }
}
