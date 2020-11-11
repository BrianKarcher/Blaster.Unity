using BlueOrb.Common.Container;
using BlueOrb.Physics;
using UnityEngine;

namespace BlueOrb.Scripts.AI.AtomActions
{
    public class CanSeeAtom : AtomActionBase
    {
        public enum CanSeeTargetEnum
        {
            EntityTarget = 0
        }

        public CanSeeTargetEnum CanSeeTarget;
        public bool CheckLOS;
        public bool CheckDistance;
        public bool CheckFOV;

        private PhysicsComponent _physicsComponent;
        //protected EntityCommonComponent _entityCommon;
        private int _obstacleLayerMask = -1;
        //private bool _result;

        public override void Start(IEntity entity)
        {
            base.Start(entity);
            if (_physicsComponent == null)
                _physicsComponent = entity.Components.GetComponent<PhysicsComponent>();
            //if (_entityCommon == null)
            //    _entityCommon = ent
        }

        //public override void OnUpdate()
        //{
        //    base.OnUpdate();
        //    var target = GetTarget();
        //    _result = Tick();
        //}

        public bool Check()
        {
            var target = GetTarget();
            if (target == null)
                return false;

            if (CheckDistance)
            {
                if (!_physicsComponent.Controller.CheckLOSDistance(target))
                    return false;
                //else
                //    Debug.Log("Passed Distance Check");
            }

            if (CheckFOV)
            {
                if (!_physicsComponent.Controller.PosInFOV3(target.transform.position))
                    return false;
                else
                {
                    //Debug.Log("Passed FOV Check");
                }
            }

            if (CheckLOS)
            {
                if (!_physicsComponent.Controller.HasLineOfSight(target, _obstacleLayerMask))
                    return false;
                else
                {
                    //Debug.Log("Passed LOS Check");
                }
            }

            return true;
        }

        private GameObject GetTarget()
        {
            switch (CanSeeTarget)
            {
                case CanSeeTargetEnum.EntityTarget:
                    return _entity.Target;
            }
            throw new System.Exception("Target not found!");
        }

        public void SetObstacleLayerMask(int layerMask)
        {
            _obstacleLayerMask = layerMask;
        }
    }
}
