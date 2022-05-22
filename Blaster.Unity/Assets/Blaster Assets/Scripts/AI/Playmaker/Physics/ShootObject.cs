using BlueOrb.Scripts.AI.AtomActions;
using HutongGames.PlayMaker;
using BlueOrb.Common.Container;

namespace BlueOrb.Scripts.AI.Playmaker
{
    [ActionCategory("RQ.Physics")]
    [Tooltip("Shoot an object.")]
    public class ShootObject : BasePlayMakerAction
    {
        [RequiredField]
        public FsmOwnerDefault gameObject;

        [UIHint(UIHint.Layer)]
        [Tooltip("Pick only from these layers.")]
        public FsmInt[] layerMask;

        public FsmGameObject ObjectToShoot;

        public FsmGameObject SpawnPoint;

        public FsmFloat ShootDelay;

        public ShootObjectAtom _atom;

        public override void Reset()
        {
            gameObject = null;
        }

        public override void OnEnter()
        {
            int lMask = ActionHelpers.LayerArrayToLayerMask(layerMask, false);
            var go = Fsm.GetOwnerDefaultTarget(gameObject);
            if (go == null)
            {
                return;
            }

            var entity = base.GetEntityBase(go);
            _atom.SetLayerMask(lMask);
            _atom.SetObjectToShoot(ObjectToShoot.Value);
            if (!SpawnPoint.IsNone)
                _atom.SetSpawnPoint(SpawnPoint.Value);
            _atom.Start(entity);
            //Owner.gameObject.Corou
            //entity.StartCoroutine(_atom.ShootObject());
            //Finish();
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
