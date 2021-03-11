using System.Collections;
using AbilitySupports;
using InventoryScripts;
using UniRx;
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

        public override void SetWeapon(Weapon weapon, EquipableType weaponType)
        {
            base.SetWeapon(weapon, weaponType);
            if(!(_mainHandWeapon is null))
            {
                SetWaveSpawner(_mainHandWeapon.GetComponentInParent<WaveSpawner>());
            }
        }

        private void SetWaveSpawner(WaveSpawner waveSpawner)
        {
            waveSpawner.enabled = true;
            waveSpawner.WavePrefab = _wavePrefab;
            waveSpawner.WaveDistance = _waveDistance;
            waveSpawner.WaveSpeed = _waveSpeed;
            waveSpawner.Player = _playerBonuses.transform;
        }

        public override void Execute(ReactiveProperty<float> mana)
        {
            if (!(_mainHandWeapon is null))
            {
                mana.Value -= _manaCost;
                _caster.StartCoroutine(CrossAttack());
            }
        }

        private IEnumerator CrossAttack()
        {
            CanCast = false;

            _mainHandWeapon.SetSkillTrigger("Cross Wave");
        
            _playerBonuses.SpeedBonus.Value += _speedChange;

            yield return new WaitForSeconds(_animDelay);

            _playerBonuses.SpeedBonus.Value -= _speedChange;
        
            yield return new WaitForSeconds(_coolDown);

            CanCast = true;
        }
    }
}
