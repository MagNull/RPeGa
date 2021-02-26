using AbilitySupports;
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
        public override void Init(AbilityCaster caster, Weapon mainHandWeapon, Weapon offHandWeapon, InputHandler inputHandler)
        {
            base.Init(caster, mainHandWeapon, offHandWeapon, inputHandler);
            _fireShield = ((BaseDamageDealer)_offHandWeapon).GetComponentInChildren<FireShield>().GetComponent<BaseDamageDealer>();
            _fireShield.gameObject.SetActive(false);
            _playerSpeedManipulator = _inputHandler.GetComponent<PlayerSpeedManipulator>();
        }

        public override void Execute()
        {
            if (!_mainHandAnimator || !_offHandAnimator)
            {
                Init(_caster, _mainHandWeapon, _offHandWeapon, _inputHandler);
            }
            ChangeBlockState();
        }

        private void ChangeBlockState()
        {
            _mainHandAnimator.SetTrigger("ShieldOn");
            _offHandAnimator.SetTrigger("ShieldOn");
            _fireShield.gameObject.SetActive(!_fireShield.gameObject.activeSelf);
            _playerSpeedManipulator.SpeedBonus =
                _fireShield.gameObject.activeSelf
                    ? _playerSpeedManipulator.SpeedBonus + _speedChange
                    : _playerSpeedManipulator.SpeedBonus - _speedChange;
            _inputHandler.CanCast = !_fireShield.gameObject.activeSelf;
        }
    }
}
