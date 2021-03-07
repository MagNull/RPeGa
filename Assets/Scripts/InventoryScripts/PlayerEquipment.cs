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

        public void PutOnEquipment(EquipableItem equipment, EquipableType equipableType)
        {
            switch (equipableType)
            {
                case EquipableType.MAINHANDWEAPON:
                    _mainHandWeapon = equipment;
                    _mainHandWeapon.gameObject.layer = 7;
                    _mainHandWeapon.WeaponTransform.transform.position = _mainHandTransform.position;
                    _mainHandWeapon.WeaponTransform.rotation = _mainHandTransform.rotation;
                    _mainHandWeapon.WeaponTransform.parent = _mainHandTransform;
                    InitWeapons(EquipableType.MAINHANDWEAPON, _mainHandWeapon.GetComponent<Weapon>());
                    _inventory.DeleteItem(_mainHandWeapon);
                    _uiController.SetEquipmentInSlot(EquipableType.MAINHANDWEAPON, _mainHandWeapon);
                    break;
                case EquipableType.OFFHANDWEAPON:
                    _offHandWeapon = equipment;
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

        public void TakeOffEquipment(EquipableType equipmentType)
        {
            switch (equipmentType)
            {
                case EquipableType.MAINHANDWEAPON:
                    _uiController.RemoveEquipmentFromSlot(equipmentType);
                    TakeOffWeapon(_mainHandWeapon);
                    InitWeapons(EquipableType.MAINHANDWEAPON, null);
                    break;
                case EquipableType.OFFHANDWEAPON:
                    _uiController.RemoveEquipmentFromSlot(equipmentType);
                    TakeOffWeapon(_offHandWeapon);
                    InitWeapons(EquipableType.OFFHANDWEAPON, null);
                    break;
            }
        }

        private void TakeOffWeapon(EquipableItem weapon)
        {
            _inventory.AddItem(weapon);
            weapon.gameObject.layer = 0;
            weapon.WeaponTransform.parent = null;
            weapon = null;
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
