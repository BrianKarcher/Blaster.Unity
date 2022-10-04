using Assets.Blaster_Assets.Scripts.Components;
using BlueOrb.Scripts.AI.Playmaker;
using HutongGames.PlayMaker;
using UnityEngine;

namespace BlueOrb.Scripts.AI.PlayMaker.DollyCart
{
    [ActionCategory("BlueOrb.DollyCart")]
    //[Tooltip("Returns Success if Button is pressed.")]
    public class DollyCartStopped : BasePlayMakerAction
    {
        [RequiredField]
        public FsmOwnerDefault gameObject;
        private DollyCartComponent dollyCart;
        public FsmEvent Go;

        public override void Reset()
        {
            gameObject = null;
        }

        public override void OnPreprocess()
        {
            base.OnPreprocess();
        }

        public override void OnEnter()
        {
            Debug.Log("DollyCartStopped Entered");
            var go = Fsm.GetOwnerDefaultTarget(gameObject);
            if (go == null)
            {
                return;
            }

            var entity = base.GetEntityBase(go);
            dollyCart ??= entity.GetComponent<DollyCartComponent>();
            if (dollyCart == null)
            {
                Debug.LogError($"Could not locate Dolly Cart Component");
                return;
            }
            this.dollyCart.Stop();
        }

        public override void OnExit()
        {
            base.OnExit();
            Debug.Log("DollyCartStopped Exited");
        }
    }
}