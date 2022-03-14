using HutongGames.PlayMaker;
using BlueOrb.Common.Container;

namespace BlueOrb.Scripts.AI.Playmaker.Input
{
    [ActionCategory("BlueOrb")]
    [HutongGames.PlayMaker.Tooltip("Wait until level has begun")]
    public class WaitForLevelBegin : FsmStateAction
    {
        [RequiredField]
        public FsmOwnerDefault gameObject;

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
            var entity = go.GetComponent<IEntity>();
            if (CheckComplete())
            {
                Finish();
            }
        }

        public override void OnUpdate()
        {
            if (CheckComplete())
            {
                Finish();
            }
        }

        private bool CheckComplete()
        {
            return EntityContainer.Instance.LevelStateController.HasLevelBegun;
        }
    }
}
