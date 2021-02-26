using UnityEngine;

namespace InventoryScripts
{
    public abstract class Item : MonoBehaviour
    {
        public string Name;
        public Sprite Image;
        public int SlotIndex = -1;
        public abstract void Use();

        private void OnTriggerEnter(Collider other)
        {
            Debug.Log(other.name + " touch.");
            if (other.gameObject.TryGetComponent(out Inventory inventory))
            {
                inventory.AddItem(this);
                gameObject.SetActive(false);
            }
        }
    }
}
