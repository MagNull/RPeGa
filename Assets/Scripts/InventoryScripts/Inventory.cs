using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InventoryScripts
{
    public class Inventory : MonoBehaviour
    {
        private readonly PriorityQueue<Slot> _slots = new PriorityQueue<Slot>();
        private readonly List<Slot> _closedSlots = new List<Slot>();

        public void AddItem(Item item)
        {
            Slot slot = _slots.Peek();
            _slots.Dequeue(slot);
            slot.SetItem(item);
            _closedSlots.Add(slot);
            item.SlotIndex = slot.Index;
        }

        public void AddSlot(Slot slot)
        {
            _slots.Enqueue(slot);   
        }

        public void DeleteItem(Item item)
        {
            int i = 0;
            while (_closedSlots[i].Index != item.SlotIndex) i++;
            _closedSlots[i].DeleteItem();
            _slots.Enqueue(_closedSlots[i]);
            _closedSlots.RemoveAt(i);
            Destroy(item.gameObject);
        }
    }
}
