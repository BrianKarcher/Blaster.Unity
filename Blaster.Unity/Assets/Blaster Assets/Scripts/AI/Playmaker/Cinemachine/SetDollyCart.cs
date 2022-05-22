using HutongGames.PlayMaker;
using BlueOrb.Common.Container;
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

            var entity = base.GetEntityBase(go);
            var dollyComponent = entity.Components.GetComponent<DollyCartComponent>();
            dollyComponent.SetDollyCartParent(DollyCart.Value);
            Finish();
        }

        public override void OnExit()
        {
            base.OnExit();
        }
    }
}
