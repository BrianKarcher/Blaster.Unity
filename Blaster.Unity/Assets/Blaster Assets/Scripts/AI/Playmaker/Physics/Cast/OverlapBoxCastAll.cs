using BlueOrb.Scripts.AI.AtomActions;
using HutongGames.PlayMaker;
using BlueOrb.Common.Container;
using UnityEngine;
using TooltipAttribute = HutongGames.PlayMaker.TooltipAttribute;
using System;
using BlueOrb.Physics.Helpers;
using BlueOrb.Controller.Block;
using System.Collections.Generic;

namespace BlueOrb.Scripts.AI.Playmaker
{
    [ActionCategory("BlueOrb.Physics")]
    [Tooltip("Cast an OverlapBox Cast All. We have our own BlueOrb version to support Entities.")]
    public class OverlapBoxCastAll : BasePlayMakerAction
    {
        [RequiredField]
        [Tooltip("The main GameObject.")]
        public FsmOwnerDefault gameObject;

        [Tooltip("Offset in local space.")]
        public FsmVector3 Offset;

        public FsmVector3 HalfExtents;

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
        public FsmArray storeEntity;

        //[UIHint(UIHint.Variable)]
        //[Tooltip("Store collider hit")]
        //public FsmGameObject storeCollider;

        //[UIHint(UIHint.Variable)]
        //[Tooltip("Store location of hit")]
        //public FsmVector3 storePosition;

        // var hit = UnityEngine.Physics.Raycast(origin, dirVector, Distance, _layerMask);

        public bool everyFrame = true;
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
            _layerMask = ActionHelpers.LayerArrayToLayerMask(Layer, false);
            //_atom.SetLayerMask();
            //_atom.Start(entity);
            Tick();
            if (!everyFrame)
                Finish();
        }

        public override void OnUpdate()
        {
            base.OnUpdate();
            Tick();
        }

        private Collider[] _results = new Collider[100];

        private void Tick()
        {
            //bool isTrue = false;
            var worldSpaceOffset = _entity.transform.TransformPoint(Offset.Value);
            //var pos = _entity.GetPosition() + worldSpaceOffset;
            //var dir = newPos - _previosPosition;
            var numHit = UnityEngine.Physics.OverlapBoxNonAlloc(worldSpaceOffset, HalfExtents.Value, _results, _entity.transform.rotation, _layerMask);
            //var hit = UnityEngine.Physics.SphereCast(_previosPosition, Radius.Value, dir, out var hitInfo, dir.magnitude, _layerMask);
            //var hit = UnityEngine.Physics.Raycast(_previosPosition, dir, out var hitInfo, dir.magnitude, _layerMask);
            //DrawBoxCast.DrawBox(_previosPosition, )
            //Debug.DrawLine(_previosPosition, _previosPosition + dir, hit ? Color.red : Color.green, 5.0f);
            //Debug.DrawLine(_previosPosition + (_entity.transform.right * -1f), _previosPosition + dir + (_entity.transform.right * -1f), hit ? Color.red : Color.green, 5.0f);
            //Debug.DrawLine(_previosPosition + _entity.transform.right, _previosPosition + _entity.transform.right + dir, hit ? Color.red : Color.green, 5.0f);
            //DrawBox();

            if (numHit == 0)
            {
                Fsm.Event(NotHitEvent);
                return;
            }

            HashSet<int> itemsAdded = new HashSet<int>();
            //Debug.Log($"Trailing Raycast hit {hitInfo.collider?.attachedRigidbody?.gameObject?.name}!");
            if (!storeEntity.IsNone)
            {
                List<UnityEngine.Object> lstHitObjects = new List<UnityEngine.Object>();
                for (int i = 0; i < numHit; i++)
                {
                    var collider = _results[i];
                    // Get the Entity hit
                    //GameObject hitGameObject;
                    var hitGameObject = collider.GetComponent<EntityItemComponent>()?.GetComponentRepository().gameObject;
                    if (hitGameObject == null)
                    {
                        hitGameObject = collider.attachedRigidbody?.gameObject;
                    }
                    if (hitGameObject == null)
                    {
                        hitGameObject = collider.gameObject;
                    }
                    if (hitGameObject == null)
                        Debug.Log($"(OverlapBoxCastAll) Could not locate an Entity for {collider.name}");
                    else
                        Debug.Log($"OverlapBoxCastAll - Hit {hitGameObject.name}");

                    // Ensure we do not add the same entity twice
                    if (itemsAdded.Contains(hitGameObject.GetInstanceID()))
                        continue;
                    itemsAdded.Add(hitGameObject.GetInstanceID());
                    lstHitObjects.Add(hitGameObject);
                }
                storeEntity.objectReferences = lstHitObjects.ToArray();
            }
            //if (!storeCollider.IsNone)
            //{
            //    storeCollider.Value = hitInfo.collider.gameObject;
            //}
                
            //if (!storePosition.IsNone)
            //{
            //    storePosition.Value = hitInfo.point;
            //}
            if (HitEvent != null)
                Fsm.Event(HitEvent);

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
