using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using WeaponScripts;
using Zenject;

namespace AbilitySupports
{
    public class WaveSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject _wavePrefab;
        [SerializeField] private float _waveDistance;
        [SerializeField] private float _waveSpeed;
        [SerializeField] private int _poolSize = 2;
        [SerializeField] private Material _fireMaterial;

        private readonly Queue<Transform> _wavesPool = new Queue<Transform>();
        private MeshRenderer _swordMeshRenderer;
        private Material _defaultMaterial;
        

        private void OnDisable()
        {
            _swordMeshRenderer = null;
        }

        private void Start()
        {
            for (int i = 1; i <= _poolSize; i++)
            {
                GameObject wave = Instantiate(_wavePrefab);
                if(i % 2 == 0) wave.transform.eulerAngles += new Vector3(0,0,90);
                wave.SetActive(false);
                _wavesPool.Enqueue(wave.transform);
            }

        }

        public void SetSword(MeshRenderer sword)
        {
            _swordMeshRenderer = sword;
            _defaultMaterial = _swordMeshRenderer.material;
        }
        public void ChargeSword()
        {
            _swordMeshRenderer.material = _fireMaterial;
        }
    
        public void CreateWave()
        {
            Transform wave = _wavesPool.Dequeue();
            wave.position = transform.position + Vector3.up * wave.localScale.y;
            wave.rotation = Quaternion.Euler(wave.eulerAngles.x, transform.eulerAngles.y,wave.eulerAngles.z);
            wave.gameObject.SetActive(true);
            _swordMeshRenderer.material = _defaultMaterial;
            StartCoroutine(PushWave(wave));
        }

        private IEnumerator PushWave(Transform wave)
        {
            Vector3 startPosition = wave.position;
            while ((startPosition - wave.position).magnitude <= _waveDistance)
            {
                wave.Translate(0, 0, _waveSpeed * Time.deltaTime);
                yield return new WaitForEndOfFrame();
            }
        
            wave.gameObject.SetActive(false);
            _wavesPool.Enqueue(wave);
        }
    }
}
