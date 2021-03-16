using System.ComponentModel;
using AbilitiesScripts;
using Installers;
using InventoryScripts;
using Others;
using UnityEngine;
using WeaponScripts;
using Zenject;

namespace AbilitySupports
{
    public class AbilityCaster : MonoBehaviour
    {
        [SerializeField] private ActiveAbility[] _activeAbilities;
        [SerializeField] private ActiveAbility[] _activeAttacks;
        [SerializeField] private PassiveBonus[] _passiveAbilities;

        private InputHandler _inputHandler;
        private PlayerResources _playerResources;
        private PlayerEquipment _playerEquipment;
        private Animator _animator;


        [Inject]
        public void Construct(InputHandler inputHandler,
            PlayerResources playerResources, PlayerEquipment playerEquipment)
        {
            _inputHandler = inputHandler;
            _playerResources = playerResources;
            _playerEquipment = playerEquipment;
        }

        public ActiveAbility[] GetActiveAttacks() => _activeAttacks;

        public ActiveAbility[] GetActiveAbilities() => _activeAbilities;
 
        private void Start()
        {
            _animator = _inputHandler.GetComponent<PlayerMovement>().PlayerAnimator;
            _playerEquipment.Init(this);
            foreach (var ability in _activeAbilities)
            {
                ability.Init(this,_inputHandler, _animator);
            }
            foreach (var ability in _activeAttacks)
            {
                ability.Init(this, _inputHandler, _animator);
            }
            foreach (var ability in _passiveAbilities)
            {
                ability.Init(_inputHandler.GetComponent<DamageCalculator>(),
                                            _inputHandler.GetComponent<PlayerSpeedManipulator>(),
                                            _playerResources);
                ability.ApplyEffect();
            }
        
            _inputHandler.OnCast += CastAbility;
            _inputHandler.OnAttack += Attack;
            _inputHandler.CanMove = true;
        }
        
        

        private void OnDisable()
        {
            _inputHandler.OnCast -= CastAbility;
            _inputHandler.OnAttack -= Attack;
        }
        

        private void CastAbility(int i)
        {
            if (_activeAbilities.Length > i && _activeAbilities[i].ManaCost <= _playerResources.CurrentMana.Value 
                && _activeAbilities[i].CanCast)
            {
                _activeAbilities[i].Execute(_playerResources.CurrentMana);
            }
        }
    
        private void Attack(int i)
        {
            if (_activeAttacks[i].CanCast)
            {
                _activeAttacks[i].Execute(_playerResources.CurrentMana);
            }
        }
    }
}
