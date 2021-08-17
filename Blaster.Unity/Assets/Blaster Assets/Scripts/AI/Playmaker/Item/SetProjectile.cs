using HutongGames.PlayMaker;
using BlueOrb.Messaging;

namespace BlueOrb.Scripts.AI.PlayMaker.Item
{
    [ActionCategory("BlueOrb.Item")]
    public class SetProjectile : FsmStateAction
    {
        [Tooltip("Projectile to set.")]
        public FsmGameObject Projectile;

        public override void OnEnter()
        {
            //var player = EntityContainer.Instance.GetMainCharacter();
            //MessageDispatcher.Instance.DispatchMsg("SetProjectile", 0f, null, player.GetId(), Projectile.Value);

            MessageDispatcher.Instance.DispatchMsg("SetProjectile", 0f, null, "Level State", Projectile.Value);

            // Broadcast it
            //MessageDispatcher.Instance.DispatchMsg("SetProjectile", 0f, null, null, Projectile.Value);
            Finish();
        }
    }
}
