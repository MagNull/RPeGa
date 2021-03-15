namespace InventoryScripts
{
    public class TakeOffEquippableItemAction : IActionWithEquippableItem
    {
        private UIController _uiController;
        protected EquippableItem _equippableItem;
        private Inventory _inventory;
        
        public TakeOffEquippableItemAction(UIController uiController, EquippableItem item, Inventory inventory)
        {
            _uiController = uiController;
            _equippableItem = item;
            _inventory = inventory;
        }
        public virtual void Do()
        {
            _uiController.RemoveEquipmentFromSlot(_equippableItem);
            _inventory.AddItem(_equippableItem);
            _equippableItem.ItemTransform.parent = null;
        }
    }
}