using AbilitySupports;
using InventoryScripts;
using UniRx;
using UnityEngine;
using Zenject;

namespace AbilitiesScripts
{
    [CreateAssetMenu(fileName = "Ability", menuName = "Abilities/Active/Melee Abilities/Melee Sword Attack")]
    public class MeleeWarriorAttack : BaseActiveAbility
    {
        private int _inBlockHash;
        private int _shieldBashToHash;
        [Inject] private PlayerEquipment _playerEquipment;

        public override void Init(AbilityCaster caster, InputHandler inputHandler, Animator animator)
        {
            base.Init(caster, inputHandler, animator);
            _inBlockHash = Animator.StringToHash("In Block");
            _shieldBashToHash = Animator.StringToHash("Shield Bash");
        }

        public override void Execute(ReactiveProperty<float> mana)
        {
            if (_animator.GetBool(_inBlockHash))
            {
                _animator.SetTrigger(_shieldBashToHash);
            }
            else
            {
                if (!(_mainHandWeapon is null))
                {
                    int number = Random.Range(1, 4);
                    _animator.SetTrigger($"Melee Attack {number}");
                }  
            }
        }
    }
}
