using System;
using UnityEngine;

namespace InventoryScripts
{
    public abstract class Item : MonoBehaviour
    {
        public string Name;
        public Sprite Image;
        public int SlotIndex = -1;
        protected Rigidbody _rigidbody;

        protected virtual void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        public abstract void Use();

        public virtual void TakeItem(Slot slot)
        {
            SlotIndex = slot.Index;
            gameObject.SetActive(false);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.TryGetComponent(out Inventory inventory)) inventory.ChangeTakeTargetItem(this);
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.gameObject.TryGetComponent(out Inventory inventory)) inventory.ChangeTakeTargetItem(this);
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.TryGetComponent(out Inventory inventory)) inventory.ChangeTakeTargetItem(null);
        }

        public virtual void ThrowOutItem(Vector3 forward, float throwForce)
        {
            _rigidbody.AddForce(forward * throwForce, ForceMode.Impulse);
        }
    }
}
