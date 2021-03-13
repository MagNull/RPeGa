using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class CastStateController : StateMachineBehaviour
{
    [Inject] private InputHandler _inputHandler;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _inputHandler.CanAttack = false;
        _inputHandler.CanCast = false;
    }
    
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _inputHandler.CanAttack = true;
        _inputHandler.CanCast = true;
    }
}
