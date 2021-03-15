using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WeaponScripts;

public class ShootingEnemy : TestEnemy
{
    [SerializeField] private Projectile _projectilePrefab;
    [SerializeField] private float _shootingInterval = 1;
    [SerializeField] private float _projectileSpeed = 1;
    private readonly Stack<Projectile> _projectilePool = new Stack<Projectile>();

    private void Start()
    {
        for (int i = 0; i < 20; i++)
        {
            Projectile gm = Instantiate(_projectilePrefab);
            gm.gameObject.SetActive(false);
            _projectilePool.Push(gm);
        }

        StartCoroutine(Shooting());
    }

    private IEnumerator Shooting()
    {
        while (true)
        {
            Projectile projectile = _projectilePool.Pop();
            projectile.gameObject.SetActive(true);
            projectile.Stack = _projectilePool;
            projectile.transform.position = transform.forward + transform.position;
            projectile.Rigidbody.velocity = transform.forward * _projectileSpeed;
            yield return new WaitForSeconds(_shootingInterval);
        }
    }
}
