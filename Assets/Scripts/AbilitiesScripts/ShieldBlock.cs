using AbilitySupports;
using UnityEngine;
using WeaponScripts;

namespace AbilitiesScripts
{
    [CreateAssetMenu(fileName = "Ability", menuName = "Abilities/Active/Melee Abilities/Shield Block")]
    public class ShieldBlock : BaseActiveAbility
    {
        [SerializeField] private float speedChange = -5;
        private BaseDamageDealer fireShield;
        private PlayerSpeedManipulator _playerSpeedManipulator;
        public override void Init(AbilityCaster caster, Weapon mainHandWeapon, Weapon offHandWeapon, InputHandler inputHandler)
        {
            base.Init(caster, mainHandWeapon, offHandWeapon, inputHandler);
            fireShield = ((BaseDamageDealer)_offHandWeapon).GetComponentInChildren<FireShield>().GetComponent<BaseDamageDealer>();
            fireShield.gameObject.SetActive(false);
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
            fireShield.gameObject.SetActive(!fireShield.gameObject.activeSelf);
            _playerSpeedManipulator.SpeedBonus =
                fireShield.gameObject.activeSelf
                    ? _playerSpeedManipulator.SpeedBonus + speedChange
                    : _playerSpeedManipulator.SpeedBonus - speedChange;
            _inputHandler.CanCast = !fireShield.gameObject.activeSelf;
        }
    }
}
