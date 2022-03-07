using HutongGames.PlayMaker;
using BlueOrb.Common.Container;
using UnityEngine;
using TooltipAttribute = HutongGames.PlayMaker.TooltipAttribute;
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

        [UIHint(UIHint.Layer)]
        [Tooltip("Layers to check.")]
        public FsmInt[] Layer;

        [UIHint(UIHint.Variable)]
        [Tooltip("Fire when raycast hits.")]
        public FsmEvent HitEvent;

        [UIHint(UIHint.Variable)]
        [Tooltip("Fire when raycast does not hit.")]
        public FsmEvent NotHitEvent;

        [UIHint(UIHint.Variable)]
        [Tooltip("Store entity hit")]
        public FsmArray storeEntity;

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
            var worldSpaceOffset = _entity.transform.TransformPoint(Offset.Value);
            var numHit = UnityEngine.Physics.OverlapBoxNonAlloc(worldSpaceOffset, HalfExtents.Value, _results, _entity.transform.rotation, _layerMask);

            if (numHit == 0)
            {
                Fsm.Event(NotHitEvent);
                return;
            }

            HashSet<int> itemsAdded = new HashSet<int>();
            itemsAdded.Clear();
            if (!storeEntity.IsNone)
            {
                List<UnityEngine.Object> lstHitObjects = new List<UnityEngine.Object>();
                for (int i = 0; i < numHit; i++)
                {
                    var collider = _results[i];
                    // Get the Entity hit
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
            if (HitEvent != null)
                Fsm.Event(HitEvent);
        }

        public override void OnExit()
        {
            base.OnExit();
        }
    }
}