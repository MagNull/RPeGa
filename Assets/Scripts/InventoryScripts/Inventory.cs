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
        [SerializeField] private Text _takeItemText;
        private readonly PriorityQueue<Slot> _slots = new PriorityQueue<Slot>();
        private readonly List<Slot> _closedSlots = new List<Slot>();
        [SerializeField] private Item _targetItem;

        private void Start()
        {
            this.UpdateAsObservable()
                .Where(_ => Input.GetKeyDown(KeyCode.E))
                .Subscribe(_ =>
                {
                    if (!(_targetItem is null))
                    {
                        AddItem(_targetItem);
                        ChangeTakeTargetItem(null);
                    }
                });
        }

        public void ChangeTakeTargetItem(Item item)
        {
            if (_slots.Length > 0)
            {
                _takeItemText.gameObject.SetActive(!_takeItemText.gameObject.activeSelf);
                _targetItem = item; 
            }
        }

        public void AddItem(Item item)
        {
            if (_slots.Length > 0)
            {
                Slot slot = _slots.Peek();
                _slots.Dequeue(slot);
                slot.SetItem(item);
                _closedSlots.Add(slot);
            }
        }

        public void AddSlot(Slot slot)
        {
            if(slot.Index >= 0) _slots.Enqueue(slot);   
        }

        public void DeleteItem(Item item)
        {
            if (item.SlotIndex < 0) return;
            int i = 0;
            while (_closedSlots[i].Index != item.SlotIndex) i++;
            _closedSlots[i].DeleteItem();
            _slots.Enqueue(_closedSlots[i]);
            _closedSlots.RemoveAt(i);
        }
    }
}
