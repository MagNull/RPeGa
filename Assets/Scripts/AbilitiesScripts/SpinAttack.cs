using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Ability", menuName = "Abilities/Active/Melee Abilities/Spin Attack")]
public class SpinAttack : BaseActiveAbility
{
    [SerializeField] private float spinSpeed = 1;
    [SerializeField] private float spinDuration = 1;
    [SerializeField] private Transform spinTargetTransform;
    [SerializeField] private int abilityDamage = 1;
    [SerializeField] private float speedBoost = 1.5f;

    public override void Init(AbilityCaster caster, DamageDealer mainHandWeapon, DamageDealer offHandWeapon, InputHandler inputHandler)
    {
        base.Init(caster, mainHandWeapon, offHandWeapon, inputHandler);
        spinTargetTransform = caster.transform;
    }

    public override void Execute()
    {
        if (!_mainHandAnimator || _offHandAnimator || !spinTargetTransform)
        {
            Init(_caster, _mainHandWeapon, _offHandWeapon, _inputHandler);
        }
        _caster.StartCoroutine(Spin(_caster));
    }

    private IEnumerator Spin(AbilityCaster coolDowner)
    {
        _inputHandler.CanCast = false;
        _inputHandler.CanAttack = false;
        CanCast = false;
        
        coolDowner.CurrentMana -= manaCost;
        
        _mainHandWeapon.ChangeDamageState();
        _offHandWeapon.ChangeDamageState();
        
        _mainHandWeapon.damage = (abilityDamage / (spinSpeed * spinDuration)) / 2;
        _offHandWeapon.damage = (abilityDamage / (spinSpeed * spinDuration)) / 2;
        
        _mainHandAnimator.SetBool("Spin", true);
        _offHandAnimator.SetBool("Spin", true);

        _inputHandler.Speed *= speedBoost;
        
        float t = 0;
        while (t < spinDuration)
        {
            t += Time.deltaTime;
            spinTargetTransform.Rotate(Vector3.up, 360 * spinSpeed * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }
        spinTargetTransform.localEulerAngles = Vector3.zero;
        
        _mainHandAnimator.SetBool("Spin", false);
        _offHandAnimator.SetBool("Spin", false);
        
        _inputHandler.CanCast = true;
        _inputHandler.CanAttack = true;
        
        _mainHandWeapon.ChangeDamageState();
        _offHandWeapon.ChangeDamageState();
        
        _mainHandWeapon.damage = _mainHandWeapon.PureDamage;
        _offHandWeapon.damage = _mainHandWeapon.PureDamage;

        _inputHandler.Speed *= 1;
        
        yield return new WaitForSeconds(coolDown);

        CanCast = true;
    }
}