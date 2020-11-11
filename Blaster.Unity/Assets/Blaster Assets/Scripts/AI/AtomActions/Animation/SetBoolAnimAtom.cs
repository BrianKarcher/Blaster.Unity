using BlueOrb.Common.Container;
using BlueOrb.Controller;
using UnityEngine;

namespace BlueOrb.Scripts.AI.AtomActions.Animation
{
    public class SetBoolAnimAtom : AtomActionBase
    {
        public bool RevertOnExit = false;
        public string Anim;
        public bool Value;
        private AnimationComponent _animationComponent;
        private bool PreviousValue;

        public override void Start(IEntity entity)
        {
            base.Start(entity);
            _animationComponent = entity.Components.GetComponent<AnimationComponent>();
            //Debug.Log($"Setting bool for anim {Anim}");
            PreviousValue = _animationComponent.GetBool(Anim);
            _animationComponent.SetBool(Anim, Value);
            Finish();
        }

        public override void End()
        {
            base.End();
            if (RevertOnExit)
            {
                _animationComponent.SetBool(Anim, PreviousValue);
            }
        }
    }
}
