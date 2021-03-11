using System.Collections;
using AbilitySupports;
using UniRx;
using UnityEngine;
using WeaponScripts;

namespace AbilitiesScripts
{
    [CreateAssetMenu(fileName = "Ability", menuName = "Abilities/Active/Melee Abilities/Spin Attack")]
    public class SpinAttack : BaseActiveAbility
    {
        [SerializeField] private float _spinSpeed = 1;
        [SerializeField] private float _spinDuration = 1;
        [SerializeField] private Transform _spinTargetTransform;
        [SerializeField] private float _speedChange = 1.5f;

        public override void Init(AbilityCaster caster,InputHandler inputHandler)
        {
            base.Init(caster, inputHandler);
            _spinTargetTransform = caster.transform;
        }

        public override void Execute(ReactiveProperty<float> mana)
        {
            if (!(_mainHandWeapon is null) || !(_offHandWeapon is null))
            {
                if (!_spinTargetTransform)
                {
                    Init(_caster, _inputHandler);
                }
                mana.Value -= _manaCost;
                _caster.StartCoroutine(Spin(_caster));
            }
            
        }

        private IEnumerator Spin(AbilityCaster coolDowner)
        {
            _inputHandler.CanCast = false;
            _inputHandler.CanAttack = false;
            CanCast = false;
            
            _mainHandWeapon?.ChangeDamageState();
            _offHandWeapon?.ChangeDamageState();
            
            
            _mainHandWeapon?.SetSkillBoolParameter("Spin", true);
            _offHandWeapon?.SetSkillBoolParameter("Spin", true);

            _playerBonuses.SpeedBonus.Value += _speedChange;
        
            float t = 0;
            while (t < _spinDuration)
            {
                t += Time.deltaTime;
                _spinTargetTransform.Rotate(Vector3.up, 360 * _spinSpeed * Time.deltaTime);
                yield return new WaitForEndOfFrame();
            }
            _spinTargetTransform.localEulerAngles = Vector3.zero;
        
            _mainHandWeapon?.SetSkillBoolParameter("Spin", false);
            _offHandWeapon?.SetSkillBoolParameter("Spin", false);
        
            _playerBonuses.SpeedBonus.Value -= _speedChange;
        
            _inputHandler.CanCast = true;
            _inputHandler.CanAttack = true;
        
            _mainHandWeapon?.ChangeDamageState();
            _offHandWeapon?.ChangeDamageState();

            yield return new WaitForSeconds(_coolDown);

            CanCast = true;
        }
    }
}