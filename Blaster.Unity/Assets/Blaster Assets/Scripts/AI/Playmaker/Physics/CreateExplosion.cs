using BlueOrb.Scripts.AI.AtomActions;
using HutongGames.PlayMaker;
using BlueOrb.Common.Container;
using System.Linq;

namespace BlueOrb.Scripts.AI.Playmaker
{
    [ActionCategory("RQ.Physics")]
    [Tooltip("Create an explosion.")]
    public class CreateExplosion : FsmStateAction
    {
        [RequiredField]
        public FsmOwnerDefault gameObject;

        public FsmFloat Force = 7;
        public FsmFloat Radius = 5;
        public FsmFloat Damage = 1;
        public FsmFloat Delay;

        //protected EntityCommonComponent _entityCommon;
        [UIHint(UIHint.Layer)]
        [Tooltip("Layers to check.")]
        public FsmInt[] Layer;
        [UIHint(UIHint.Tag)]
        [Tooltip("The Tag to search for. If Child Name is set, both name and Tag need to match.")]
        public FsmString[] Tags;

        public CreateExplosionAtom _atom;

        public override void Reset()
        {
            gameObject = null;
        }

        public override void OnEnter()
        {
            var go = Fsm.GetOwnerDefaultTarget(gameObject);
            if (go == null)
            {
                return;
            }
            var entity = go.GetComponent<IEntity>();
            var layerMask = ActionHelpers.LayerArrayToLayerMask(Layer, false);
            _atom.LayerMask = layerMask;
            _atom.Damage = Damage.Value;
            _atom.Delay = Delay.Value;
            _atom.Force = Force.Value;
            _atom.Radius = Radius.Value;
            _atom.Tags = Tags.Select(i => i.Value).ToArray();

            _atom.Start(entity);
        }

        public override void OnUpdate()
        {
            base.OnUpdate();
            _atom.OnUpdate();
        }

        public override void OnExit()
        {
            base.OnExit();
            _atom.End();
        }
    }
}
