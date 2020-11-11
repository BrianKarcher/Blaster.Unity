using BlueOrb.Common.Container;
using BlueOrb.Physics;
using UnityEngine;

namespace BlueOrb.Scripts.AI.AtomActions
{
    public enum Space
    {
        Self = 0,
        World = 1
    }

    public class RaycastAtom : AtomActionBase
    {
        public Space Space;
        public float Distance;
        public bool Debug;

        private PhysicsComponent _physicsComponent;
        //protected EntityCommonComponent _entityCommon;
        private int _layerMask = -1;
        public Vector3 _offset;
        public Vector3 _direction;
        public Color _debugColor;

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
            var origin = _entity.transform.position + _offset;
            var dirVector = _direction;
            if (Space == Space.Self)
            {
                dirVector = _entity.transform.TransformDirection(_direction);
            }
            var hit = UnityEngine.Physics.Raycast(origin, dirVector, Distance, _layerMask);

            if (Debug)
            {
                var debugRayLength = Mathf.Min(Distance, 1000);
                UnityEngine.Debug.DrawLine(origin, origin + dirVector * debugRayLength, _debugColor);
            }

            return hit;
        }

        public void SetLayerMask(int layerMask)
        {
            _layerMask = layerMask;
        }

        //public void SetOffset(Vector3 offset)
        //{
        //    _offset = offset;
        //}

        //public void SetDirection(Vector3 direction)
        //{
        //    _direction = direction;
        //}

        public void SetDebugColor(Color color)
        {
            _debugColor = color;
        }
    }
}
