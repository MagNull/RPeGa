using System.Collections;
using AbilitySupports;
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
        private PlayerSpeedManipulator _playerSpeedManipulator;

        public override void Init(AbilityCaster caster, Weapon mainHandWeapon, Weapon offHandWeapon, InputHandler inputHandler)
        {
            base.Init(caster, mainHandWeapon, offHandWeapon, inputHandler);
            _spinTargetTransform = caster.transform;
            _playerSpeedManipulator = _inputHandler.GetComponent<PlayerSpeedManipulator>();

        }

        public override void Execute()
        {
            if (!_mainHandAnimator || !_offHandAnimator || !_spinTargetTransform)
            {
                Init(_caster, _mainHandWeapon, _offHandWeapon, _inputHandler);
            }
            _caster.StartCoroutine(Spin(_caster));
        }

        private IEnumerator Spin(AbilityCaster coolDowner)
        {
            _inputHandler.CanCast = false;
            _inputHandler.CanAttack = false;
            CanCast = false;
            
            _mainHandWeapon.ChangeDamageState();
            
            _mainHandAnimator.SetBool("Spin", true);
            _offHandAnimator.SetBool("Spin", true);

            _playerSpeedManipulator.SpeedBonus += _speedChange;
        
            float t = 0;
            while (t < _spinDuration)
            {
                t += Time.deltaTime;
                _spinTargetTransform.Rotate(Vector3.up, 360 * _spinSpeed * Time.deltaTime);
                yield return new WaitForEndOfFrame();
            }
            _spinTargetTransform.localEulerAngles = Vector3.zero;
        
            _mainHandAnimator.SetBool("Spin", false);
            _offHandAnimator.SetBool("Spin", false);
        
            _playerSpeedManipulator.SpeedBonus -= _speedChange;
        
            _inputHandler.CanCast = true;
            _inputHandler.CanAttack = true;
        
            ((Sword)_mainHandWeapon).ChangeDamageState();

            yield return new WaitForSeconds(_coolDown);

            CanCast = true;
        }
    }
}