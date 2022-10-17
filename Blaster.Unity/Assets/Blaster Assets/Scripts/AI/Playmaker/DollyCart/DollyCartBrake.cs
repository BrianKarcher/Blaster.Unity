using BlueOrb.Scripts.AI.Playmaker;
using HutongGames.PlayMaker;
using UnityEngine;
using Tooltip = HutongGames.PlayMaker.TooltipAttribute;
using Assets.Blaster_Assets.Scripts.Components;

namespace BlueOrb.Scripts.AI.PlayMaker.DollyCart
{
    [ActionCategory("BlueOrb.DollyCart")]
    [Tooltip("Brake cart to zero.")]
    public class DollyCartBrake : BasePlayMakerAction
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
            Debug.Log("DollyCartBrake Entered");
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
            dollyCart.SetBrake(true);
        }

        public override void OnExit()
        {
            base.OnExit();
            Debug.Log("DollyCartBrake Exited");
            dollyCart?.SetBrake(false);
        }

        public override void OnFixedUpdate()
        {
            base.OnFixedUpdate();
            if (dollyCart == null)
            {
                return;
            }
            dollyCart.ProcessDollyCartSpeedChange();
            if (dollyCart.Speed < 0.001f)
            {
                dollyCart.Stop();
                Finish();
            }
        }
    }
}