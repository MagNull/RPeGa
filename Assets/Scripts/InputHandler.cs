using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    public Action<float> OnMove;
    public Action<int> OnCast;
    public Action<int> OnAttack;
    public bool CanMove = true;
    public bool CanCast = true;
    public bool CanAttack = true;
    [SerializeField] private float baseSpeed = 5;
    private float _currentSpeed;

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
            OnMove(_currentSpeed);
        }

        if (CanCast)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1)) OnCast(0);
            if (Input.GetKeyDown(KeyCode.Alpha2)) OnCast(1);
            if (Input.GetKeyDown(KeyCode.Alpha3)) OnCast(2);
        }

        if (CanAttack)
        {
            if (Input.GetMouseButtonDown(0)) OnAttack(0);
            if (Input.GetMouseButtonDown(1)) OnAttack(1);
        }
    }
}
