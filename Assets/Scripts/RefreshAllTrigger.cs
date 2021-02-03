using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RefreshAllTrigger : StateMachineBehaviour
{
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("MA 1");
        animator.ResetTrigger("MA 2");
        animator.ResetTrigger("MA 3");
    }
}
