using System;
using InventoryScripts;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using WeaponScripts;

namespace AbilitySupports
{
    public abstract class BaseActiveAbility : ScriptableObject
    {
        public bool CanCast = true;
        [SerializeField] protected int _manaCost = 1;
        [SerializeField] protected float _coolDown;
        [SerializeField] protected Text _coolDownText;
        [SerializeField] protected Image _abilityImage;
        protected AbilityCaster _caster;
        protected Weapon _mainHandWeapon;
        protected Weapon _offHandWeapon;
        protected InputHandler _inputHandler;

        public int ManaCost => _manaCost;
        
        public virtual void Init(AbilityCaster caster, InputHandler inputHandler)
        {
            _inputHandler = inputHandler;
            _caster = caster;
            CanCast = true;
            _mainHandWeapon = null;
            _offHandWeapon = null;
        }

        public virtual void SetWeapon(Weapon weapon, EquipableType weaponType)
        {
            switch (weaponType)
            {
                case EquipableType.MAINHANDWEAPON:
                    _mainHandWeapon = weapon;
                    break;
                case EquipableType.OFFHANDWEAPON:
                    _offHandWeapon = weapon;
                    break;
            }
        }

        public abstract void Execute(ReactiveProperty<float> mana);
    }
}
