using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace InventoryScripts
{
    public class Equipper : SerializedMonoBehaviour
    {
        public Dictionary<int,Transform> EquipmentPlaceDictionary = new Dictionary<int, Transform>();
        [Inject] private Inventory _inventory;

        private void Start()
        {
            foreach (var slot in _inventory.EquipmentSlots)
            {
                slot.EquipmentPlace = EquipmentPlaceDictionary[slot.Index];
            }
        }
    }
}