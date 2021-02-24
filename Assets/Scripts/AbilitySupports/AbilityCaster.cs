using UnityEngine;
using WeaponScripts;
using Zenject;

namespace AbilitySupports
{
    public class AbilityCaster : MonoBehaviour
    {
        public Weapon mainHandWeapon;
        public Weapon offHandWeapon;
        [SerializeField] private BaseActiveAbility[] activeAbilities;
        [SerializeField] private BaseActiveAbility[] activeAttacks;
        [SerializeField] private BasePassiveAbility[] passiveAbilities;
    
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
            foreach (var ability in activeAbilities)
            {
                ability.Init(this, mainHandWeapon, offHandWeapon, _inputHandler);
            }
            foreach (var ability in activeAttacks)
            {
                ability.Init(this, mainHandWeapon, offHandWeapon, _inputHandler);
            }
            foreach (var ability in passiveAbilities)
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
            if (activeAbilities[i].ManaCost <= _playerResources.Mana.Value && activeAbilities[i].CanCast)
            {
                _playerResources.Mana.Value -= activeAbilities[i].ManaCost;
                activeAbilities[i].Execute();
            }
        }
    
        private void Attack(int i)
        {
            if (activeAttacks[i].CanCast)
            {
                activeAttacks[i].Execute();
            }
        }
    }
}
