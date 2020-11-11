using BlueOrb.Common.Container;
using BlueOrb.Controller.Player;
using UnityEngine;

namespace BlueOrb.Scripts.AI.AtomActions.Physics
{
    public class GetInputVectorAtom : AtomActionBase
    {
        private ThirdPersonUserControl _thirdPersonUserControl;

        public override void Start(IEntity entity)
        {
            base.Start(entity);
            if (_thirdPersonUserControl == null)
                _thirdPersonUserControl = entity.Components.GetComponent<ThirdPersonUserControl>();
        }

        public Vector3 GetInputVector()
        {
            return _thirdPersonUserControl.GetInputAxisVector();
        }
    }
}
