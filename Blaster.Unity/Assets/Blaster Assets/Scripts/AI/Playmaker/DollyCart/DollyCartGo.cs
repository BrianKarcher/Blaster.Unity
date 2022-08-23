using BlueOrb.Scripts.AI.Playmaker;
using HutongGames.PlayMaker;
using BlueOrb.Controller;
using UnityEngine;
using Tooltip = HutongGames.PlayMaker.TooltipAttribute;

namespace BlueOrb.Scripts.AI.PlayMaker.DollyCart
{
    [ActionCategory("BlueOrb.DollyCart")]
    [Tooltip("Dolly cart driving!.")]
    public class DollyCartGo : BasePlayMakerAction
    {
        [RequiredField]
        public FsmOwnerDefault gameObject;

        //[UIHint(UIHint.Layer)]
        //[HutongGames.PlayMaker.Tooltip("Layers to check.")]
        //public FsmInt[] Layer;

        public FsmEvent EnemyCollision;

        private DollyCartComponent dollyCart;
        //private int layerMask;
        private Collider[] colliders = new Collider[20];

        public override void Reset()
        {
            gameObject = null;
        }

        public override void OnPreprocess()
        {
            base.OnPreprocess();
            Fsm.HandleFixedUpdate = true;
        }

        public override void OnEnter()
        {
            Debug.Log("Entering DollyCartGo");
            var go = Fsm.GetOwnerDefaultTarget(gameObject);
            if (go == null)
            {
                return;
            }

            var entity = base.GetEntityBase(go);
            dollyCart ??= entity.GetComponent<DollyCartComponent>();

            //layerMask = ActionHelpers.LayerArrayToLayerMask(Layer, false);
            dollyCart.StartAcceleration(new DollyCartComponent.SetSpeedData()
            {
                SmoothTime = dollyCart.SmoothTime,
                TargetSpeed = dollyCart.GetTargetSpeed
            });
        }

        public override void OnFixedUpdate()
        {
            base.OnFixedUpdate();
            if (CheckEnemyCollision())
            {
                Fsm.Event(EnemyCollision);
                return;
            }
            dollyCart.ProcessDollyCartSpeedChange();
        }

        private bool CheckEnemyCollision()
        {
            int count = UnityEngine.Physics.OverlapBoxNonAlloc(dollyCart.transform.TransformPoint(dollyCart.EnemyCheckOffset),
                dollyCart.EnemyCheckHalfExtents, colliders, Quaternion.identity);
            for (int i = 0; i < count; i++)
            {
                if (this.colliders[i].CompareTag(dollyCart.EnemyTag))
                {
                    return true;
                }
            }
            return false;
        }
    }
}