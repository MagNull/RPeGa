using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Ability", menuName = "Abilities/Active/Melee Abilities/Spin Attack")]
public class SpinAttack : BaseActiveAbility
{
    [SerializeField] private float spinSpeed = 1;
    [SerializeField] private float spinDuration = 1;
    [SerializeField] private Transform _spinTargetTransform;
    [SerializeField] private int abilityDamage = 1;

    public override void Init(AbilityCaster caster, DamageDealer mainHandWeapon, DamageDealer offHandWeapon)
    {
        base.Init(caster, mainHandWeapon, offHandWeapon);
        _spinTargetTransform = caster.transform;
    }

    public override void Execute(InputHandler inputHandler)
    {
        if (!_mainHandAnimator || _offHandAnimator || !_spinTargetTransform)
        {
            Init(_caster, _mainHandWeapon, _offHandWeapon);
        }
        cdTimer = coolDown;
        _caster.StartCoroutine(Spin(_caster, inputHandler));
    }

    private IEnumerator Spin(AbilityCaster coolDowner,InputHandler inputHandler)
    {
        inputHandler.CanCast = false;
        inputHandler.CanAttack = false;
        
        coolDowner.CurrentMana -= manaCost;
        
        _mainHandWeapon.ChangeDamageState();
        _offHandWeapon.ChangeDamageState();
        
        _mainHandWeapon.damage = Mathf.CeilToInt(abilityDamage / (spinSpeed * spinDuration)) / 2;
        _offHandWeapon.damage = Mathf.CeilToInt(abilityDamage / (spinSpeed * spinDuration)) / 2;
        
        _mainHandAnimator.SetBool("Spin", true);
        _offHandAnimator.SetBool("Spin", true);
        
        float t = 0;
        while (t < spinDuration)
        {
            t += Time.deltaTime;
            _spinTargetTransform.Rotate(Vector3.up, 360 * spinSpeed * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }
        _spinTargetTransform.localEulerAngles = Vector3.zero;
        
        _mainHandAnimator.SetBool("Spin", false);
        _offHandAnimator.SetBool("Spin", false);
        
        inputHandler.CanCast = true;
        inputHandler.CanAttack = true;
        
        _mainHandWeapon.ChangeDamageState();
        _offHandWeapon.ChangeDamageState();
        
        _mainHandWeapon.damage = _mainHandWeapon.PureDamage;
        _offHandWeapon.damage = _mainHandWeapon.PureDamage;
        
        coolDowner.StartCoroutine(StartCooldown());
    }
}