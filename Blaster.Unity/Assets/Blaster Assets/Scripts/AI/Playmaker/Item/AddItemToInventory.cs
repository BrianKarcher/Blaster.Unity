using BlueOrb.Scripts.AI.AtomActions;
using HutongGames.PlayMaker;
using BlueOrb.Common.Container;

namespace BlueOrb.Scripts.AI.PlayMaker.Item
{
    [ActionCategory("BlueOrb.Item")]
    //[Tooltip("Returns Success if Button is pressed.")]
    public class AddItemToInventory : FsmSimpleAction<AddItemToInventoryAtom>
    {
        //public AddItemToInventoryAtom _atom;

        //public override void OnEnter()
        //{
        //    var entity = Owner.GetComponent<IEntity>();
        //    _atom.Start(entity);
        //    if (_atom.IsFinished)
        //        Finish();
        //}

        //public override void OnExit()
        //{
        //    base.OnExit();
        //    _atom.End();
        //}

        //public override void OnUpdate()
        //{
        //    _attackAtom.OnUpdate();
        //    if (_attackAtom.IsFinished)
        //        Finish();
        //}
    }
}
