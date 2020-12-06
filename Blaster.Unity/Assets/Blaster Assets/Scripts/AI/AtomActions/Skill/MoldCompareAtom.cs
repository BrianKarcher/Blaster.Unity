//using BlueOrb.Base.Manager;
//using BlueOrb.Base.Skill;
//using BlueOrb.Common.Container;
//using UnityEngine;

//namespace BlueOrb.Scripts.AI.AtomActions
//{
//    public class CurrentMoldCompareAtom : AtomActionBase
//    {
//        public MoldConfig MoldConfig;
//        //private bool _isEqual;
//        //private ThirdPersonUserControl _thirdPersonUserControl;

//        public override void Start(IEntity entity)
//        {
//            base.Start(entity);
//            //_isEqual = 
//            //if (_thirdPersonUserControl == null)
//            //    _thirdPersonUserControl = entity.Components.GetComponent<ThirdPersonUserControl>();
//        }

//        public bool IsEqual()
//        {
//            string mold1Name;
//            string mold1UniqueId;
//            string mold2Name;
//            string mold2UniqueId;

//            if (MoldConfig != null)
//            {
//                mold1Name = MoldConfig.name;
//                mold1UniqueId = MoldConfig.UniqueId;
//            }
//            else
//            {
//                mold1Name = string.Empty;
//                mold1UniqueId = string.Empty;
//            }

//            if (GameStateController.Instance.CurrentMold != null)
//            {
//                mold2Name = GameStateController.Instance.CurrentMold.name;
//                mold2UniqueId = GameStateController.Instance.CurrentMold.UniqueId;
//            }
//            else
//            {
//                mold2Name = string.Empty;
//                mold2UniqueId = string.Empty;
//            }

//            Debug.Log("Comparing " + mold1Name + "(" + mold1UniqueId + ") to " + mold2Name + "(" + mold2UniqueId + ")");
//            return GameStateController.Instance.CurrentMold == MoldConfig;
//        }
//    }
//}
