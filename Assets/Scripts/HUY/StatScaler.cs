using AbilitySupports;
using UnityEngine;
using Zenject;

namespace HUY
{
    public class StatScaler : MonoBehaviour
    {
        [SerializeField] private Stats stats;

        [Header("Strength Scale Coefficient ")] [SerializeField]
        private float strengthDamageCoefficient;
        private float strengthHealthCoefficient;
        [Header("Agility Scale Coefficient ")] [SerializeField]
        private float agilitySpeedCoefficient;
        [Header("Intelligence Scale Coefficient ")] [SerializeField]
        private float intelligenceManaCoefficient;


        private PlayerSpeedManipulator _playerSpeedManipulator;
        private PlayerResources _playerResources;
        private DamageCalculator _damageCalculator;
    
        [Inject]
        public void Construct(PlayerSpeedManipulator playerSpeedManipulator, PlayerResources playerResources, 
            DamageCalculator damageCalculator)
        {
            _playerSpeedManipulator = playerSpeedManipulator;
            _playerResources = playerResources;
            _damageCalculator = damageCalculator;
        }
    

        private void OnEnable()
        {
            stats.OnStatsChange += ScaleStrength;
            stats.OnStatsChange += ScaleAgility;
            stats.OnStatsChange += ScaleIntelligence;
        }

        private void OnDisable()
        {
            stats.OnStatsChange -= ScaleStrength;
            stats.OnStatsChange -= ScaleAgility;
            stats.OnStatsChange -= ScaleIntelligence;
        }

        private void Start()
        {
            ScaleStrength(StatType.Strength, stats.Strength);
            ScaleAgility(StatType.Agility, stats.Agility);
            ScaleIntelligence(StatType.Intelligence, stats.Intelligence);
            _playerResources.Init();
        }

        private void ScaleStrength(StatType type, int delta)
        {
            if (type == StatType.Strength)
            {
                _playerResources.MAXHealth += strengthHealthCoefficient * delta;
                _damageCalculator.DamageBonus += strengthDamageCoefficient * delta; 
            }
        }

        private void ScaleAgility(StatType type, int delta)
        {
            if (type == StatType.Agility)
                _playerSpeedManipulator.SpeedBonus += delta * agilitySpeedCoefficient;
        }

        private void ScaleIntelligence(StatType type, int delta)
        {
            if (type == StatType.Intelligence)
                _playerResources.MAXMana += intelligenceManaCoefficient * delta;
        }
    }
}
