using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Melee Attack", menuName = "Abilities/Melee Abilities/Cross Wave Attack")]
public class CrossWaveAttack : BaseAbility
{
    [SerializeField] private GameObject wavePrefab;
    [SerializeField] private float waveDistance = 1;
    [SerializeField] private float waveSpeed;
    public override void Init(Animator a, AbilityCaster caster, DamageDealer dd)
    {
        base.Init(a, caster, dd);
        WaveSpawner waveSpawner = _damageDealer.GetComponent<WaveSpawner>();
        waveSpawner.WavePrefab = wavePrefab;
        waveSpawner.WaveDistance = waveDistance;
        waveSpawner.WaveSpeed = waveSpeed;
    }

    public override void Execute(InputHandler inputHandler)
    {
        if (!_animator)
        {
            Init(_animator, _caster, _animator.GetComponent<DamageDealer>());
        }
        if (CanUse)
        {
            CanUse = false;
            _caster.StartCoroutine(CrossAttack(_caster, _animator, inputHandler));
        }
    }

    private IEnumerator CrossAttack(AbilityCaster coolDowner, Animator animator, InputHandler inputHandler)
    {
        animator.SetBool("Cross Wave", true);
        _damageDealer.ChangeDamageState();
        inputHandler.CanCast = false;
        
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        
        inputHandler.CanCast = true;
        animator.SetBool("Cross Wave", false);
        _damageDealer.ChangeDamageState();
        
        coolDowner.StartCoroutine(StartCooldown());
    }
}
