using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class CastStateController : MonoBehaviour
{
    [Inject]
    private InputHandler _inputHandler;

    public void SetTrueCastState() => _inputHandler.CanCast = true;
    
    public void SetFalseCastState() => _inputHandler.CanCast = false;

    public void SetTrueAttackState() => _inputHandler.CanAttack = true;
    
    public void SetFalseAttackState() => _inputHandler.CanAttack = false;
    
}
