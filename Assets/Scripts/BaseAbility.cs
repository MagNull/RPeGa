using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public abstract class BaseAbility : ScriptableObject
{
    [SerializeField] protected float coolDown;
    [SerializeField] protected Text coolDownText;
    [SerializeField] protected Image abilityImage;
    [SerializeField] protected int manaCost = 1;
    public bool CanUse = true;
    protected AbilityCaster _caster;
    protected Animator _animator;
    protected DamageDealer _damageDealer;
    

    public virtual void Init(Animator a, AbilityCaster caster, DamageDealer dd)
    {
        _animator = a;
        _damageDealer = dd;
        _caster = caster;
        CanUse = true;
    }

    public abstract void Execute(InputHandler inputHandler);

    protected IEnumerator StartCooldown()
    {
        CanUse = false;
        yield return new WaitForSeconds(coolDown);
        CanUse = true;
    }
    
    
}
