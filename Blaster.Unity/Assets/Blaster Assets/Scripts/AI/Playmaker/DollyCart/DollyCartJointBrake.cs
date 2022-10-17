using BlueOrb.Scripts.AI.Playmaker;
using HutongGames.PlayMaker;
using BlueOrb.Controller;
using UnityEngine;
using Tooltip = HutongGames.PlayMaker.TooltipAttribute;

namespace BlueOrb.Scripts.AI.PlayMaker.DollyCart
{
    [ActionCategory("BlueOrb.DollyCartJoint")]
    [Tooltip("Brake cart to zero.")]
    public class DollyCartJointBrake : BasePlayMakerAction
    {
        [RequiredField]
        public FsmOwnerDefault gameObject;
        private DollyCartJointComponent dollyCart;

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
            Debug.Log("DollyCartJointBrake Entered");
            var go = Fsm.GetOwnerDefaultTarget(gameObject);
            if (go == null)
            {
                return;
            }

            var entity = base.GetEntityBase(go);
            dollyCart ??= entity.GetComponent<DollyCartJointComponent>();
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
            Debug.Log("DollyCartJointBrake Exited");
            dollyCart?.SetBrake(false);
        }

        public override void OnFixedUpdate()
        {
            base.OnFixedUpdate();
            if (dollyCart == null)
            {
                return;
            }
            if (dollyCart.Speed < 0.001f)
            {
                dollyCart.Stop();
                Finish();
            }
        }
    }
}