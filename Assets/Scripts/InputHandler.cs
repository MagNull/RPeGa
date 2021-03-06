using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class InputHandler : MonoBehaviour //TODO: Change CanDoSMT system.
{
    public event Action OnMove;
    public event Action<int> OnCast;
    public event Action<int> OnAttack;
    
    public bool CanMove = true;
    public bool CanCast = true;
    public bool CanAttack = true;
    

    private void Update()
    {
        if (CanMove)
        {
            OnMove?.Invoke();
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
