using BlueOrb.Base.Item;
using BlueOrb.Common.Container;
using BlueOrb.Controller.Inventory;
using BlueOrb.Messaging;

namespace BlueOrb.Scripts.AI.AtomActions
{
    public class AddItemToInventoryAtom : AtomActionBase
    {
        public ItemDesc Item;
        //private InventoryComponent _inventoryComponent;
        public bool ToMainCharacter;

        public override void Start(IEntity entity)
        {
            base.Start(entity);
            IEntity targetEntity;
            if (ToMainCharacter)
                targetEntity = EntityContainer.Instance.GetMainCharacter();
            else
                targetEntity = entity;
            //if (_inventoryComponent == null)
            //    _inventoryComponent = targetEntity.Components.GetComponent<InventoryComponent>();

            MessageDispatcher.Instance.DispatchMsg("AddItem", 0f, entity.GetId(), targetEntity.GetId(), Item);

            //_inventoryComponent.AddItem(Item);
            Finish();
        }

        //public override void OnUpdate()
        //{
        //    base.OnUpdate();
        //    if (_bufferState)
        //        return;

        //    var buttonDown = _player.GetButtonDown(_action);
        //    if (buttonDown)
        //        _bufferState = true;
        //}

        public override void End()
        {

        }
    }
}
