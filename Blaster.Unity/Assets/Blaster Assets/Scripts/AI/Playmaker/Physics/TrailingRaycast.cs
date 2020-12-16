using BlueOrb.Scripts.AI.AtomActions;
using HutongGames.PlayMaker;
using BlueOrb.Common.Container;
using UnityEngine;
using TooltipAttribute = HutongGames.PlayMaker.TooltipAttribute;
using System;

namespace BlueOrb.Scripts.AI.Playmaker
{
    [ActionCategory("RQ.Physics")]
    [Tooltip("Cast a trailing raycast. This raycast is used for fast moving objects to prevent tunneling.")]
    public class TrailingRaycast : BasePlayMakerAction
    {
        [RequiredField]
        [Tooltip("The main GameObject.")]
        public FsmOwnerDefault gameObject;

        //public RaycastAtom _atom;

        [UIHint(UIHint.Layer)]
        [Tooltip("Layers to check.")]
        public FsmInt[] Layer;

        //[UIHint(UIHint.TagMenu)]
        //[Tooltip("Filter by Tag.")]
        //public FsmString collideTag;

        [UIHint(UIHint.Variable)]
        [Tooltip("Fire when raycast hits.")]
        public FsmEvent HitEvent;

        [UIHint(UIHint.Variable)]
        [Tooltip("Fire when raycast does not hit.")]
        public FsmEvent NotHitEvent;

        [UIHint(UIHint.Variable)]
        [Tooltip("Store entity hit")]
        public FsmGameObject storeEntity;

        [UIHint(UIHint.Variable)]
        [Tooltip("Store location of hit")]
        public FsmVector3 storePosition;

        // var hit = UnityEngine.Physics.Raycast(origin, dirVector, Distance, _layerMask);

        //public bool everyFrame;
        private Vector3 _previosPosition;
        private IEntity _entity;
        private int _layerMask;

        public override void Reset()
        {
            gameObject = null;
        }

        public override void OnEnter()
        {
            var go = Fsm.GetOwnerDefaultTarget(gameObject);
            if (go == null)
            {
                return;
            }

            _entity = base.GetRepo(go);
            _previosPosition = _entity.GetPosition();
            _layerMask = ActionHelpers.LayerArrayToLayerMask(Layer, false);
            //_atom.SetLayerMask();
            //_atom.Start(entity);
            //Tick();
            //if (!everyFrame)
            //    Finish();
        }

        public override void OnUpdate()
        {
            base.OnUpdate();
            Tick();
        }

        private void Tick()
        {
            //bool isTrue = false;
            var newPos = _entity.GetPosition();
            var dir = newPos - _previosPosition;
            var hit = UnityEngine.Physics.Raycast(_previosPosition, dir, out var hitInfo, dir.magnitude, _layerMask);
            Debug.DrawLine(_previosPosition, _previosPosition + dir, hit ? Color.red : Color.green, 5.0f);

            if (hit)
            {
                Debug.Log($"Trailing Raycast hit {hitInfo.collider?.attachedRigidbody?.gameObject?.name}!");
                if (!storeEntity.IsNone)
                {
                    // Get the Entity hit
                    var hitGameObject = hitInfo.collider?.attachedRigidbody?.gameObject;
                    if (hitGameObject == null)
                    {
                        hitGameObject = hitInfo.collider.gameObject;
                    }
                    if (hitGameObject == null)
                        throw new Exception("(TrailingRaycast) Could not locate a GameObject");
                    storeEntity.Value = hitGameObject;
                }
                if (!storePosition.IsNone)
                {
                    storePosition.Value = hitInfo.point;
                }
                if (HitEvent != null)
                    Fsm.Event(HitEvent);
            }
            else
            {
                Fsm.Event(NotHitEvent);
            }

            // Update previous position with new
            _previosPosition = newPos;

            //if (_atom.Check())
            //{
            //    //isTrue = true;

            //    //Finish();
            //}
            //else
            //{
            //    isTrue = false;

            //}
            
            //if (!storeResult.IsNone)
            //{
            //    storeResult.Value = isTrue;
            //}
        }

        public override void OnExit()
        {
            base.OnExit();
            //_atom.End();
        }
    }
}
