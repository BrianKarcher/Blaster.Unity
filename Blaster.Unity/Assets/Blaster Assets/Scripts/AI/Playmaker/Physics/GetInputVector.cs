using BlueOrb.Scripts.AI.AtomActions.Physics;
using HutongGames.PlayMaker;
using BlueOrb.Common.Container;

namespace BlueOrb.Scripts.AI.Playmaker.Physics
{
    [ActionCategory("RQ.Physics")]
    [Tooltip("Look at the player input vector direction.")]
    public class GetInputVector : FsmStateAction
    {
        [UIHint(UIHint.Variable)]
        public FsmVector3 InputVector;
        [UIHint(UIHint.Variable)]
        public FsmFloat InputVectorMagnitude;
        public bool everyFrame = false;

        public GetInputVectorAtom _atom;

        public override void OnEnter()
        {
            var entity = Owner.GetComponent<IEntity>();
            _atom.Start(entity);
            Tick();
            if (!everyFrame)
                Finish();
        }

        public override void OnExit()
        {
            base.OnExit();
            _atom.End();
        }

        public override void OnUpdate()
        {
            base.OnUpdate();
            Tick();
        }

        public void Tick()
        {
            var inputVector = _atom.GetInputVector();
            InputVector.Value = inputVector;
            InputVectorMagnitude.Value = inputVector.magnitude;
        }
    }
}
