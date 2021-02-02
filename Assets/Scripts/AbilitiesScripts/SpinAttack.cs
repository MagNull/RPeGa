using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Melee Attack", menuName = "Abilities/Melee Abilities/Spin Attack")]
public class SpinAttack : BaseAbility
{
    [SerializeField] private float spinSpeed = 1;
    [SerializeField] private float spinDuration = 1;
    private Transform _spinTarget;
    public override void Execute(AbilityCaster coolDowner, Animator anim, InputHandler inputHandler)
    {
        if (!animator)
        {
            animator = anim;
            _spinTarget = coolDowner.transform;
        }
        if (_canUse)
        {
            _canUse = false;
            coolDowner.StartCoroutine(Spin(coolDowner, anim, inputHandler));
        }
    }

    private IEnumerator Spin(AbilityCaster coolDowner,Animator anim,InputHandler inputHandler)
    {
        inputHandler.CanCast = false;
        float t = 0;
        anim.SetBool("Spin", true);
        while (t < spinDuration)
        {
            t += Time.deltaTime;
            _spinTarget.Rotate(Vector3.up, 360 * spinSpeed * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }
        _spinTarget.localEulerAngles = Vector3.zero;
        anim.SetBool("Spin", false);
        inputHandler.CanCast = true;
        coolDowner.StartCoroutine(StartCooldown());
    }
}