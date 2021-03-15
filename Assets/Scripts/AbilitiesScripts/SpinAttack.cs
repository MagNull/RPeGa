using System;
using System.Collections;
using AbilitySupports;
using Other;
using UniRx;
using UnityEngine;
using WeaponScripts;

namespace AbilitiesScripts
{
    [CreateAssetMenu(fileName = "Ability", menuName = "Abilities/Active/Melee Abilities/Spin Attack")]
    public class SpinAttack : ActiveAbility
    {
        [SerializeField] private float _spinSpeed = 1;
        [SerializeField] private float _spinDuration = 1;
        [SerializeField] private Transform _spinTargetTransform;
        [SerializeField] private float _speedChange = 1.5f;

        private int _spinAttackToHash;

        public override void Init(AbilityCaster caster,InputHandler inputHandler, Animator animator)
        {
            base.Init(caster, inputHandler, animator);
            _spinTargetTransform = caster.transform;
            _spinAttackToHash = Animator.StringToHash("Spin Attack");
        }
        

        public override void Execute(ReactiveProperty<float> mana)
        {
            if (!(_mainHandWeapon is null) || !(_offHandWeapon is null))
            {
                if (!_spinTargetTransform)
                {
                    Init(_caster, _inputHandler, _animator);
                }
                mana.Value -= _manaCost;
                _caster.StartCoroutine(Spin(_caster));
            }
            
        }

        private IEnumerator Spin(AbilityCaster coolDowner)
        {
            CanCast = false;
            
            _mainHandWeapon?.InvertDamageState();
            _offHandWeapon?.InvertDamageState();
            
            _animator.SetBool(_spinAttackToHash, true);

            _playerBonuses.SpeedBonus.Value += _speedChange;
        
            float t = 0;
            while (t < _spinDuration)
            {
                t += Time.deltaTime;
                _spinTargetTransform.Rotate(Vector3.up, 360 * _spinSpeed * Time.deltaTime);
                yield return new WaitForEndOfFrame();
            }
            _spinTargetTransform.localEulerAngles = Vector3.zero;
        
            _animator.SetBool(_spinAttackToHash, false);
        
            _playerBonuses.SpeedBonus.Value -= _speedChange;

            _mainHandWeapon?.InvertDamageState();
            _offHandWeapon?.InvertDamageState();

            yield return new WaitForSeconds(_coolDown);

            CanCast = true;
        }
    }
}