//using Assets.Source.UI;
using BlueOrb.Base.Input;
using BlueOrb.Base.Item;
using BlueOrb.Base.Manager;
using BlueOrb.Base.Skill;
using BlueOrb.Common.Components;
using BlueOrb.Messaging;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BlueOrb.Scripts.UI
{
    [AddComponentMenu("RQ/UI/UI Controller")]
    public class UIController : ComponentBase<UIController>
    {
        [SerializeField]
        private Image OverlayImage;
    }
}
