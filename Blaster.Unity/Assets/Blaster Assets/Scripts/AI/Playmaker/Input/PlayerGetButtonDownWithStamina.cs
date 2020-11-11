using HutongGames.PlayMaker;
using Rewired.Integration.PlayMaker;
using BlueOrb.Common.Container;
using BlueOrb.Controller.Damage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueOrb.Scripts.AI.Playmaker.Input
{
    [ActionCategory("RQ.Input")]
    [Tooltip("Gets the button just pressed state of an Action. This will only return TRUE only on the first frame the button is pressed or for the duration of the Button Down Buffer time limit if set in the Input Behavior assigned to this Action. This also applies to axes being used as buttons.")]

    public class PlayerGetButtonDownWithStamina : RewiredPlayerActionGetBoolFsmStateAction
    {
        [RequiredField]
        public FsmOwnerDefault gameObject;
        public FsmFloat Stamina;
        private EntityStatsComponent _entityStatsComponent;

        public override void OnEnter()
        {
            var go = Fsm.GetOwnerDefaultTarget(gameObject);
            if (go == null)
            {
                return;
            }
            var entity = go.GetComponent<IEntity>();
            if (_entityStatsComponent == null)
                _entityStatsComponent = entity.Components.GetComponent<EntityStatsComponent>();


        }

        protected override void DoUpdate()
        {
            // Don't check for button down unless the player has the stamina to perform the action
            var hasStamina = _entityStatsComponent.CheckStamina(Stamina.Value);
            if (!hasStamina)
                return;

            UpdateStoreValue(Player.GetButtonDown(actionName.Value));
        }
    }
}
