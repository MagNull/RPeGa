using UnityEngine;
using WeaponScripts;
using Zenject;

namespace AbilitySupports
{
    public class AbilityCaster : MonoBehaviour
    {
        public Weapon _mainHandWeapon;
        public Weapon _offHandWeapon;
        [SerializeField] private BaseActiveAbility[] _activeAbilities;
        [SerializeField] private BaseActiveAbility[] _activeAttacks;
        [SerializeField] private BasePassiveAbility[] _passiveAbilities;
    
        private InputHandler _inputHandler;
        private PlayerResources _playerResources;

        [Inject]
        public void Construct(InputHandler inputHandler,
            PlayerResources playerResources)
        {
            _inputHandler = inputHandler;
            _playerResources = playerResources;
        }

        private void Start()
        {
            foreach (var ability in _activeAbilities)
            {
                ability.Init(this, _mainHandWeapon, _offHandWeapon, _inputHandler);
            }
            foreach (var ability in _activeAttacks)
            {
                ability.Init(this, _mainHandWeapon, _offHandWeapon, _inputHandler);
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
            if (_activeAbilities[i].ManaCost <= _playerResources.CurrentMana.Value && _activeAbilities[i].CanCast)
            {
                _playerResources.CurrentMana.Value -= _activeAbilities[i].ManaCost;
                _activeAbilities[i].Execute();
            }
        }
    
        private void Attack(int i)
        {
            if (_activeAttacks[i].CanCast)
            {
                _activeAttacks[i].Execute();
            }
        }
    }
}
