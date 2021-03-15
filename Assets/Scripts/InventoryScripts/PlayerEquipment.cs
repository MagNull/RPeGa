using System;
using System.Collections.Generic;
using AbilitiesScripts;
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
        private AbilityCaster _abilityCaster;
        private ActiveAbility[] _activeAbilities;
        private ActiveAbility[] _activeAttacks;

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

        public void DoActionWithItem(IActionWithEquippableItem action)
        {
            action.Do();
        }

        public void InitWeapons(WeaponType weaponType, Weapon weapon)
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
