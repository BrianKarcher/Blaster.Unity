//using BlueOrb.Scripts.AI.AtomActions;
//using HutongGames.PlayMaker;
//using BlueOrb.Common.Container;
//using System.Collections.Generic;
//using BlueOrb.Controller.Block;
//using Assets.Blaster_Assets.Scripts.AI.Playmaker.Physics.Data;
//using BlueOrb.Messaging;

//namespace BlueOrb.Scripts.AI.Playmaker
//{
//    [ActionCategory("RQ.Physics")]
//    [Tooltip("Can see the entity?")]
//    public class Explode : BasePlayMakerAction
//    {
//        [RequiredField]
//        public FsmOwnerDefault gameObject;

//        [UIHint(UIHint.Layer)]
//        [Tooltip("Layers to check.")]
//        public FsmInt[] Layer;

//        public float Radius;
//        public float Force = 10f;
//        public float Damage = 1f;

//        public override void Reset()
//        {
//            gameObject = null;
//        }

//        public override void OnEnter()
//        {
//            var go = Fsm.GetOwnerDefaultTarget(gameObject);
//            if (go == null)
//            {
//                return;
//            }
//            var entity = go.GetComponent<IEntity>();
//            //_atom.Start(entity);
//            Kaboom(entity);
//            //if (!everyFrame)
//            Finish();
//        }

//        private void Kaboom(IEntity entity)
//        {
//            int layers = ActionHelpers.LayerArrayToLayerMask(Layer, false);
//            var entitiesHit = UnityEngine.Physics.OverlapSphere(entity.GetPosition(), Radius, layers);
//            var entitiesSentMessage = new HashSet<IEntity>();

//            var explodeData = new ExplodeData()
//            {
//                ExplodePosition = entity.GetPosition(),
//                Damage = Damage,
//                Force = Force
//            };

//            for (int i = 0; i < entitiesHit.Length; i++)
//            {
//                var entityHit = entitiesHit[i];
//                var entityItem = entityHit.GetComponent<EntityItemComponent>();
//                if (entityItem == null)
//                    continue;
//                var entityRepo = entityItem.GetComponentRepository();
//                // Duplicate send check
//                if (entitiesSentMessage.Contains(entityRepo))
//                {
//                    continue;
//                }

//                MessageDispatcher.Instance.DispatchMsg("Explode", 0f, entity.GetId(), entityRepo.GetId(), explodeData);

//                entitiesSentMessage.Add(entityRepo);
//            }
//        }
//    }
//}
