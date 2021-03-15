using InventoryScripts;
using UnityEngine;

namespace UIScripts
{
    public class EquipmentUIController : MonoBehaviour
    {
        [SerializeField] private EquipmentSlot[] _equipmentSlots;
    
        public EquipmentSlot SetAndGetEquipmentInSlot(EquippableItem item)
        {
            int i = 0;
            while (_equipmentSlots[i].Index != item.ItemPlaceIndex) i++;
            _equipmentSlots[i].SetItem(item);
            return _equipmentSlots[i];
        }

        public void RemoveEquipmentFromSlot(EquippableItem item)
        {
            int i = 0;
            while (_equipmentSlots[i].Index != item.ItemPlaceIndex) i++;
            _equipmentSlots[i].DeleteItem();
        }
    }
}
