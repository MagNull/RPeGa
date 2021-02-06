using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastStateController : MonoBehaviour
{
    private InputHandler _inputHandler;

    private void Awake()
    {
        _inputHandler = transform.parent.transform.parent.GetComponent<InputHandler>();
    }

    public void SetTrueCastState() => _inputHandler.CanCast = true;
    
    public void SetFalseCastState() => _inputHandler.CanCast = false;

    public void SetTrueAttackState() => _inputHandler.CanAttack = true;
    
    public void SetFalseAttackState() => _inputHandler.CanAttack = false;
    
}
