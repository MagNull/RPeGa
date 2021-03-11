using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using WeaponScripts;

[RequireComponent(typeof(Rigidbody))]
public class Projectile : BaseDamageDealer
{
    public Stack<Projectile> Stack { private get; set; }
    public Rigidbody Rigidbody { get; private set; }
    [SerializeField] private float _lifeTime = 1;

    protected override void Awake()
    {
        Rigidbody = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        StartCoroutine(Back());
    }

    private IEnumerator Back()
    {
        yield return new WaitForSeconds(_lifeTime);
        Stack.Push(this);
        gameObject.SetActive(false);
    }


    public override void OnCollisionEnter(Collision other)
    {
        base.OnCollisionEnter(other);
        Stack.Push(this);
        gameObject.SetActive(false);
    }
}
