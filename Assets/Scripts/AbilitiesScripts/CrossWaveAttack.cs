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

    public override void Init(AbilityCaster caster, DamageDealer mainHandWeapon, DamageDealer offHandWeapon, InputHandler inputHandler)
    {
        base.Init(caster, mainHandWeapon, offHandWeapon, inputHandler);
        WaveSpawner waveSpawner = _mainHandWeapon.GetComponent<WaveSpawner>();
        waveSpawner.WavePrefab = wavePrefab;
        waveSpawner.WaveDistance = waveDistance;
        waveSpawner.WaveSpeed = waveSpeed;
    }

    public override void Execute()
    {
        if (!_mainHandAnimator || _offHandAnimator)
        {
            Init(_caster, _mainHandWeapon, _offHandWeapon, _inputHandler);
        }
        _caster.StartCoroutine(CrossAttack(_caster));
    }

    private IEnumerator CrossAttack(AbilityCaster coolDowner)
    {
        CanCast = false;
        coolDowner.CurrentMana -= manaCost;
        
        _mainHandAnimator.SetTrigger("Cross Wave");
        _inputHandler.Speed /= 2; 

        yield return new WaitForSeconds(animDelay);

        _inputHandler.Speed *= 1;
        
        yield return new WaitForSeconds(coolDown);

        CanCast = true;
    }
}
