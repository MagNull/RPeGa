using UnityEngine;
using Zenject;

namespace InventoryScripts
{
    [RequireComponent(typeof(Rigidbody))]
    public abstract class СonsumableItem : Item
    {
        [SerializeField] private int _numberOfUses = 1;
        
        private Inventory _inventory;

        [Inject]
        public void Construct(Inventory inventory)
        {
            _inventory = inventory;
        }
        
        public override void Use()
        {
            _numberOfUses--;
            if(_numberOfUses <= 0) DeleteItem();
        }

        private void DeleteItem()
        {
            _inventory.DeleteItem(this);
            Destroy(gameObject);
        }
    }
}
