using HutongGames.PlayMaker;
using BlueOrb.Common.Container;

namespace BlueOrb.Scripts.AI.Playmaker.Input
{
    [ActionCategory("BlueOrb")]
    [HutongGames.PlayMaker.Tooltip("Wait until an active enemy count has been reached")]
    public class WaitForEnemyCount : BasePlayMakerAction
    {
        [RequiredField]
        public FsmOwnerDefault gameObject;

        [RequiredField]
        public FsmInt EnemyCount = 6;

        public override void Reset()
        {
            gameObject = null;
            EnemyCount = 6;
        }

        public override void OnEnter()
        {
            var go = Fsm.GetOwnerDefaultTarget(gameObject);
            if (go == null)
            {
                return;
            }

            var entity = base.GetEntityBase(go);
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
            => EntityContainer.Instance.GetEnemies().Count <= this.EnemyCount.Value;
    }
}
