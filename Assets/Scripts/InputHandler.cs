using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    public Action OnMove;
    public Action<int> OnCast;
    public bool CanMove = true;
    public bool CanCast = true;
    private void Update()
    {
        if (CanMove)
        {
            OnMove();
        }

        if (CanCast)
        {
            if (Input.GetMouseButtonDown(0)) OnCast(0);
            if (Input.GetKeyDown(KeyCode.Alpha1)) OnCast(1);
        }
    }
}
