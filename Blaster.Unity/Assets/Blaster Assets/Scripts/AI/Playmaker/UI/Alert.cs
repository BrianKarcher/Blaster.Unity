using HutongGames.PlayMaker;
using BlueOrb.Common.Container;
using BlueOrb.Messaging;

namespace BlueOrb.Scripts.AI.Playmaker
{
    [ActionCategory("BlueOrb.UI")]
    [HutongGames.PlayMaker.Tooltip("Alert")]
    public class Alert : BasePlayMakerAction
    {
        [RequiredField]
        public FsmOwnerDefault gameObject;

        [RequiredField]
        public FsmString AlertMessage;

        private IEntity entity;

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
            this.entity = base.GetEntityBase(go);
            MessageDispatcher.Instance.DispatchMsg("Alert", 0f, null, "Hud Controller", AlertMessage.Value);
            Finish();
        }
    }
}