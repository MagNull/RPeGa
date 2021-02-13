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
    [SerializeField] private float speedChange = 2;
    private PlayerSpeedManipulator _playerSpeedManipulator;

    public override void Init(AbilityCaster caster, DamageDealer mainHandWeapon, DamageDealer offHandWeapon, InputHandler inputHandler)
    {
        base.Init(caster, mainHandWeapon, offHandWeapon, inputHandler);
        WaveSpawner waveSpawner = _mainHandWeapon.GetComponent<WaveSpawner>();
        waveSpawner.WavePrefab = wavePrefab;
        waveSpawner.WaveDistance = waveDistance;
        waveSpawner.WaveSpeed = waveSpeed;
        _playerSpeedManipulator = _inputHandler.GetComponent<PlayerSpeedManipulator>();
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
        
        _playerSpeedManipulator.SpeedBonus += speedChange;

        yield return new WaitForSeconds(animDelay);

        _playerSpeedManipulator.SpeedBonus -= speedChange;
        
        yield return new WaitForSeconds(coolDown);

        CanCast = true;
    }
}
