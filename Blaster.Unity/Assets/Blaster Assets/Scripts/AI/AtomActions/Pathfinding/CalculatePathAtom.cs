using Pathfinding;
using BlueOrb.Common.Container;
using BlueOrb.Controller.Inventory;
using BlueOrb.Controller.Physics;
using System;
using System.Collections.Generic;
using UnityEngine;

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
        private float _timeToCalculate;

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
            if (Time.time > _timeToCalculate && _seeker.IsDone())
            {
                CalculatePath();
            }
        }

        private void CalculatePath()
        {
            _seeker.StartPath(_entity.GetFootPosition(), Target, OnPathComplete);
        }

        public override void End()
        {
            base.End();
            _seeker.CancelCurrentPathRequest();
        }

        public void OnPathComplete(Path p)
        {
            //Debug.Log("CalculatePath OnPathComplete called.");
            //We got our path back
            if (p.error)
            {
                Debug.Log("CalculatePath returned an error.");
                // Nooo, a valid path couldn't be found
                _isPathError = true;
                Finish();
            }
            else
            {
                Debug.Log("CalculatePath returned a new path successfully.");
                // Yay, now we can get a Vector3 representation of the path
                // from p.vectorPath
                _path = p.vectorPath;
                Finish();
            }
        }
    }
}
