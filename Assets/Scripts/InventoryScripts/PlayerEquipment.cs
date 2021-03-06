using System;
using AbilitySupports;
using UniRx;
using UnityEngine;
using WeaponScripts;
using Zenject;

namespace InventoryScripts
{
    public class PlayerEquipment : MonoBehaviour
    {
        [SerializeField] private Transform _mainHandTransform;
        private AbilityCaster _abilityCaster;
        private Transform _offHandTransform;
        private BaseActiveAbility[] _activeAbilities;
        private BaseActiveAbility[] _activeAttacks;
        private EquipableItem _mainHandWeapon;
        private EquipableItem _offHandWeapon;
        [SerializeField] private UIController _uiController;
        private Inventory _inventory;

        [Inject]
        public void Construct(UIController uiController, Inventory inventory)
        {
            _uiController = uiController;
            _inventory = inventory;
        }
        public void Init(AbilityCaster abilityCaster)
        {
            _abilityCaster = abilityCaster;
            _mainHandTransform = _abilityCaster.MainHandTransform;
            _offHandTransform = _abilityCaster.OffHandTransform;
            _abilityCaster
                .ObserveEveryValueChanged(x => _abilityCaster)
                .Subscribe(_ =>
                {
                    _activeAbilities = _abilityCaster.GetActiveAbilities();
                    _activeAttacks = _abilityCaster.GetActiveAttacks();
                });
        }

        public void EquipWeapon(EquipableItem weapon, EquipableType weaponType)
        {
            switch (weaponType)
            {
                case EquipableType.MAINHANDWEAPON:
                    _mainHandWeapon = weapon;
                    _mainHandWeapon.gameObject.layer = 7;
                    _mainHandWeapon.WeaponTransform.transform.position = _mainHandTransform.position;
                    _mainHandWeapon.WeaponTransform.rotation = _mainHandTransform.rotation;
                    _mainHandWeapon.WeaponTransform.parent = _mainHandTransform;
                    InitWeapons(EquipableType.MAINHANDWEAPON, _mainHandWeapon.GetComponent<Weapon>());
                    _inventory.DeleteItem(_mainHandWeapon);
                    _uiController.SetEquipmentInSlot(EquipableType.MAINHANDWEAPON, _mainHandWeapon);
                    break;
                case EquipableType.OFFHANDWEAPON:
                    _offHandWeapon = weapon;
                    _offHandWeapon.gameObject.layer = 7;
                    _offHandWeapon.WeaponTransform.position = _offHandTransform.position;
                    _offHandWeapon.WeaponTransform.rotation = _offHandTransform.rotation;
                    _offHandWeapon.WeaponTransform.parent = _offHandTransform;
                    InitWeapons(EquipableType.OFFHANDWEAPON, _offHandWeapon.GetComponent<Weapon>());
                    _inventory.DeleteItem(_offHandWeapon);
                    _uiController.SetEquipmentInSlot(EquipableType.OFFHANDWEAPON, _offHandWeapon.GetComponent<EquipableItem>());
                    break;
                
            }
        }

        public void TakeOffWeapon(EquipableType weaponType)
        {
            switch (weaponType)
            {
                case EquipableType.MAINHANDWEAPON:
                    _uiController.RemoveEquipmentFromSlot(weaponType);
                    _inventory.AddItem(_mainHandWeapon);
                    _mainHandWeapon.gameObject.layer = 0;
                    _mainHandWeapon.WeaponTransform.parent = null;
                    _mainHandWeapon = null;
                    InitWeapons(EquipableType.MAINHANDWEAPON, null);
                    break;
                case EquipableType.OFFHANDWEAPON:
                    _uiController.RemoveEquipmentFromSlot(weaponType);
                    _inventory.AddItem(_offHandWeapon);
                    _offHandWeapon.gameObject.layer = 0;
                    _offHandWeapon.WeaponTransform.parent = null;
                    _offHandWeapon = null;
                    InitWeapons(EquipableType.OFFHANDWEAPON, null);
                    break;

            }
        }

        private void InitWeapons(EquipableType weaponType, Weapon weapon)
        {
            foreach (var ability in _activeAttacks)
            {
                ability.SetWeapon(weapon, weaponType);
            }

            foreach (var ability in _activeAbilities)
            {
                ability.SetWeapon(weapon, weaponType);
            }
        }
    }
}
