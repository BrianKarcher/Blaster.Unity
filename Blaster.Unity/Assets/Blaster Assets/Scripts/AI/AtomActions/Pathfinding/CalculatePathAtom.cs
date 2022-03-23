using Pathfinding;
using BlueOrb.Common.Container;
using BlueOrb.Controller.Inventory;
using BlueOrb.Controller.Physics;
using System;
using System.Collections.Generic;
using UnityEngine;
using BlueOrb.Messaging;

namespace BlueOrb.Scripts.AI.AtomActions.Physics
{
    [Serializable]
    public class CalculatePathAtom : AtomActionBase
    {
        public Vector3 Target { get; set; }
        private List<Vector3> _path;
        public List<Vector3> Path => _path;
        private AIComponent _aiComponent;
        private Seeker _seeker;
        private bool _isPathError;
        public bool IsPathError => _isPathError;
        public float Delay = 0f;
        public bool Repeat = false;
        public float RepeatDelay = 0f;
        public bool ClosestOnPathCheck = true;
        private float _timeToCalculate;
        private bool _canSearchAgain = true;
        // Determines the distance you can be from a waypoint before triggering the next one
        //public float WaypointSeekDist = .08f;
        //the distance (squared) a vehicle has to be from a path waypoint before
        //it starts seeking to the next waypoint
        public float WaypointSeekDist;

        public override void Start(IEntity entity)
        {
            base.Start(entity);
            _isPathError = false;
            if (_seeker == null)
            {
                _aiComponent = entity.Components.GetComponent<AIComponent>();
                _seeker = _aiComponent.Seeker;
            }
            _timeToCalculate = Time.time + Delay;
        }

        public override void OnUpdate()
        {
            base.OnUpdate();
            if (Time.time > _timeToCalculate && _canSearchAgain)
            {
                CalculatePath();
            }
        }

        private void CalculatePath()
        {
            _canSearchAgain = false;
            _seeker.StartPath(_entity.GetFootPosition(), Target, OnPathComplete);
            //var newPath = _seeker.GetNewPath(_entity.GetFootPosition(), Target);
            //OnPathComplete(newPath);
        }

        public override void End()
        {
            base.End();
            _seeker.CancelCurrentPathRequest();
        }

        public void OnPathComplete(Path p)
        {
            ABPath abp = p as ABPath;

            if (abp == null) throw new System.Exception("This function only handles ABPaths, do not use special path types");

            _canSearchAgain = true;

            abp.Claim(_entity);

            if (abp.error)
            {
                abp.Release(_entity);
                _path = null;
                return;
            }

            //Debug.Log("CalculatePath returned a new path successfully.");
            // Yay, now we can get a Vector3 representation of the path
            // from p.vectorPath
            _path = abp.vectorPath;

            int waypoint = CalculateWaypointToStartOn(abp);

            MessageDispatcher.Instance.DispatchMsg("NewPath", 0f, _entity.GetId(), _entity.GetId(), (_path, waypoint));
            if (!Repeat)
            {
                Finish();
                return;
            }
            _timeToCalculate = Time.time + RepeatDelay;
        }

        private int CalculateWaypointToStartOn(ABPath p)
        {
            // Simulate movement from the point where the path was requested
            // to where we are right now. This reduces the risk that the agent
            // gets confused because the first point in the path is far away
            // from the current position (possibly behind it which could cause
            // the agent to turn around, and that looks pretty bad).
            //Vector3 p1 = Time.time - lastFoundWaypointTime < 0.3f ? lastFoundWaypointPosition : p.originalStartPoint;
            Vector3 p1 = p.originalStartPoint;
            Vector3 p2 = _entity.GetFootPosition();
            Vector3 dir = p2 - p1;
            float magn = dir.magnitude;
            dir /= magn;
            int steps = (int)(magn / WaypointSeekDist);

#if ASTARDEBUG
			Debug.DrawLine(p1, p2, Color.red, 1);
#endif
            List<Vector3> vPath = p.vectorPath;

            int waypoint = 0;

            for (int i = 0; i <= steps; i++)
            {
                waypoint = WaypointIndexCheck(waypoint, vPath, p1);
                //CalculateVelocity(p1);
                p1 += dir;
            }

            return waypoint;
        }

        private int WaypointIndexCheck(int index, List<Vector3> vPath, Vector3 currentPosition)
        {
            int newIndex = index;
            while (true)
            {
                if (newIndex < vPath.Count - 1)
                {
                    //There is a "next path segment"
                    float dist = XZSqrMagnitude(vPath[index], currentPosition);
                    //Mathfx.DistancePointSegmentStrict (vPath[currentWaypointIndex+1],vPath[currentWaypointIndex+2],currentPosition);
                    if (dist < WaypointSeekDist * WaypointSeekDist)
                    {
                        //lastFoundWaypointPosition = currentPosition;
                        //lastFoundWaypointTime = Time.time;
                        newIndex++;
                    }
                    else
                    {
                        return newIndex;
                    }
                }
                else
                {
                    return newIndex;
                }
            }
        }

        protected float XZSqrMagnitude(Vector3 a, Vector3 b)
        {
            float dx = b.x - a.x;
            float dz = b.z - a.z;

            return dx * dx + dz * dz;
        }

        public Vector4[] GetPath()
        {
            Vector4[] vector4s = new Vector4[_path.Count];
            for (int i = 0; i < _path.Count; i++)
            {
                var tempVector = _path[i];
                vector4s[i] = new Vector4(tempVector.x, tempVector.y, tempVector.z);
            }
            return vector4s;
        }
    }
}
