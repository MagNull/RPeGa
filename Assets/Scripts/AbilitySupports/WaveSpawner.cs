using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    [HideInInspector] public GameObject WavePrefab;
    [HideInInspector] public float WaveDistance;
    [HideInInspector] public float WaveSpeed;
    [SerializeField] private int poolSize = 2;
    [SerializeField] private Material fireMaterial;
    private Queue<Transform> wavesPool = new Queue<Transform>();
    private Material _defaultMaterial;
    private MeshRenderer _meshRenderer;

    private void Awake()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
    }

    private void Start()
    {
        for (int i = 1; i <= poolSize; i++)
        {
            GameObject wave = Instantiate(WavePrefab);
            wave.GetComponent<DamageDealer>().ChangeDamageState();
            if(i % 2 == 0) wave.transform.eulerAngles += new Vector3(0,0,90);
            wave.SetActive(false);
            wavesPool.Enqueue(wave.transform);
        }
        _defaultMaterial = _meshRenderer.material;
    }

    public void ChargeSword()
    {
        _meshRenderer.material = fireMaterial;
    }
    
    public void CreateWave()
    {
        Transform wave = wavesPool.Dequeue();
        wave.position = transform.parent.transform.parent.position;
        wave.rotation = Quaternion.Euler(wave.eulerAngles.x,transform.parent.transform.parent.eulerAngles.y,wave.eulerAngles.z);
        wave.gameObject.SetActive(true);
        _meshRenderer.material = _defaultMaterial;
        StartCoroutine(PushWave(wave));
    }

    private IEnumerator PushWave(Transform wave)
    {
        Vector3 startPosition = wave.position;
        while ((startPosition - wave.position).magnitude <= WaveDistance)
        {
            wave.Translate(0, 0, WaveSpeed * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }
        
        wave.gameObject.SetActive(false);
        wavesPool.Enqueue(wave);
    }
}
