using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public abstract class BaseAbility : ScriptableObject
{
    [SerializeField] protected float coolDown;
    [SerializeField] protected Text coolDownText;
    [SerializeField] protected Image abilityImage;
    [SerializeField] protected int manaCost = 1;
    public bool _canUse = true;
    protected Animator animator;
    public abstract void Execute(AbilityCaster coolDowner, Animator animator, InputHandler inputHandler);
    
    protected IEnumerator StartCooldown()
    {
        _canUse = false;
        yield return new WaitForSeconds(coolDown);
        _canUse = true;
    }
    
    
}
