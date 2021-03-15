using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.UI;

namespace InventoryScripts
{
    public class Inventory : MonoBehaviour
    {
        public readonly List<EquipmentSlot> EquipmentSlots = new List<EquipmentSlot>();
        [SerializeField] private Item _targetItem;
        [SerializeField] private Text _takeItemText;
        private readonly PriorityQueue<Slot> _inventorySlots = new PriorityQueue<Slot>();
        private readonly List<Slot> _closedSlots = new List<Slot>();

        private void Start()
        {
            this.UpdateAsObservable()
                .Where(_ => Input.GetKeyDown(KeyCode.E))
                .Subscribe(_ =>
                {
                    if (!(_targetItem is null) && _inventorySlots.Length >= 0)
                    {
                        AddItem(_targetItem);
                        ChangeTakeTargetItem(null);
                    }
                });
        }

        public void ChangeTakeTargetItem(Item item)
        {
            if (item is null)
            {
                _takeItemText.gameObject.SetActive(false);
            }
            else
            {
                _takeItemText.gameObject.SetActive(true);
            }
            _targetItem = item;
        }

        public void AddItem(Item item)
        {
            if (_inventorySlots.Length > 0)
            {
                Slot slot = _inventorySlots.Peek();
                _inventorySlots.Dequeue(slot);
                slot.SetItem(item);
                _closedSlots.Add(slot);
            }
        }

        public void AddSlot(Slot slot)
        {
            if (slot.Index >= 0) _inventorySlots.Enqueue(slot);
            else EquipmentSlots.Add((EquipmentSlot)slot);
        }

        public void DeleteItem(Item item)
        {
            if (item.SlotIndex < 0) return;
            int i = 0;
            while (_closedSlots[i].Index != item.SlotIndex) i++;
            _closedSlots[i].DeleteItem();
            _inventorySlots.Enqueue(_closedSlots[i]);
            _closedSlots.RemoveAt(i);
        }
    }
}
