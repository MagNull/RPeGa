using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Stats", menuName = "Player Features/Characteristics")]
public class Stats : ScriptableObject
{
    public event Action<StatType> OnStatsChange; 
    [SerializeField] private int _strength;    
    [SerializeField] private int _agility;
    [SerializeField] private int _intelligence;
    
    

    public int Strength
    {
        get => _strength;
        set
        {
            _strength = value;
            OnStatsChange(StatType.Strength);
        }
    }

    public int Agility
    {
        get => _agility;
        set
        {
            _agility = value;
            OnStatsChange(StatType.Agility);
        }
    }

    public int Intelligence
    {
        get => _intelligence;
        set
        {
            _intelligence = value;
            OnStatsChange(StatType.Intelligence);
        }
    }
}