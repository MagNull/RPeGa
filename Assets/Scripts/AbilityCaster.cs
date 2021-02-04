using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityCaster : MonoBehaviour
{
    public Animator CurrentWeaponAnimator;
    [SerializeField] private Slider manaBar;
    [SerializeField] private InputHandler inputHandler;
    [SerializeField] private BaseActiveAbility[] activeAbilities;
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
            ability.Init(CurrentWeaponAnimator, this, CurrentWeaponAnimator.GetComponent<DamageDealer>());
        }
        foreach (var ability in passiveAbilities)
        {
            ability.Init(CurrentWeaponAnimator.GetComponent<DamageDealer>(),GetComponent<IDamageable>());
        }

        CurrentMana = manaPool;    
    }

    private void Start()
    {
        inputHandler.OnCast += CastAbility;
    }

    private void CastAbility(int i)
    {
        if (activeAbilities[i].ManaCost <= _currentMana)
        {
            activeAbilities[i].Execute(inputHandler);
        }
    }
}
