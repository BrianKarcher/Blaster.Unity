using HutongGames.PlayMaker;
using static BlueOrb.Controller.DollyCartJointComponent;
using BlueOrb.Controller;

namespace BlueOrb.Scripts.AI.Playmaker.Cinemachine
{
    [ActionCategory("BlueOrb.Cinemachine")]
    [HutongGames.PlayMaker.Tooltip("Set dolly cart.")]
    public class SetDollyCart : BasePlayMakerAction
    {
        [RequiredField]
        public FsmOwnerDefault gameObject;

        public FsmGameObject DollyCart;
        public CartStartPosition CartStartPosition;

        public override void Reset()
        {
            gameObject = null;
            CartStartPosition = CartStartPosition.Reset;
        }

        public override void OnEnter()
        {
            var go = Fsm.GetOwnerDefaultTarget(gameObject);
            if (go == null)
            {
                return;
            }

            var entity = base.GetEntityBase(go);
            var dollyComponent = entity.Components.GetComponent<DollyCartJointComponent>();
            dollyComponent.SetDollyCart(new SetJointData()
            {
                Joint = DollyCart.Value,
                CartStartPosition = CartStartPosition
            });
            Finish();
        }

        public override void OnExit()
        {
            base.OnExit();
        }
    }
}