using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;


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
    protected InputHandler _inputHandler;

    public int ManaCost => manaCost;
    



    public virtual void Init(AbilityCaster caster, DamageDealer mainHandWeapon, 
                            DamageDealer offHandWeapon, InputHandler inputHandler)
    {
        _mainHandAnimator = mainHandWeapon.GetComponent<Animator>();
        _offHandAnimator = offHandWeapon.GetComponent<Animator>();
        _mainHandWeapon = mainHandWeapon;
        _offHandWeapon = offHandWeapon;
        _inputHandler = inputHandler;
        _caster = caster;
        CanCast = true;
    }

    public abstract void Execute();
    
}
