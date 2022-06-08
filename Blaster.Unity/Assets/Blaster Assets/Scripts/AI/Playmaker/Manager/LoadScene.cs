using BlueOrb.Scripts.AI.AtomActions;
using HutongGames.PlayMaker;
using BlueOrb.Common.Container;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rewired;
using BlueOrb.Messaging;
using UnityEngine;

namespace BlueOrb.Scripts.AI.Playmaker.Manager
{
    [ActionCategory("BlueOrb.Manager")]
    [HutongGames.PlayMaker.Tooltip("Load a scene using the SceneController")]
    public class LoadScene : BasePlayMakerAction
    {
        [RequiredField]
        public FsmOwnerDefault gameObject;
        public FsmObject SceneConfig;

        public override void Reset()
        {
            gameObject = null;
            SceneConfig = null;
        }

        public override void OnEnter()
        {
            var go = Fsm.GetOwnerDefaultTarget(gameObject);
            if (go == null)
            {
                return;
            }
            var entity = base.GetEntityBase(go);


        }

        public override void OnExit()
        {
            base.OnExit();
        }
    }
}
