using BlueOrb.Scripts.AI.AtomActions;
using HutongGames.PlayMaker;
using BlueOrb.Common.Container;
using UnityEngine;

namespace BlueOrb.Scripts.AI.Playmaker.Camera
{
    [ActionCategory("RQ.Camera")]
    [HutongGames.PlayMaker.Tooltip("Center the camera behind the player.")]
    public class CenterCamera : FsmStateAction
    {
        [UIHint(UIHint.Variable)]
        [HutongGames.PlayMaker.Tooltip("Fire when Lock On is clicked.")]
        public FsmEvent FinishEvent;

        public CenterCameraAtom _atom;

        public override void OnEnter()
        {
            Debug.Log("(CenterCamera) OnEnter called");
            var entity = Owner.GetComponent<IEntity>();
            _atom.Start(entity);
        }

        public override void OnPreprocess()
        {
            Fsm.HandleLateUpdate = true;
        }

        public override void OnLateUpdate()
        {
            _atom.OnLateUpdate();
            if (_atom.IsFinished)
            {
                Fsm.Event(FinishEvent);
                Finish();
            }
        }

        public override void OnExit()
        {
            base.OnExit();
            Debug.Log("(CenterCamera) OnExit called");
            _atom.End();
        }
    }
}
