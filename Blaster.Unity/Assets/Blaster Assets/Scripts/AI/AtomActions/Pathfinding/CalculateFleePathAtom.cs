//using Pathfinding;
//using BlueOrb.Common.Container;
//using BlueOrb.Controller.Inventory;
//using BlueOrb.Controller.Physics;
//using System;
//using System.Collections.Generic;
//using UnityEngine;

//namespace BlueOrb.Scripts.AI.AtomActions.Physics
//{
//    [Serializable]
//    public class CalculateFleePathAtom : AtomActionBase
//    {
//        public Vector3 FleeFromTarget { get; set; }
//        private List<Vector3> _path;
//        public List<Vector3> Path => _path;
//        private AIComponent _aiComponent;
//        private Seeker _seeker;
//        private bool _isPathError;
//        public bool IsPathError => _isPathError;
//        public float Delay = 0f;
//        private float _timeToCalculate;

//        public override void Start(IEntity entity)
//        {
//            base.Start(entity);
//            _isPathError = false;
//            if (_seeker == null)
//            {
//                _aiComponent = entity.Components.GetComponent<AIComponent>();
//                _seeker = _aiComponent.Seeker;
//            }
//            _timeToCalculate = Time.time + Delay;            
//        }

//        public override void OnUpdate()
//        {
//            base.OnUpdate();
//            if (Time.time > _timeToCalculate)
//            {
//                CalculatePath();
//            }
//        }

//        private void CalculatePath()
//        {
//            // Call a FleePath call like this, assumes that a Seeker is attached to the GameObject
//            Vector3 thePointToFleeFrom = FleeFromTarget;
//            // The path will be returned when the path is over a specified length (or more accurately when the traversal cost is greater than a specified value).
//            // A score of 1000 is approximately equal to the cost of moving one world unit.
//            int theGScoreToStopAt = 10000;
//            // Create a path object
//            FleePath path = FleePath.Construct(_entity.transform.position, thePointToFleeFrom, theGScoreToStopAt);
//            // This is how strongly it will try to flee, if you set it to 0 it will behave like a RandomPath
//            path.aimStrength = 1;
//            // Determines the variation in path length that is allowed
//            path.spread = 4000;
//            // Get the Seeker component which must be attached to this GameObject
//            //Seeker seeker = GetComponent<Seeker>();
//            // Start the path and return the result to MyCompleteFunction (which is a function you have to define, the name can of course be changed)
//            _seeker.StartPath(path, OnPathComplete);

//            //_seeker.StartPath(_entity.GetFootPosition(), Target, OnPathComplete);
//        }

//        public override void End()
//        {
//            base.End();
//            _seeker.CancelCurrentPathRequest();
//        }

//        public void OnPathComplete(Path p)
//        {
//            //We got our path back
//            if (p.error)
//            {
//                // Nooo, a valid path couldn't be found
//                _isPathError = true;
//                Finish();
//            }
//            else
//            {
//                // Yay, now we can get a Vector3 representation of the path
//                // from p.vectorPath
//                _path = p.vectorPath;
//                Finish();
//            }
//        }
//    }
//}
