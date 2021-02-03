using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityCaster : MonoBehaviour
{
    public Animator CurrentWeaponAnimator;
    [SerializeField] private InputHandler inputHandler;
    [SerializeField] private BaseAbility[] abilities;


    private void Awake()
    {
        foreach (var ability in abilities)
        {
            ability.Init(CurrentWeaponAnimator, this, CurrentWeaponAnimator.GetComponent<DamageDealer>());
        }
    }

    private void Start()
    {
        inputHandler.OnCast += CastAbility;
    }

    private void CastAbility(int i)
    {
        abilities[i].Execute(inputHandler);
    }
}
