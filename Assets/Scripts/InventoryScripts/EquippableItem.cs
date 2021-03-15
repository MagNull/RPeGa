using System;
using AbilitySupports;
using UnityEngine;
using WeaponScripts;
using Zenject;

namespace InventoryScripts
{
    [RequireComponent(typeof(Rigidbody), typeof(ItemBonus))]
    public class EquippableItem : Item
    {
        public Transform ItemTransform;
        public event Action OnTakeOffItem;
        public event Action OnPutOnItem;
        [SerializeField] private Collider _takeCollider;
        [SerializeField] private int _itemPlaceIndex;
        private PlayerEquipment _playerEquipment;
        private UIController _uiController;
        private Inventory _inventory;
        private bool _isEquipped;

        public int ItemPlaceIndex => _itemPlaceIndex;

        [Inject]
        public void Construct(PlayerEquipment playerEquipment, UIController uiController, Inventory inventory)
        {
            _playerEquipment = playerEquipment;
            _uiController = uiController;
            _inventory = inventory;
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
                
                IActionWithEquippableItem action;
                if (TryGetComponent(out Weapon weapon))
                    action = new TakeOffWeaponAction(_uiController, this,
                        _inventory, weapon, _playerEquipment);
                else action = new TakeOffEquippableItemAction(_uiController, this, _inventory);
                _playerEquipment.DoActionWithItem(action);
                
                _isEquipped = false;
            }
            else
            {
                IActionWithEquippableItem action;
                if (TryGetComponent(out Weapon weapon))
                    action = new PutOnWeaponAction(_uiController, this,
                        _inventory, _playerEquipment, weapon);
                else action = new PutOnEquippableItemAction(_uiController, this, _inventory, _playerEquipment);
                _playerEquipment.DoActionWithItem(action);
                
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
            _rigidbody.constraints = RigidbodyConstraints.None;
            OnTakeOffItem?.Invoke();
            
            if (TryGetComponent(out Weapon weapon)) _playerEquipment.InitWeapons(weapon.WeaponType, null);
        }
    }
}