using BlueOrb.Common.Container;
using BlueOrb.Controller.Player;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueOrb.Scripts.AI.AtomActions.Physics
{
    public class LookAtInputVectorAtom : AtomActionBase
    {
        private ThirdPersonUserControl _thirdPersonUserControl;

        public override void Start(IEntity entity)
        {
            base.Start(entity);
            if (_thirdPersonUserControl == null)
                _thirdPersonUserControl = entity.Components.GetComponent<ThirdPersonUserControl>();
            Tick();
        }

        public override void OnUpdate()
        {
            base.OnUpdate();
        }

        public void Tick()
        {
            var inputVector = _thirdPersonUserControl.GetInputAxisVector();
            var lookAtVector = _entity.transform.position + inputVector;
            _entity.transform.LookAt(lookAtVector);
        }
    }
}
