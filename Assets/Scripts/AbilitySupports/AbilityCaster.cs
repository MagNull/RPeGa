using System.ComponentModel;
using Installers;
using InventoryScripts;
using UnityEngine;
using WeaponScripts;
using Zenject;

namespace AbilitySupports
{
    public class AbilityCaster : MonoBehaviour
    {
        public Transform MainHandTransform;
        public Transform OffHandTransform;
        
        [SerializeField] private BaseActiveAbility[] _activeAbilities;
        [SerializeField] private BaseActiveAbility[] _activeAttacks;
        [SerializeField] private BasePassiveAbility[] _passiveAbilities;

        private InputHandler _inputHandler;
        private PlayerResources _playerResources;
        private PlayerEquipment _playerEquipment;
        

        [Inject]
        public void Construct(InputHandler inputHandler,
            PlayerResources playerResources, PlayerEquipment playerEquipment)
        {
            _inputHandler = inputHandler;
            _playerResources = playerResources;
            _playerEquipment = playerEquipment;
        }

        public BaseActiveAbility[] GetActiveAttacks() => _activeAttacks;

        public BaseActiveAbility[] GetActiveAbilities() => _activeAbilities;
 
        private void Start()
        {
            foreach (var ability in _activeAbilities)
            {
                ability.Init(this,_inputHandler);
            }
            foreach (var ability in _activeAttacks)
            {
                ability.Init(this, _inputHandler);
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
            _playerEquipment.Init(this);
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
