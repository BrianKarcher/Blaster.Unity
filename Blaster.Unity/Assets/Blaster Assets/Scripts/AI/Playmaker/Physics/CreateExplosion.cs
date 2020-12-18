using BlueOrb.Scripts.AI.AtomActions;
using HutongGames.PlayMaker;
using PM = HutongGames.PlayMaker;
using BlueOrb.Common.Container;
using System.Linq;
using UnityEngine;

namespace BlueOrb.Scripts.AI.Playmaker
{
    [ActionCategory("RQ.Physics")]
    [PM.Tooltip("Create an explosion.")]
    public class CreateExplosion : FsmStateAction
    {
        [RequiredField]
        public FsmOwnerDefault gameObject;

        public FsmFloat Force = 7;
        public FsmFloat Radius = 5;
        public FsmFloat Damage = 1;
        public FsmFloat Delay;
        public bool CastSphere = true;
        public FsmArray Recipients;

        //protected EntityCommonComponent _entityCommon;
        [UIHint(UIHint.Layer)]
        [PM.Tooltip("Layers to check.")]
        public FsmInt[] Layer;
        [UIHint(UIHint.Tag)]
        [PM.Tooltip("The Tag to search for. If Child Name is set, both name and Tag need to match.")]
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
            _atom.CastSphere = CastSphere;
            if (!Recipients.IsNone && Recipients.objectReferences != null)
            {
                _atom.Recipients = Recipients.objectReferences.Select(i => (GameObject)i).ToList();
            }
            else
            {
                _atom.Recipients = null;
            }

            _atom.Start(entity);
        }

        public override void OnUpdate()
        {
            base.OnUpdate();
            _atom.OnUpdate();
            if (_atom.IsFinished)
                Finish();
        }

        public override void OnExit()
        {
            base.OnExit();
            _atom.End();
        }
    }
}
