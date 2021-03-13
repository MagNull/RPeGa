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
        private int _crossWaveToHash;


        public override void Init(AbilityCaster caster, InputHandler inputHandler, Animator animator)
        {
            base.Init(caster, inputHandler, animator);
            _crossWaveToHash = Animator.StringToHash("Cross Wave");
        }

        public override void Execute(ReactiveProperty<float> mana)
        {
            if (!(_mainHandWeapon is null))
            {
                mana.Value -= _manaCost;
                
                _caster.StartCoroutine(CrossAttack());
            }
        }

        public override void SetWeapon(Weapon weapon, EquipableType weaponType)
        {
            base.SetWeapon(weapon, weaponType);
            WaveSpawner waveSpawner = _caster.GetComponent<WaveSpawner>();
            if (weapon)
            {
                waveSpawner.SetSword(weapon.GetComponent<MeshRenderer>());
                waveSpawner.enabled = true;
            }
            else
            {
                waveSpawner.enabled = false;
            }
        }

        private IEnumerator CrossAttack()
        {
            CanCast = false;

            _animator.SetTrigger(_crossWaveToHash);
        
            _playerBonuses.SpeedBonus.Value += _speedChange;

            yield return new WaitForSeconds(_animDelay);

            _playerBonuses.SpeedBonus.Value -= _speedChange;
        
            yield return new WaitForSeconds(_coolDown);
            
            CanCast = true;
        }
    }
}
