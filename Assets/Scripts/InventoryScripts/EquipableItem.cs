using System;
using AbilitySupports;
using UnityEngine;
using WeaponScripts;
using Zenject;

namespace InventoryScripts
{
    public class EquipableItem : Item
    {
        public Transform WeaponTransform;
        [Inject] private PlayerEquipment _playerEquipment;
        [SerializeField] private EquipableType _equipableType;
        [SerializeField] private Collider _takeCollider;
        private Animator _animator;
        private bool _isEquipped;

        protected override void Awake()
        {
            base.Awake();
            _animator = GetComponentInParent<Animator>();
            _animator.enabled = false;
        }

        public override void TakeItem(Slot slot)
        {
            base.TakeItem(slot);
            _takeCollider.enabled = false;
        }

        public override void Use()
        {
            if (_isEquipped)
            {
                _playerEquipment.TakeOffEquipment(_equipableType);
                _isEquipped = false;
            }
            else
            {
                _playerEquipment.PutOnEquipment(this, _equipableType);
                gameObject.SetActive(true);
                _animator.enabled = true;
                _isEquipped = true;
            }
        }
        
        
        public override void ThrowOutItem(Vector3 forward, float throwForce)
        {
            if (transform.parent.TryGetComponent(out Animator animator)) animator.enabled = false;
            transform.gameObject.layer = 0;
            transform.parent.SetParent(null);
            base.ThrowOutItem(forward, throwForce);
            _takeCollider.enabled = true;
            _isEquipped = false;

        }

    }
}