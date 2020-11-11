using BlueOrb.Scripts.AI.AtomActions;
using BlueOrb.Scripts.AI.AtomActions.Camera;
using HutongGames.PlayMaker;
using BlueOrb.Common.Container;
using System;
using System.Linq;
using UnityEngine;

namespace BlueOrb.Scripts.AI.Playmaker.Camera
{
    [ActionCategory("RQ.Camera")]
    [HutongGames.PlayMaker.Tooltip("Choose Lock Target.")]
    public class ChooseLockTarget : FsmStateAction
    {
        public ChooseLockTargetAtom _atom;

        [UIHint(UIHint.Layer)]
        [HutongGames.PlayMaker.Tooltip("Layer mask of possible targets.")]
        public FsmInt[] Layers;

        [UIHint(UIHint.Tag)]
        [HutongGames.PlayMaker.Tooltip("Tags of possible targets.")]
        public FsmString[] Tags;

        [UIHint(UIHint.Variable)]
        [HutongGames.PlayMaker.Tooltip("Fire when Center is clicked.")]
        public FsmEvent Center;

        [UIHint(UIHint.Variable)]
        [HutongGames.PlayMaker.Tooltip("Fire when Lock On is clicked.")]
        public FsmEvent LockOn;

        private Action centerAction, lockOnAction;

        public override void OnEnter()
        {
            var entity = Owner.GetComponent<IEntity>();
            // Saves a small bit of memory only having to instantiate a delegate once.
            if (centerAction == null)
            {
                centerAction = () =>
                {
                    Fsm.Event(Center);
                };
            }
            if (lockOnAction == null)
            {
                lockOnAction = () =>
                {
                    Fsm.Event(LockOn);
                };
            }
            int layers = 0;
            for (int i = 0; i < Layers.Length;i++)
            {
                layers |= 1 << Layers[i].Value;
            }
            Debug.Log("Setting target lock layers " + layers);
            _atom.SetTargetLockLayers(layers);
            string[] tags = Tags.Select(i => i.Value).ToArray();
            _atom.SetTags(tags);
            _atom.SetCenterAction(centerAction);
            _atom.SetLockOnAction(lockOnAction);
            _atom.Start(entity);
        }

        public override void OnUpdate()
        {
            _atom.Update();
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
