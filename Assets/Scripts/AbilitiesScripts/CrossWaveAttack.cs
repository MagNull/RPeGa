using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Ability", menuName = "Abilities/Active/Melee Abilities/Cross Wave Attack")]
public class CrossWaveAttack : BaseActiveAbility
{
    [SerializeField] private GameObject wavePrefab;
    [SerializeField] private float waveDistance = 1;
    [SerializeField] private float waveSpeed;
    [SerializeField] private float animDelay = 1;
    [SerializeField] private float speedReduce = 2;

    public override void Init(AbilityCaster caster, DamageDealer mainHandWeapon, DamageDealer offHandWeapon)
    {
        base.Init(caster, mainHandWeapon, offHandWeapon);
        WaveSpawner waveSpawner = _mainHandWeapon.GetComponent<WaveSpawner>();
        waveSpawner.WavePrefab = wavePrefab;
        waveSpawner.WaveDistance = waveDistance;
        waveSpawner.WaveSpeed = waveSpeed;
    }

    public override void Execute(InputHandler inputHandler)
    {
        if (!_mainHandAnimator || _offHandAnimator)
        {
            Init(_caster, _mainHandWeapon, _offHandWeapon);
        }
        _caster.StartCoroutine(CrossAttack(_caster, inputHandler));
    }

    private IEnumerator CrossAttack(AbilityCaster coolDowner, InputHandler inputHandler)
    {
        CanCast = false;
        coolDowner.CurrentMana -= manaCost;
        
        _mainHandAnimator.SetTrigger("Cross Wave");
        inputHandler.Speed /= 2; 

        yield return new WaitForSeconds(animDelay);

        inputHandler.Speed *= 1;
        
        yield return new WaitForSeconds(coolDown);

        CanCast = true;
    }
}
