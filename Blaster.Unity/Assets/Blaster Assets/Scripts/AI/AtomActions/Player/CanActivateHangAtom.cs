//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using BlueOrb.Common.Container;
//using BlueOrb.Controller.Player;

//namespace BlueOrb.Scripts.AI.AtomActions
//{
//    public class CanActivateHangAtom : AtomActionBase
//    {
//        //private ThirdPersonUserControl _thirdPersonUserControl;
//        private PlayerController _playerController;
//        public bool CanHang;

//        public override void Start(IEntity entity)
//        {
//            base.Start(entity);
//            if (_playerController == null)
//                _playerController = entity.Components.GetComponent<PlayerController>();
//            CanHang = false;
//        }

//        public override void OnUpdate()
//        {
//            CanHang = _playerController.CanHang;
//            base.OnUpdate();
//        }
//    }
//}
