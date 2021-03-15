using AbilitySupports;
using InventoryScripts;
using UniRx;
using UnityEngine;
using WeaponScripts;

namespace AbilitiesScripts
{
    [CreateAssetMenu(fileName = "Ability", menuName = "Abilities/Active/Melee Abilities/Shield Block")]
    public class ShieldBlock : ActiveAbility
    {
        [SerializeField] private float _speedChange = -5;
        private DamageDealer _fireShield;
        private int _inBlockToHash;

        public override void Init(AbilityCaster caster, InputHandler inputHandler, Animator animator)
        {
            base.Init(caster, inputHandler, animator);
            _inBlockToHash = Animator.StringToHash("In Block");
        }

        public override void Execute(ReactiveProperty<float> mana)
        {
            if(!(_offHandWeapon is null))ChangeBlockState();
        }

        public override void SetWeapon(Weapon weapon, WeaponType weaponType)
        {
            base.SetWeapon(weapon, weaponType);
            if (!(_offHandWeapon is null) && weapon.GetType() == typeof(Shield)) 
            {
                _fireShield = ((Shield)_offHandWeapon).FireShield.GetComponent<DamageDealer>();
                _fireShield.gameObject.SetActive(false); 
            }
               
        }

        private void ChangeBlockState()
        {
            bool state = !_fireShield.gameObject.activeSelf;
            _playerBonuses.SpeedBonus.Value =
                state
                    ? _playerBonuses.SpeedBonus.Value + _speedChange
                    : _playerBonuses.SpeedBonus.Value - _speedChange;
            _inputHandler.CanCast = !state;
            _animator.SetBool(_inBlockToHash, state);
            int weight = state ? 1 : 0;
            _animator.SetLayerWeight(1,weight);
            _fireShield.gameObject.SetActive(state);
        }
    }
}
