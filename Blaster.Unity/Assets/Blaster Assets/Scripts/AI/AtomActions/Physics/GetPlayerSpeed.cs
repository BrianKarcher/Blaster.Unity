//using BlueOrb.Common.Container;
//using BlueOrb.Controller.Player;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace BlueOrb.Scripts.AI.AtomActions.Physics
//{
//    public class GetPlayerSpeed : AtomActionBase
//    {
//        private PlayerController _playerController;
//        private float _speed;

//        public override void Start(IEntity entity)
//        {
//            base.Start(entity);
//            if (_playerController == null)
//                _playerController = entity.Components.GetComponent<PlayerController>();
            
//        }

//        public override void OnUpdate()
//        {
//            base.OnUpdate();
//        }

//        public float GetSpeed()
//        {
//            return _speed;
//        }
//    }
//}
