using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityCaster : MonoBehaviour
{
    public DamageDealer mainHandWeapon;
    public DamageDealer offHandWeapon;
    [SerializeField] private Slider manaBar;
    [SerializeField] private InputHandler inputHandler;
    [SerializeField] private BaseActiveAbility[] activeAbilities;
    [SerializeField] private BaseActiveAbility[] activeAttacks;
    [SerializeField] private BasePassiveAbility[] passiveAbilities;
    [SerializeField] private int manaPool = 5;
    private float _currentMana;

    public float CurrentMana
    {
        get => _currentMana;
        set
        {
            _currentMana = value;
            manaBar.value = _currentMana / manaPool;
        }
    }


    private void Awake()
    {
        foreach (var ability in activeAbilities)
        {
            ability.Init(this, mainHandWeapon, offHandWeapon);
        }
        foreach (var ability in activeAttacks)
        {
            ability.Init(this, mainHandWeapon, offHandWeapon);
        }
        foreach (var ability in passiveAbilities)
        {
            ability.Init(mainHandWeapon, offHandWeapon,GetComponent<IDamageable>());
        }

        CurrentMana = manaPool;    
    }

    private void Start()
    {
        inputHandler.OnCast += CastAbility;
        inputHandler.OnAttack += Attack;
    }

    private void CastAbility(int i)
    {
        if (activeAbilities[i].ManaCost <= _currentMana && activeAbilities[i].CanCast)
        {
            activeAbilities[i].Execute(inputHandler);
        }
    }
    
    private void Attack(int i)
    {
        if (activeAttacks[i].CanCast)
        {
            activeAttacks[i].Execute(inputHandler);
        }
    }
}
