using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Melee Attack", menuName = "Abilities/Melee Abilities/Melee Attack")]
public class MeleeAttack : BaseAbility
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
            Debug.Log("Attack");
            _caster.StartCoroutine(StartCooldown());

        }
    }
}
