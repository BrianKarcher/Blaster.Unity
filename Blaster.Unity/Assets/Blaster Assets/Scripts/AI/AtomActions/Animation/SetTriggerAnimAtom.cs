using BlueOrb.Common.Container;
using BlueOrb.Controller;

namespace BlueOrb.Scripts.AI.AtomActions.Animation
{
    public class SetTriggerAnimAtom : AtomActionBase
    {
        public string Anim;
        private AnimationComponent _animationComponent;

        public override void Start(IEntity entity)
        {
            base.Start(entity);
            _animationComponent = entity.Components.GetComponent<AnimationComponent>();
            _animationComponent.SetTrigger(Anim);
            Finish();
        }
    }
}
