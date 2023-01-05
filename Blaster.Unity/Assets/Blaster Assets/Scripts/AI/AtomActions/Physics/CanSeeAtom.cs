using BlueOrb.Base.Extensions;
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
        public float losDistance;
        public bool CheckDistance;
        public bool CheckFOV;
        public float fovDegrees = 30f;

        private IPhysicsComponent _physicsComponent;
        //protected EntityCommonComponent _entityCommon;
        private int _obstacleLayerMask = -1;
        private float losDistanceSq;
        private IEntity entity;
        //private bool _result;

        public override void Start(IEntity entity)
        {
            base.Start(entity);
            this.entity = entity;
            if (_physicsComponent == null)
                _physicsComponent = entity.Components.GetComponent<IPhysicsComponent>();
            this.losDistanceSq = this.losDistance * this.losDistance;
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
                if (!PhysicsHelper.CheckLOSDistance(entity.gameObject, target, losDistanceSq))
                    return false;
                //else
                //    Debug.Log("Passed Distance Check");
            }

            if (CheckFOV)
            {
                if (!PhysicsHelper.PosInFOV2(_entity.transform.position.xz(), _entity.transform.forward.xz(), target.xz(), fovDegrees))
                    return false;
                //else
                //{
                //    //Debug.Log("Passed FOV Check");
                //}
            }

            if (CheckLOS)
            {
                if (!PhysicsHelper.HasLineOfSight(entity.GetHeadPosition(), target, _obstacleLayerMask))
                    return false;
                //else
                //{
                //    //Debug.Log("Passed LOS Check");
                //}
            }

            return true;
        }

        private Vector3 GetTarget()
        {
            switch (CanSeeTarget)
            {
                case CanSeeTargetEnum.EntityTarget:
                    IEntity target = _entity.Target.GetComponent<IEntity>();
                    if (target != null)
                        return target.GetHeadPosition();
                    return _entity.Target.transform.position;
            }
            throw new System.Exception("Target not found!");
        }

        public void SetObstacleLayerMask(int layerMask)
        {
            _obstacleLayerMask = layerMask;
        }
    }
}