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
                OnTakeOffItem?.Invoke();
                _playerEquipment.TakeOffEquipment(_equipableType);
                _animator.enabled = false;
                _isEquipped = false;
            }
            else
            {
                _playerEquipment.PutOnEquipment(this, _equipableType);
                gameObject.SetActive(true);
                OnPutOnItem?.Invoke(); 
                _animator.enabled = true;
                _isEquipped = true;
                if (_equipableType != EquipableType.MAINHANDWEAPON &&
                    _equipableType != EquipableType.OFFHANDWEAPON) _rigidbody.constraints = RigidbodyConstraints.FreezeAll;
            }
        }
        
        
        public override void ThrowOutItem(Vector3 forward, float throwForce)
        {
            Animator animator;
            if (transform.TryGetComponent(out animator) || 
                transform.parent.TryGetComponent(out animator)) animator.enabled = false;
            transform.gameObject.layer = 0;
            ItemTransform.SetParent(null);
            base.ThrowOutItem(forward, throwForce);
            _takeCollider.enabled = true;
            _isEquipped = false;
            if (_equipableType != EquipableType.MAINHANDWEAPON &&
                _equipableType != EquipableType.OFFHANDWEAPON)
            {
                _rigidbody.constraints =  RigidbodyConstraints.None;
            }
            OnDropItem?.Invoke(_equipableType);
        }

    }
}