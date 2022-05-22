using HutongGames.PlayMaker;
using BlueOrb.Common.Container;
using BlueOrb.Messaging;

namespace BlueOrb.Scripts.AI.Playmaker.Input
{
    [ActionCategory("BlueOrb.GameSettings")]
    [HutongGames.PlayMaker.Tooltip("Set Level Begin variable")]
    public class SetLevelBegin : BasePlayMakerAction
    {
        [RequiredField]
        public FsmOwnerDefault gameObject;
        public FsmString LevelBeginMessage;

        public override void Reset()
        {
            gameObject = null;
            LevelBeginMessage = "SetLevelBegin";
        }

        public override void OnEnter()
        {
            var go = Fsm.GetOwnerDefaultTarget(gameObject);
            if (go == null)
            {
                return;
            }

            var entity = base.GetEntityBase(go);
            MessageDispatcher.Instance.DispatchMsg(LevelBeginMessage.Value, 0f, entity.GetId(), entity.GetId(), null);
            Finish();
        }
    }
}
