using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class AbilityCaster : MonoBehaviour
{
    public DamageDealer mainHandWeapon;
    public DamageDealer offHandWeapon;
    [SerializeField] private BaseActiveAbility[] activeAbilities;
    [SerializeField] private BaseActiveAbility[] activeAttacks;
    [SerializeField] private BasePassiveAbility[] passiveAbilities;
    [SerializeField] private int manaPool = 5;
    private float _currentMana;
    
    [Inject]
    private InputHandler _inputHandler;
    
    [Inject(Id = "Mana")]
    private Slider _manaBar;

    public float CurrentMana
    {
        get => _currentMana;
        set
        {
            if (value <= manaPool)
            {
                _currentMana = value;
                _manaBar.value = _currentMana / ManaPool;
            }
        }
    }

    public int ManaPool
    {
        get => manaPool;
        set => manaPool = value;
    }

    private void Start()
    {
        foreach (var ability in activeAbilities)
        {
            ability.Init(this, mainHandWeapon, offHandWeapon, _inputHandler);
        }
        foreach (var ability in activeAttacks)
        {
            ability.Init(this, mainHandWeapon, offHandWeapon, _inputHandler);
        }
        foreach (var ability in passiveAbilities)
        {
            ability.Init(mainHandWeapon, offHandWeapon,GetComponent<IDamageable>());
        }

        CurrentMana = ManaPool;
        _manaBar.gameObject.SetActive(true);
        _inputHandler.OnCast += CastAbility;
        _inputHandler.OnAttack += Attack;
        _inputHandler.CanMove = true;
    }

    private void OnDisable()
    {
        _inputHandler.OnCast -= CastAbility;
        _inputHandler.OnAttack -= Attack;
    }

    private void CastAbility(int i)
    {
        if (activeAbilities[i].ManaCost <= _currentMana && activeAbilities[i].CanCast)
        {
            activeAbilities[i].Execute();
        }
    }
    
    private void Attack(int i)
    {
        if (activeAttacks[i].CanCast)
        {
            activeAttacks[i].Execute();
        }
    }
}
