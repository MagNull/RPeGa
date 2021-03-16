using System;
using UnityEngine;

namespace StatScripts
{
    [Serializable]
    public class Stats
    {
        public event Action<StatType, int> OnStatsChange; 
        [SerializeField] private int _strength;    
        [SerializeField] private int _agility;
        [SerializeField] private int _intelligence;

        // public Stats(int strength, int agility, int intelligence)
        // {
        //     _strength = strength;
        //     _agility = agility;
        //     _intelligence = intelligence;
        // }
        public int Strength
        {
            get => _strength;
            set
            {
                OnStatsChange(StatType.Strength, value - _strength);
                _strength = value;
            }
        }

        public int Agility
        {
            get => _agility;
            set
            {
                OnStatsChange(StatType.Agility, value - _agility);
                _agility = value;
            }
        }

        public int Intelligence
        {
            get => _intelligence;
            set
            {
                OnStatsChange(StatType.Intelligence, value - _intelligence);
                _intelligence = value;
            }
        }
    }
}