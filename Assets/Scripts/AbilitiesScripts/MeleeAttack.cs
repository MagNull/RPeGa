using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Ability", menuName = "Abilities/Active/Melee Abilities/Melee Attack")]
public class MeleeAttack : BaseActiveAbility
{
    public override void Execute(InputHandler inputHandler)
    {
        if (CanUse)
        {
            int num = Random.Range(1,4);
            switch (num)
            {
                case 1:
                    _animator.SetTrigger("MA 1");
                    break;
                case 2:
                    _animator.SetTrigger("MA 2");
                    break;
                case 3:
                    _animator.SetTrigger("MA 3");
                    break;
            }
        }
    }
}
