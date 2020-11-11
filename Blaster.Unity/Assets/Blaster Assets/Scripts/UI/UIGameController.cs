//using BlueOrb.Base.Input;
//using System;
//using UnityEngine;
//using UnityEngine.UI;

//namespace BlueOrb.Scripts.UI
//{
//    [Serializable]
//    public class UIGameController
//    {
//        [SerializeField]
//        private GameObject Controller;
//        [SerializeField]
//        private Image R1ActiveImage;
//        [SerializeField]
//        private Text R1Text;

//        public void ActivateController(GameControllerButtons button, string text)
//        {
//            // TODO Flesh this out for all buttons.
//            if (!String.IsNullOrEmpty(text))
//                R1Text.text = text;
//            Controller.SetActive(true);
//        }

//        public void DeactivateController()
//        {
//            Controller.SetActive(false);
//        }
//    }
//}
