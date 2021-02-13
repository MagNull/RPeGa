using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class StatScaler : MonoBehaviour
{
    [SerializeField] private Stats stats;

    [Header("Stats Scale Coefficient ")] [SerializeField]
    private float strengthPowerCoefficient;
    [Header("Stats Scale Coefficient ")] [SerializeField]
    private float agilitySpeedCoefficient;
    [Header("Stats Scale Coefficient ")] [SerializeField]
    private float intelligenceManaCoefficient;


    [Inject] private PlayerSpeedManipulator _playerSpeedManipulator;

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

    private void Awake()
    {
        ScaleAgility(StatType.Agility, stats.Agility);
    }

    private void ScaleStrength(StatType type, int delta)
    {
        
    }

    private void ScaleAgility(StatType type, int delta)
    {
        if (type == StatType.Agility)
            _playerSpeedManipulator.SpeedBonus += stats.Agility * agilitySpeedCoefficient;
    }

    private void ScaleIntelligence(StatType type, int delta)
    {
        
    }
}
