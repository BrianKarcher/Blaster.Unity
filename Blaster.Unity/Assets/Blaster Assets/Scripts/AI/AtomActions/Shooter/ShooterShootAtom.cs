//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using BlueOrb.Base.Item;
//using BlueOrb.Base.Manager;
//using BlueOrb.Common.Container;
//using BlueOrb.Controller.Component;
//using BlueOrb.Controller.Player;
//using BlueOrb.Messaging;
//using UnityEngine;

//namespace BlueOrb.Scripts.AI.AtomActions
//{
//    public class ShooterShootAtom : AtomActionBase
//    {
//        public List<ShardProjectileMap> ShardProjectile;
//        //private PlayerController _playerController;
//        private ShooterComponent _shooterComponent;

//        public class ShardProjectileMap
//        {
//            public ItemConfig Shard;
//            public GameObject Projectile;
//        }

//        public override void Start(IEntity entity)
//        {
//            base.Start(entity);
//            if (_shooterComponent == null)
//                _shooterComponent = entity.Components.GetComponent<ShooterComponent>();
//            var targetEntity = EntityContainer.Instance.GetMainCharacter();
//            var Item = new ItemDesc()
//            {
//                ItemConfig = GameStateController.Instance.CurrentShard,
//                Qty = -1
//            };
//            //if (_playerController == null)
//            //    _playerController = entity.Components.GetComponent<PlayerController>();
//            // Don't cache the shooter since it can change
//            //var shooter = _playerController.Shooter;
//            //var currentShard = 

//            // The Mold doesn't have an item config and cannot have inventory count removed.
//            if (Item.ItemConfig != null)
//                MessageDispatcher.Instance.DispatchMsg("AddItem", 0f, entity.GetId(), targetEntity.GetId(), Item);
//            _shooterComponent.Shoot();
//            Finish();
//        }
//    }
//}
