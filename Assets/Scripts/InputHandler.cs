using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    public event Action<float> OnMove;
    public event Action<int> OnCast;
    public event Action<int> OnAttack;
    public bool CanMove = true;
    public bool CanCast = true;
    public bool CanAttack = true;
    [SerializeField] private float baseSpeed = 5;
    [SerializeField] private float _currentSpeed;

    public float Speed
    {
        get => baseSpeed;
        set => _currentSpeed = value;
    }


    private void Awake()
    {
        Speed = baseSpeed;
    }

    private void Update()
    {
        if (CanMove)
        {
            OnMove?.Invoke(_currentSpeed);
        }

        if (CanCast)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1)) OnCast?.Invoke(0);
            if (Input.GetKeyDown(KeyCode.Alpha2)) OnCast?.Invoke(1);
            if (Input.GetKeyDown(KeyCode.Alpha3)) OnCast?.Invoke(2);
        }

        if (CanAttack)
        {
            if (Input.GetMouseButtonDown(0)) OnAttack?.Invoke(0);
            if (Input.GetMouseButtonDown(1)) OnAttack?.Invoke(1);
        }
    }
}
