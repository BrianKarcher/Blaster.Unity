﻿using BlueOrb.Common.Container;
using BlueOrb.Controller.Player;
using BlueOrb.Physics;

namespace BlueOrb.Scripts.AI.AtomActions
{
    public class SetApplyGravityAtom : AtomActionBase
    {
        private IPhysicsComponent _physicsComponent;
        public bool ApplyGravity;

        public override void Start(IEntity entity)
        {
            base.Start(entity);
            if (_physicsComponent == null)
                _physicsComponent = entity.Components.GetComponent<IPhysicsComponent>();

            _physicsComponent.Controller.GetPhysicsData().ApplyGravity = ApplyGravity;
        }
    }
}
