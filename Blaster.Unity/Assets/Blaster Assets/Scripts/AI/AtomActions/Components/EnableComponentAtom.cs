using BlueOrb.Common.Components;
using BlueOrb.Common.Container;

namespace BlueOrb.Scripts.AI.AtomActions.Components
{
    public class EnableComponentAtom : AtomActionBase
    {
        public bool EnableOnEnter;
        public bool EnableOnExit;
        public string ComponentName;
        private IComponentBase _component;

        public override void Start(IEntity entity)
        {
            base.Start(entity);
            if (_component == null)
                _component = entity.Components.GetComponent(ComponentName);
            _component?.gameObject.SetActive(EnableOnEnter);
        }

        public override void End()
        {
            _component?.gameObject.SetActive(EnableOnExit);
        }
    }
}
