using BlueOrb.Scripts.AI.AtomActions;
using HutongGames.PlayMaker;
using BlueOrb.Common.Container;

namespace BlueOrb.Scripts.AI.Playmaker
{
    [ActionCategory("RQ.Physics")]
    [Tooltip("Apply explosion force.")]
    public class AddExplosionForce : FsmStateAction
    {
        [RequiredField]
        public FsmOwnerDefault gameObject;

        public FsmFloat Damage = 1f;
        //public float Damage => _damage;
        //private float _force;
        public FsmFloat Force = 10f;
        //private float _radius;
        public FsmFloat Radius = 5f;
        public FsmFloat UpwardsModifier = 1f;

        public AddExplosionForceAtom _atom;

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
            _atom.SetProperties(Damage.Value, Force.Value, Radius.Value, UpwardsModifier.Value);
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
