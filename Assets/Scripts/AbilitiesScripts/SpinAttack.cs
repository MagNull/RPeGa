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

    public override void Init(Animator a, AbilityCaster caster, DamageDealer dd)
    {
        base.Init(a, caster, dd);
        _spinTargetTransform = caster.transform;
    }

    public override void Execute(InputHandler inputHandler)
    {
        if (!_animator || !_spinTargetTransform)
        {
            Init(_animator,_caster, _animator.GetComponent<DamageDealer>());
        }
        if (CanUse)
        {
            CanUse = false;
            _caster.StartCoroutine(Spin(_caster, _animator, inputHandler));
        }
    }

    private IEnumerator Spin(AbilityCaster coolDowner,Animator anim,InputHandler inputHandler)
    {
        inputHandler.CanCast = false;
        coolDowner.CurrentMana -= manaCost;
        _damageDealer.ChangeDamageState();
        _damageDealer.damage = Mathf.CeilToInt(abilityDamage / (spinSpeed * spinDuration));
        
        float t = 0;
        anim.SetBool("Spin", true);
        while (t < spinDuration)
        {
            t += Time.deltaTime;
            _spinTargetTransform.Rotate(Vector3.up, 360 * spinSpeed * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }
        _spinTargetTransform.localEulerAngles = Vector3.zero;
        
        anim.SetBool("Spin", false);
        inputHandler.CanCast = true;
        _damageDealer.ChangeDamageState();
        _damageDealer.damage = _damageDealer.PureDamage;
        
        coolDowner.StartCoroutine(StartCooldown());
    }
}