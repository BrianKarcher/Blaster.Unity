using BlueOrb.Scripts.AI.AtomActions;
using HutongGames.PlayMaker;
using BlueOrb.Common.Container;

namespace BlueOrb.Scripts.AI.Playmaker.Camera
{
    [ActionCategory("RQ.Camera")]
    [Tooltip("Center the camera behind the player.")]
    public class OverShoulderCamera : FsmStateAction
    {
        //[UIHint(UIHint.Layer)]
        //[Tooltip("Pick only from these layers.")]
        //public FsmInt[] raycastLayerMask;

        public OverShoulderCameraAtom _atom;

        public override void OnEnter()
        {
            //int lMask = ActionHelpers.LayerArrayToLayerMask(raycastLayerMask, false);

            var entity = Owner.GetComponent<IEntity>();
            //_atom.SetLayerMask(lMask);
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
                Finish();
        }

        public override void OnExit()
        {
            base.OnExit();
            _atom.End();
        }
    }
}
