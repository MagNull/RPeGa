using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public abstract class BaseActiveAbility : ScriptableObject
{
    [SerializeField] protected int manaCost = 1;
    [SerializeField] protected float coolDown;
    [SerializeField] protected Text coolDownText;
    [SerializeField] protected Image abilityImage;
    protected AbilityCaster _caster;
    protected Animator _mainHandAnimator;
    protected Animator _offHandAnimator;
    protected DamageDealer _mainHandWeapon;
    protected DamageDealer _offHandWeapon;
    [SerializeField] protected float cdTimer = 0;

    public int ManaCost => manaCost;

    public bool CanCast => (cdTimer <= 0);



    public virtual void Init(AbilityCaster caster, DamageDealer mainHandWeapon, DamageDealer offHandWeapon)
    {
        _mainHandAnimator = mainHandWeapon.GetComponent<Animator>();
        _offHandAnimator = offHandWeapon.GetComponent<Animator>();
        _mainHandWeapon = mainHandWeapon;
        _offHandWeapon = offHandWeapon;
        _caster = caster;
        cdTimer = 0;
    }

    public abstract void Execute(InputHandler inputHandler);
    

    protected IEnumerator StartCooldown()
    {
        while (cdTimer > 0)
        {
            cdTimer = cdTimer - Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
    }
}
