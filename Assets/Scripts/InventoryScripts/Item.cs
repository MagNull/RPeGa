using System;
using UnityEngine;

namespace InventoryScripts
{
    public abstract class Item : MonoBehaviour
    {
        public string Name;
        public Sprite Image;
        public int SlotIndex = -1;
        public abstract void Use();

        private void OnCollisionEnter(Collision other)
        {
            Debug.Log(other.gameObject.name + " touch.");
            if (other.gameObject.TryGetComponent(out Inventory inventory))
            {
                inventory.ChangeTakeTargetItem(this);
            }
        }

        private void OnCollisionExit(Collision other)
        {
            if (other.gameObject.TryGetComponent(out Inventory inventory))
            {
                inventory.ChangeTakeTargetItem(null);
            }
        }
    }
}
