using AbilitySupports;
using InventoryScripts;
using UniRx;
using UnityEngine;
using WeaponScripts;

namespace AbilitiesScripts
{
    [CreateAssetMenu(fileName = "Ability", menuName = "Abilities/Active/Melee Abilities/Shield Block")]
    public class ShieldBlock : BaseActiveAbility
    {
        [SerializeField] private float _speedChange = -5;
        private BaseDamageDealer _fireShield;
        private PlayerSpeedManipulator _playerSpeedManipulator;
        public override void Init(AbilityCaster caster,InputHandler inputHandler)
        {
            base.Init(caster, inputHandler);
            _playerSpeedManipulator = _inputHandler.GetComponent<PlayerSpeedManipulator>();
        }

        public override void Execute(ReactiveProperty<float> mana)
        {
            if(!(_offHandWeapon is null))ChangeBlockState();
        }

        public override void SetWeapon(Weapon weapon, EquipableType weaponType)
        {
            base.SetWeapon(weapon, weaponType);
            if (!(_offHandWeapon is null) && weapon.GetType() == typeof(Shield)) 
            {
                _fireShield = ((Shield)_offHandWeapon).FireShield.GetComponent<BaseDamageDealer>();
                _fireShield.gameObject.SetActive(false); 
            }
               
        }

        private void ChangeBlockState()
        {
            bool state = !_fireShield.gameObject.activeSelf;
            _playerSpeedManipulator.SpeedBonus =
                state
                    ? _playerSpeedManipulator.SpeedBonus + _speedChange
                    : _playerSpeedManipulator.SpeedBonus - _speedChange;
            _inputHandler.CanCast = !state;
            _mainHandWeapon?.SetSkillBoolParameter("ShieldOn", state);
            _offHandWeapon.SetSkillBoolParameter("ShieldOn", state);
            _fireShield.gameObject.SetActive(state);
        }
    }
}
