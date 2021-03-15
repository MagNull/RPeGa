using AbilitySupports;
using Other;
using UnityEngine;
using Zenject;

namespace StatScripts
{
    public class StatScaler : MonoBehaviour
    {
        [SerializeField] private Stats _stats;

        [Header("Strength Scale Coefficient ")]
        [SerializeField] private float _strengthDamageCoefficient;
        [SerializeField] private float _strengthHealthCoefficient;
        [Header("Agility Scale Coefficient ")]
        [SerializeField] private float _agilitySpeedCoefficient;
        [Header("Intelligence Scale Coefficient ")]
        [SerializeField] private float _intelligenceManaCoefficient;


        private PlayerBonuses _playerBonuses;
        private PlayerResources _playerResources;
        private DamageCalculator _damageCalculator;
    
        [Inject]
        public void Construct(PlayerBonuses playerBonuses, PlayerResources playerResources, 
            DamageCalculator damageCalculator)
        {
            _playerBonuses = playerBonuses;
            _playerResources = playerResources;
            _damageCalculator = damageCalculator;
        }
    

        private void OnEnable()
        {
            _stats.OnStatsChange += ScaleStrength;
            _stats.OnStatsChange += ScaleAgility;
            _stats.OnStatsChange += ScaleIntelligence;
        }

        private void OnDisable()
        {
            _stats.OnStatsChange -= ScaleStrength;
            _stats.OnStatsChange -= ScaleAgility;
            _stats.OnStatsChange -= ScaleIntelligence;
        }

        private void Start()
        {
            ScaleStrength(StatType.Strength, _stats.Strength);
            ScaleAgility(StatType.Agility, _stats.Agility);
            ScaleIntelligence(StatType.Intelligence, _stats.Intelligence);
            _playerResources.Init();;
        }

        private void ScaleStrength(StatType type, int delta)
        {
            if (type == StatType.Strength)
            {
                _playerResources.MAXHealth.Value += _strengthHealthCoefficient * delta;
                _damageCalculator.DamageBonus += _strengthDamageCoefficient * delta; 
            }
        }

        private void ScaleAgility(StatType type, int delta)
        {
            if (type == StatType.Agility)
                _playerBonuses.SpeedBonus.Value += delta * _agilitySpeedCoefficient;
        }

        private void ScaleIntelligence(StatType type, int delta)
        {
            if (type == StatType.Intelligence)
                _playerResources.MAXMana.Value += _intelligenceManaCoefficient * delta;
        }
    }
}
