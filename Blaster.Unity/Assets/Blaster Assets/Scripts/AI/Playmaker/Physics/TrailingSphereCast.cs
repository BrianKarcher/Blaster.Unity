﻿using BlueOrb.Scripts.AI.AtomActions;
using HutongGames.PlayMaker;
using BlueOrb.Common.Container;
using UnityEngine;
using TooltipAttribute = HutongGames.PlayMaker.TooltipAttribute;
using System;
using BlueOrb.Physics.Helpers;

namespace BlueOrb.Scripts.AI.Playmaker
{
    [ActionCategory("RQ.Physics")]
    [Tooltip("Cast a trailing raycast. This raycast is used for fast moving objects to prevent tunneling.")]
    public class TrailingSphereCast : BasePlayMakerAction
    {
        [RequiredField]
        [Tooltip("The main GameObject.")]
        public FsmOwnerDefault gameObject;

        public FsmFloat Radius;

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
        [Tooltip("Store collider hit")]
        public FsmGameObject storeCollider;

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
            var hit = UnityEngine.Physics.SphereCast(_previosPosition, Radius.Value, dir, out var hitInfo, dir.magnitude, _layerMask);
            //var hit = UnityEngine.Physics.Raycast(_previosPosition, dir, out var hitInfo, dir.magnitude, _layerMask);
            //DrawBoxCast.DrawBox(_previosPosition, )
            Debug.DrawLine(_previosPosition, _previosPosition + dir, hit ? Color.red : Color.green, 5.0f);
            Debug.DrawLine(_previosPosition + (_entity.transform.right * -1f), _previosPosition + dir + (_entity.transform.right * -1f), hit ? Color.red : Color.green, 5.0f);
            Debug.DrawLine(_previosPosition + _entity.transform.right, _previosPosition + _entity.transform.right + dir, hit ? Color.red : Color.green, 5.0f);
            //DrawBox();

            if (hit)
            {
                Debug.Log($"Trailing Raycast hit {hitInfo.collider?.attachedRigidbody?.gameObject?.name}!");
                if (!storeEntity.IsNone)
                {
                    // Get the Entity hit
                    var hitGameObject = hitInfo.collider?.attachedRigidbody?.gameObject;
                    if (hitGameObject == null)
                    {
                        // TODO - Create a better way to get the Entity (gameObject that contains EntityCommon) that was hit
                        // Perhaps by using an empty "EntityTenticle" component or similar.
                        hitGameObject = hitInfo.collider.gameObject;
                    }
                    if (hitGameObject == null)
                        throw new Exception("(TrailingRaycast) Could not locate a GameObject");
                    storeEntity.Value = hitGameObject;
                }
                if (!storeCollider.IsNone)
                {
                    storeCollider.Value = hitInfo.collider.gameObject;
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
