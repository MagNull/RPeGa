using System;
using AbilitySupports;
using UnityEngine;
using WeaponScripts;
using Zenject;

namespace InventoryScripts
{
    [RequireComponent(typeof(Rigidbody), typeof(ItemBonus))]
    public class EquipableItem : Item
    {
        public Transform ItemTransform;
        public event Action OnTakeOffItem;
        public event Action OnPutOnItem;
        public event Action<EquipableType> OnDropItem;
        [Inject] private PlayerEquipment _playerEquipment;
        [SerializeField] private EquipableType _equipableType;
        [SerializeField] private Collider _takeCollider;
        private bool _isEquipped;
        

        public override void TakeItem(Slot slot)
        {
            base.TakeItem(slot);
            _takeCollider.enabled = false;
        }

        public override void Use()
        {
            if (_isEquipped)
            {
                OnTakeOffItem?.Invoke();
                _playerEquipment.TakeOffEquipment(_equipableType);
                _isEquipped = false;
            }
            else
            {
                _playerEquipment.PutOnEquipment(this, _equipableType);
                gameObject.SetActive(true);
                OnPutOnItem?.Invoke();
                _isEquipped = true;
                _rigidbody.constraints = RigidbodyConstraints.FreezeAll;
            }
        }
        
        
        public override void ThrowOutItem(Vector3 forward, float throwForce)
        {
            transform.gameObject.layer = 0;
            ItemTransform.SetParent(null);
            base.ThrowOutItem(forward, throwForce);
            _takeCollider.enabled = true;
            _isEquipped = false;
            _rigidbody.constraints =  RigidbodyConstraints.None;
            OnDropItem?.Invoke(_equipableType);
        }

    }
}