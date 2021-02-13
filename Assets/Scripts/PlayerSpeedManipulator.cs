using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpeedManipulator : MonoBehaviour
{
    [SerializeField] private float baseSpeed = 1;
    [SerializeField] private float _currentSpeed;
    private float _speedBonus = 0;


    public float SpeedBonus
    {
        get => _speedBonus;
        set
        {
            _speedBonus = value;
            RecalculateSpeed();
        }
    }

    public float Speed
    {
        get => _currentSpeed;
    }

    private void Start()
    {
        RecalculateSpeed();
    }

    private void RecalculateSpeed()
    {
        _currentSpeed = baseSpeed + _speedBonus;
        _currentSpeed = Mathf.Clamp(_currentSpeed, 0, 100);
    }
    
    
}
