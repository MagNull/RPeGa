using System.Collections;
using AbilitySupports;
using UnityEngine;
using WeaponScripts;

namespace AbilitiesScripts
{
    [CreateAssetMenu(fileName = "Ability", menuName = "Abilities/Active/Melee Abilities/Cross Wave Attack")]
    public class CrossWaveAttack : BaseActiveAbility
    {
        [SerializeField] private GameObject _wavePrefab;
        [SerializeField] private float _waveDistance = 1;
        [SerializeField] private float _waveSpeed;
        [SerializeField] private float _animDelay = 1;
        [SerializeField] private float _speedChange = 2;
        private PlayerSpeedManipulator _playerSpeedManipulator;

        public override void Init(AbilityCaster caster, Weapon mainHandWeapon, Weapon offHandWeapon, InputHandler inputHandler)
        {
            base.Init(caster, mainHandWeapon, offHandWeapon, inputHandler);
            WaveSpawner waveSpawner = _mainHandWeapon.GetComponent<WaveSpawner>();
            waveSpawner.WavePrefab = _wavePrefab;
            waveSpawner.WaveDistance = _waveDistance;
            waveSpawner.WaveSpeed = _waveSpeed;
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

            _mainHandAnimator.SetTrigger("Cross Wave");
        
            _playerSpeedManipulator.SpeedBonus += _speedChange;

            yield return new WaitForSeconds(_animDelay);

            _playerSpeedManipulator.SpeedBonus -= _speedChange;
        
            yield return new WaitForSeconds(_coolDown);

            CanCast = true;
        }
    }
}
