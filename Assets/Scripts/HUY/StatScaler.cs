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
    

    [Inject]
    private InputHandler _inputHandler;

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
        ScaleAgility(StatType.Agility);
    }

    private void ScaleStrength(StatType type)
    {
        
    }

    private void ScaleAgility(StatType type)
    {
        if (type == StatType.Agility)
            _inputHandler.Speed = _inputHandler.Speed + stats.Agility * agilitySpeedCoefficient;
    }

    private void ScaleIntelligence(StatType type)
    {
        
    }
}
