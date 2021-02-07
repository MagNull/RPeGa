using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public abstract class BaseActiveAbility : ScriptableObject
{
    public bool CanCast = true;
    [SerializeField] protected int manaCost = 1;
    [SerializeField] protected float coolDown;
    [SerializeField] protected Text coolDownText;
    [SerializeField] protected Image abilityImage;
    protected AbilityCaster _caster;
    protected Animator _mainHandAnimator;
    protected Animator _offHandAnimator;
    protected DamageDealer _mainHandWeapon;
    protected DamageDealer _offHandWeapon;

    public int ManaCost => manaCost;
    



    public virtual void Init(AbilityCaster caster, DamageDealer mainHandWeapon, DamageDealer offHandWeapon)
    {
        _mainHandAnimator = mainHandWeapon.GetComponent<Animator>();
        _offHandAnimator = offHandWeapon.GetComponent<Animator>();
        _mainHandWeapon = mainHandWeapon;
        _offHandWeapon = offHandWeapon;
        _caster = caster;
        CanCast = true;
    }

    public abstract void Execute(InputHandler inputHandler);
    
    //
    // protected IEnumerator StartCooldown()
    // {
    //     while (cdTimer > 0)
    //     {
    //         cdTimer = cdTimer - Time.deltaTime;
    //         yield return new WaitForEndOfFrame();
    //     }
    // }
}
