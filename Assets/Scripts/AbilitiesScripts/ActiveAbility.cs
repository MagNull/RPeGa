using AbilitySupports;
using InventoryScripts;
using Others;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using WeaponScripts;

namespace AbilitiesScripts
{
    public abstract class ActiveAbility : ScriptableObject
    {
        public bool CanCast = true;
        [SerializeField] protected int _manaCost = 1;
        [SerializeField] protected float _coolDown;
        [SerializeField] protected Text _coolDownText;
        [SerializeField] protected Image _abilityImage;
        protected AbilityCaster _caster;
        [SerializeField] protected Weapon _mainHandWeapon;
        protected Weapon _offHandWeapon;
        protected InputHandler _inputHandler;
        protected PlayerBonuses _playerBonuses;
        protected Animator _animator;

        public int ManaCost => _manaCost;
        
        public virtual void Init(AbilityCaster caster, InputHandler inputHandler, Animator animator)
        {
            _inputHandler = inputHandler;
            _caster = caster;
            CanCast = true;
            _mainHandWeapon = null;
            _offHandWeapon = null;
            _playerBonuses = _inputHandler.GetComponent<PlayerBonuses>();
            _animator = animator;
        }

        public virtual void SetWeapon(Weapon weapon, WeaponType weaponType)
        {
            switch (weaponType)
            {
                case WeaponType.MAINHANDWEAPON:
                    _mainHandWeapon = weapon;
                    break;
                case WeaponType.OFFHANDWEAPON:
                    _offHandWeapon = weapon;
                    break;
            }
        }

        public abstract void Execute(ReactiveProperty<float> mana);
    }
}
