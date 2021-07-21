using BlueOrb.Scripts.AI.AtomActions;
using HutongGames.PlayMaker;
using BlueOrb.Base.Manager;
using BlueOrb.Common.Container;
using BlueOrb.Controller.Scene;
using BlueOrb.Controller.Manager;
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
            var player = EntityContainer.Instance.GetMainCharacter();
            MessageDispatcher.Instance.DispatchMsg("SetProjectile", 0f, null, player.GetId(), Projectile.Value);
            Finish();
        }
    }
}
