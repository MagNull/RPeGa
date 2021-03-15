using UnityEngine;
using WeaponScripts;
using Zenject.ReflectionBaking.Mono.Cecil;

namespace InventoryScripts
{
    public class PutOnEquippableItemAction : IActionWithEquippableItem
    {
        private UIController _uiController;
        protected EquippableItem _equippableItem;
        private Inventory _inventory;
        protected PlayerEquipment _playerEquipment;

        public PutOnEquippableItemAction(UIController uiController, EquippableItem item, Inventory inventory,
            PlayerEquipment playerEquipment)
        {
            _uiController = uiController;
            _equippableItem = item;
            _inventory = inventory;
            _playerEquipment = playerEquipment;
        }
        
        public virtual void Do()
        {
            _inventory.DeleteItem(_equippableItem);
            EquipmentSlot equipmentSlot = _uiController.SetAndGetEquipmentInSlot(_equippableItem);
            _equippableItem.ItemTransform.position = equipmentSlot.EquipmentPlace.position;
            _equippableItem.ItemTransform.rotation = equipmentSlot.EquipmentPlace.rotation;
            _equippableItem.ItemTransform.parent = equipmentSlot.EquipmentPlace;
        }
    }
}