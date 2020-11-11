using BlueOrb.Scripts.AI.AtomActions;
using HutongGames.PlayMaker;
using BlueOrb.Common.Container;

namespace BlueOrb.Scripts.AI.Playmaker.Camera
{
    [ActionCategory("RQ.Camera")]
    [Tooltip("Center the camera behind the player.")]
    public class NormalCamera : FsmStateAction
    {
        public NormalCameraAtom _atom;

        public override void OnEnter()
        {
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
                Finish();
        }

        public override void OnExit()
        {
            base.OnExit();
            _atom.End();
        }
    }
}
