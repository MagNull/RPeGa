using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Ability", menuName = "Abilities/Active/Melee Abilities/Shield Block")]
public class ShieldBlock : BaseActiveAbility
{
    [SerializeField] private float speedChange = -5;
    private DamageDealer fireShield;
    private PlayerSpeedManipulator _playerSpeedManipulator;
    public override void Init(AbilityCaster caster, DamageDealer mainHandWeapon, DamageDealer offHandWeapon, InputHandler inputHandler)
    {
        base.Init(caster, mainHandWeapon, offHandWeapon, inputHandler);
        fireShield = _offHandWeapon.GetComponentInChildren<FireShield>().GetComponent<DamageDealer>();
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
        fireShield.ChangeDamageState();
        fireShield.gameObject.SetActive(!fireShield.gameObject.activeSelf);
        _playerSpeedManipulator.SpeedBonus =
            fireShield.gameObject.activeSelf
                ? _playerSpeedManipulator.SpeedBonus + speedChange
                : _playerSpeedManipulator.SpeedBonus - speedChange;
        _inputHandler.CanCast = !fireShield.gameObject.activeSelf;
    }
}
