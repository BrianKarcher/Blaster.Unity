using BlueOrb.Scripts.AI.AtomActions;
using BlueOrb.Scripts.AI.Playmaker;
using HutongGames.PlayMaker;
using BlueOrb.Common.Container;

namespace BlueOrb.Scripts.AI.PlayMaker.Item
{
    //[ActionCategory("RQ.Item")]
    //[Tooltip("Returns Success if Button is pressed.")]
    public class FsmSimpleAction<T> : BasePlayMakerAction where T : IAtomActionBase
    {
        public T _atom;

        public override void OnEnter()
        {
            //var entity = Owner.GetComponent<IEntity>();
            var entity = base.GetRepo(Owner);
            _atom.Start(entity);
            if (_atom.IsFinished)
                Finish();
        }

        public override void OnExit()
        {
            base.OnExit();
            _atom.End();
        }

        public override void OnUpdate()
        {
            _atom.OnUpdate();
            if (_atom.IsFinished)
                Finish();
        }
    }
}
