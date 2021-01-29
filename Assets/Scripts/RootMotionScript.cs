using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Animator))]
    
public class RootMotionScript : MonoBehaviour {
            
    void OnAnimatorMove()
    {
        Animator animator = GetComponent<Animator>();
        if (animator)
        {
            transform.position += transform.forward * animator.GetFloat("Runspeed") * Time.deltaTime;
        }
    }
}