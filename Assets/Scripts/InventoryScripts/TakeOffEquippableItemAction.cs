using UIScripts;

namespace InventoryScripts
{
    public class TakeOffEquippableItemAction : IActionWithEquippableItem
    {
        private EquipmentUIController _equipmentUIController;
        protected EquippableItem _equippableItem;
        private Inventory _inventory;
        
        public TakeOffEquippableItemAction(EquipmentUIController equipmentUIController, EquippableItem item, Inventory inventory)
        {
            _equipmentUIController = equipmentUIController;
            _equippableItem = item;
            _inventory = inventory;
        }
        public virtual void Do()
        {
            _equipmentUIController.RemoveEquipmentFromSlot(_equippableItem);
            _inventory.AddItem(_equippableItem);
            _equippableItem.ItemTransform.parent = null;
        }
    }
}