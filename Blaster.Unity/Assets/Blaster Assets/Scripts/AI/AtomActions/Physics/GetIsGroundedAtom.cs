﻿using BlueOrb.Common.Container;
using BlueOrb.Controller.Player;
using BlueOrb.Physics;

namespace BlueOrb.Scripts.AI.AtomActions
{
    public class GetIsGroundedAtom : AtomActionBase
    {
        private IPhysicsComponent _physicsComponent;

        public override void Start(IEntity entity)
        {
            base.Start(entity);
            if (_physicsComponent == null)
                _physicsComponent = entity.Components.GetComponent<IPhysicsComponent>();
        }

        public bool GetIsGrounded()
        {
            return _physicsComponent.Controller.GetIsGrounded();
        }
    }
}
