using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityCaster : MonoBehaviour
{
    public Animator CurrentWeaponAnimator;
    [SerializeField] private InputHandler inputHandler;
    [SerializeField] private BaseAbility[] abilities;


    private void Start()
    {
        inputHandler.OnCast += CastAbility;
    }

    private void CastAbility(int i)
    {
        abilities[i].Execute(this, CurrentWeaponAnimator, inputHandler);
    }
}
