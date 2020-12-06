//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using BlueOrb.Common.Container;
//using BlueOrb.Controller.Player;

//namespace BlueOrb.Scripts.AI.AtomActions
//{
//    public class EnablePlayerInputAtom : AtomActionBase
//    {
//        public bool Enable;
//        public bool RevertOnExit = false;

//        private ThirdPersonUserControl _thirdPersonUserControl;
//        private PlayerController _playerController;
//        private bool _previousValue;

//        public override void Start(IEntity entity)
//        {
//            base.Start(entity);
//            if (_thirdPersonUserControl == null)
//                _thirdPersonUserControl = entity.Components.GetComponent<ThirdPersonUserControl>();
//            if (_playerController == null)
//                _playerController = entity.Components.GetComponent<PlayerController>();
//            _previousValue = _thirdPersonUserControl.GetEnablePlayerInput();
//            _thirdPersonUserControl.SetEnablePlayerInput(Enable);
//            _playerController.SetMovementActive(Enable);
//        }

//        public override void End()
//        {
//            base.End();
//            if (RevertOnExit)
//            {
//                _thirdPersonUserControl.SetEnablePlayerInput(_previousValue);
//                _playerController.SetMovementActive(_previousValue);
//            }
//        }
//    }
//}
