using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Ability", menuName = "Abilities/Active/Melee Abilities/Shield Block")]
public class ShieldBlock : BaseActiveAbility
{
    private DamageDealer fireShield;
    public override void Init(AbilityCaster caster, DamageDealer mainHandWeapon, DamageDealer offHandWeapon)
    {
        base.Init(caster, mainHandWeapon, offHandWeapon);
        fireShield = _offHandWeapon.GetComponentInChildren<FireShield>().GetComponent<DamageDealer>();
        fireShield.gameObject.SetActive(false);
    }

    public override void Execute(InputHandler inputHandler)
    {
        if (!_mainHandAnimator || !_offHandAnimator)
        {
            Init(_caster, _mainHandWeapon, _offHandWeapon);
        }
        ChangeBlockState(inputHandler);
    }

    private void ChangeBlockState(InputHandler inputHandler)
    {
        _mainHandAnimator.SetTrigger("ShieldOn");
        _offHandAnimator.SetTrigger("ShieldOn");
        fireShield.ChangeDamageState();
        fireShield.gameObject.SetActive(!fireShield.gameObject.activeSelf);
        inputHandler.CanCast = !fireShield.gameObject.activeSelf;
    }
}
