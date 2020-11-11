﻿using BlueOrb.Common.Container;
using BlueOrb.Physics;

namespace BlueOrb.Scripts.AI.AtomActions
{
    public class JumpAtom : AtomActionBase
    {
        private PhysicsComponent _physicsComponent;

        public override void Start(IEntity entity)
        {
            base.Start(entity);
            if (_physicsComponent == null)
                _physicsComponent = entity.Components.GetComponent<PhysicsComponent>();
            _physicsComponent.Jump();
        }
    }
}
