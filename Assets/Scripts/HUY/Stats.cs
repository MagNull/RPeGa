using System;
using UnityEngine;

namespace HUY
{
    [CreateAssetMenu(fileName = "Stats", menuName = "Player Features/Characteristics")]
    public class Stats : ScriptableObject
    {
        public event Action<StatType, int> OnStatsChange; 
        [SerializeField] private int _strength;    
        [SerializeField] private int _agility;
        [SerializeField] private int _intelligence;
    
    

        public int Strength
        {
            get => _strength;
            set
            {
                OnStatsChange(StatType.Strength, _strength - value);
                _strength = value;
            }
        }

        public int Agility
        {
            get => _agility;
            set
            {
                OnStatsChange(StatType.Agility, _agility - value);
                _agility = value;
            }
        }

        public int Intelligence
        {
            get => _intelligence;
            set
            {
                OnStatsChange(StatType.Intelligence, _intelligence - value);
                _intelligence = value;
            }
        }
    }
}