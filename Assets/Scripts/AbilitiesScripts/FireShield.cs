using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireShield : MonoBehaviour
{
    [SerializeField] private MeshRenderer shieldStone;
    [SerializeField] private float sinKoef = .5f;
    [SerializeField] private float minAlpha;
    [SerializeField] private float fireSpeed = 1;
    private MeshRenderer _meshRenderer;

    private void OnEnable()
    {
        shieldStone.material.EnableKeyword("_EMISSION");
    }

    private void OnDisable()
    {
        shieldStone.material.DisableKeyword("_EMISSION");
    }

    private void Awake()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
    }

    private void Update()
    {
        Color c = _meshRenderer.material.color;
        c.a = (sinKoef * Mathf.Sin(Time.realtimeSinceStartup * fireSpeed) + minAlpha) / 100;
        _meshRenderer.material.color = c;
    }
}
