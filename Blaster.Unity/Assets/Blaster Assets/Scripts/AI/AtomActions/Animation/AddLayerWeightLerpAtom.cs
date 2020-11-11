using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlueOrb.Common.Container;
using BlueOrb.Controller;
using BlueOrb.Controller.Attack;
using UnityEngine;

namespace BlueOrb.Scripts.AI.AtomActions.Animation
{
    public class AddLayerWeightLerpAtom : AtomActionBase
    {
        private AnimationComponent _animationComponent;
        public string LayerName;
        public float Final;
        public float Time;
        public bool RevertOnExit = false;
        public float RevertTime;
        private float InitialValue;

        public override void Start(IEntity entity)
        {
            base.Start(entity);
            _animationComponent = entity.Components.GetComponent<AnimationComponent>();
            InitialValue = _animationComponent.GetCurrentLayerWeight(LayerName);
            _animationComponent.AddLayerWeightLerp(LayerName, Final, Time);

            Finish();
        }

        public override void End()
        {
            base.End();
            if (RevertOnExit)
            {
                _animationComponent.AddLayerWeightLerp(LayerName, InitialValue, RevertTime);
            }
        }
    }
}
