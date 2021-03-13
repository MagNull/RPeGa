using System;
using System.Collections.Generic;
using AbilitySupports;
using Sirenix.OdinInspector;
using UniRx;
using UnityEngine;
using WeaponScripts;
using Zenject;

namespace InventoryScripts
{
    public class PlayerEquipment : SerializedMonoBehaviour
    {
        private Transform _mainHandTransform;
        private Transform _offHandTransform;
        private Transform _headTransform;
        private Transform _bodyTransform;
        private Transform _glovesTransform;
        private Transform _legsTransform;
        private Transform _bootsTransform;
        
        private EquipableItem _mainHandWeapon;
        private EquipableItem _offHandWeapon;
        private EquipableItem _helmet;
        private EquipableItem _chestPlate;
        private EquipableItem _gloves;
        private EquipableItem _legs;
        private EquipableItem _boots;
        
        
        private AbilityCaster _abilityCaster;
        private BaseActiveAbility[] _activeAbilities;
        private BaseActiveAbility[] _activeAttacks;
        private UIController _uiController;
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
            _abilityCaster
                .ObserveEveryValueChanged(x => _abilityCaster)
                .Subscribe(_ =>
                {
                    _activeAbilities = _abilityCaster.GetActiveAbilities();
                    _activeAttacks = _abilityCaster.GetActiveAttacks();
                });
        }

        public void SetEquipTransforms(Equipper equipper)
        {
            _mainHandTransform = equipper.MainHandTransform;
            _offHandTransform = equipper.OffHandTransform;
            _headTransform = equipper.HeadTransform;
            _bodyTransform = equipper.BodyTransform;
            _glovesTransform = equipper.GlovesTransform;
            _legsTransform = equipper.LegsTransform;
            _bootsTransform = equipper.BootsTransform;
        }

        public void PutOnEquipment(EquipableItem equipment, EquipableType equipableType)
        {
            switch (equipableType)
            {
                case EquipableType.MAINHANDWEAPON:
                    SetEquipment(ref _mainHandWeapon, equipment);
                    _mainHandWeapon.gameObject.layer = 7;
                    _mainHandWeapon.ItemTransform.transform.position = _mainHandTransform.position;
                    _mainHandWeapon.ItemTransform.rotation = _mainHandTransform.rotation;
                    _mainHandWeapon.ItemTransform.parent = _mainHandTransform;
                    InitWeapons(EquipableType.MAINHANDWEAPON, _mainHandWeapon.GetComponent<Weapon>());
                    _inventory.DeleteItem(_mainHandWeapon);
                    _uiController.SetEquipmentInSlot(EquipableType.MAINHANDWEAPON, _mainHandWeapon);
                    break;
                case EquipableType.OFFHANDWEAPON:
                    SetEquipment(ref _offHandWeapon, equipment);
                    _offHandWeapon.gameObject.layer = 7;
                    _offHandWeapon.ItemTransform.position = _offHandTransform.position;
                    _offHandWeapon.ItemTransform.rotation = _offHandTransform.rotation;
                    _offHandWeapon.ItemTransform.parent = _offHandTransform;
                    InitWeapons(EquipableType.OFFHANDWEAPON, _offHandWeapon.GetComponent<Weapon>());
                    _inventory.DeleteItem(_offHandWeapon);
                    _uiController.SetEquipmentInSlot(EquipableType.OFFHANDWEAPON, _offHandWeapon);
                    break;
                case EquipableType.BOOTS:
                    SetEquipment(ref _boots, equipment);
                    _boots.ItemTransform.position = _bootsTransform.position;
                    _boots.ItemTransform.rotation = _bootsTransform.rotation;
                    _boots.ItemTransform.parent = _bootsTransform;
                    _inventory.DeleteItem(_boots);
                    _uiController.SetEquipmentInSlot(EquipableType.BOOTS, _boots);
                    break;
                
            }
        }

        public void TakeOffEquipment(EquipableType equipmentType)
        {
            Debug.Log(1);
            switch (equipmentType)
            {
                case EquipableType.MAINHANDWEAPON:
                    _uiController.RemoveEquipmentFromSlot(equipmentType);
                    _mainHandWeapon.OnDropItem -= DropItem;
                    TakeOffWeapon(_mainHandWeapon);
                    _mainHandWeapon = null;
                    InitWeapons(EquipableType.MAINHANDWEAPON, null);
                    break;
                case EquipableType.OFFHANDWEAPON:
                    _uiController.RemoveEquipmentFromSlot(equipmentType);
                    _offHandWeapon.OnDropItem -= DropItem;
                    TakeOffWeapon(_offHandWeapon);
                    InitWeapons(EquipableType.OFFHANDWEAPON, null);
                    _offHandWeapon = null;
                    break;
                case EquipableType.BOOTS:
                    _uiController.RemoveEquipmentFromSlot(equipmentType);
                    _boots.OnDropItem -= DropItem;
                    TakeOffWeapon(_boots);
                    _boots = null;
                    break;
            }
        }

        private void SetEquipment(ref EquipableItem itemSlot, EquipableItem item)
        {
            if(itemSlot) itemSlot.Use();
            itemSlot = item;
            itemSlot.OnDropItem += DropItem;
        }

        private void TakeOffWeapon(EquipableItem weapon)
        {
            _inventory.AddItem(weapon);
            weapon.gameObject.layer = 0;
            weapon.ItemTransform.parent = null;
            weapon = null;
        }

        private void DropItem(EquipableType type)
        {
            switch (type)
            {
                case EquipableType.MAINHANDWEAPON:
                    _mainHandWeapon.OnDropItem -= DropItem;
                    _mainHandWeapon = null;
                    InitWeapons(EquipableType.MAINHANDWEAPON, null);
                    break;
                case EquipableType.OFFHANDWEAPON:
                    _offHandWeapon.OnDropItem -= DropItem;
                    _offHandWeapon = null;
                    InitWeapons(EquipableType.OFFHANDWEAPON, null);
                    break;
                case EquipableType.BOOTS:
                    _boots.OnDropItem -= DropItem;
                    _boots = null;
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
