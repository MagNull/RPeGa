using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Melee Attack", menuName = "Abilities/Melee Abilities/Melee Attack")]
public class MeleeAttack : BaseAbility
{

    public override void Execute(AbilityCaster coolDowner, Animator anim, InputHandler inputHandler)
    {
        if (!animator)
        {
            animator = anim;
        }
        if (_canUse)
        {
            int num = Random.Range(1,4);
            switch (num)
            {
                case 1:
                    animator.SetTrigger("MA 1");
                    break;
                case 2:
                    animator.SetTrigger("MA 2");
                    break;
                case 3:
                    animator.SetTrigger("MA 3");
                    break;
            }
            Debug.Log("Attack");
            coolDowner.StartCoroutine(StartCooldown());

        }
    }
}
