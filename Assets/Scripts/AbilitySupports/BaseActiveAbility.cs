using UnityEngine;
using UnityEngine.UI;
using WeaponScripts;

namespace AbilitySupports
{
    public abstract class BaseActiveAbility : ScriptableObject
    {
        public bool CanCast = true;
        [SerializeField] protected int manaCost = 1;
        [SerializeField] protected float coolDown;
        [SerializeField] protected Text coolDownText;
        [SerializeField] protected Image abilityImage;
        protected AbilityCaster _caster;
        protected Animator _mainHandAnimator;
        protected Animator _offHandAnimator;
        protected Weapon _mainHandWeapon;
        protected Weapon _offHandWeapon;
        protected InputHandler _inputHandler;

        public int ManaCost => manaCost;
    



        public virtual void Init(AbilityCaster caster, Weapon mainHandWeapon, 
            Weapon offHandWeapon, InputHandler inputHandler)
        {
            _mainHandWeapon = mainHandWeapon;
            _offHandWeapon = offHandWeapon;
            _inputHandler = inputHandler;
            _mainHandAnimator = _mainHandWeapon.GetComponent<Animator>();
            _offHandAnimator = _offHandWeapon.GetComponent<Animator>();
            _caster = caster;
            CanCast = true;
        }

        public abstract void Execute();
    
    }
}
