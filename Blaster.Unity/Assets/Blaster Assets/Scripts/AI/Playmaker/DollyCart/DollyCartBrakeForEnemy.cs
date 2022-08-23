using BlueOrb.Scripts.AI.Playmaker;
using HutongGames.PlayMaker;
using BlueOrb.Controller;
using UnityEngine;
using Tooltip = HutongGames.PlayMaker.TooltipAttribute;

namespace BlueOrb.Scripts.AI.PlayMaker.DollyCart
{
    [ActionCategory("BlueOrb.DollyCart")]
    [Tooltip("Enemy in the way, brake cart to zero.")]
    public class DollyCartBrakeForEnemy : BasePlayMakerAction
    {
        [RequiredField]
        public FsmOwnerDefault gameObject;
        private DollyCartComponent dollyCart;

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
            Debug.Log("Entering DollyCartBrakeForEnemy");
            var go = Fsm.GetOwnerDefaultTarget(gameObject);
            if (go == null)
            {
                return;
            }

            var entity = base.GetEntityBase(go);
            if (dollyCart == null)
            {
                dollyCart = entity.GetComponent<DollyCartComponent>();
            }
            if (dollyCart == null)
            {
                Debug.LogError($"Could not locate Dolly Cart Component");
                return;
            }
            dollyCart.Brake();
        }

        public override void OnFixedUpdate()
        {
            base.OnFixedUpdate();
            if (dollyCart == null)
            {
                return;
            }
            dollyCart.ProcessDollyCartSpeedChange();
            if (dollyCart.GetSpeed() < 0.001f)
            {
                dollyCart.Stop();
                Finish();
            }
        }
    }
}