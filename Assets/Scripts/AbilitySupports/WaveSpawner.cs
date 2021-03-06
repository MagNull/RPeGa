using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WeaponScripts;
using Zenject;

namespace AbilitySupports
{
    public class WaveSpawner : MonoBehaviour
    {
        [HideInInspector] public Transform Player;
        [HideInInspector] public GameObject WavePrefab;
        [HideInInspector] public float WaveDistance;
        [HideInInspector] public float WaveSpeed;
        [SerializeField] private int _poolSize = 2;
        [SerializeField] private Material _fireMaterial;
        [SerializeField] private MeshRenderer _swordMeshRenderer;
        private readonly Queue<Transform> _wavesPool = new Queue<Transform>();
        private Material _defaultMaterial;

        private void Start()
        {
            for (int i = 1; i <= _poolSize; i++)
            {
                GameObject wave = Instantiate(WavePrefab);
                if(i % 2 == 0) wave.transform.eulerAngles += new Vector3(0,0,90);
                wave.SetActive(false);
                _wavesPool.Enqueue(wave.transform);
            }
            _defaultMaterial = _swordMeshRenderer.material;
        }

        public void ChargeSword()
        {
            _swordMeshRenderer.material = _fireMaterial;
        }
    
        public void CreateWave()
        {
            Transform wave = _wavesPool.Dequeue();
            wave.position = Player.position;
            wave.rotation = Quaternion.Euler(wave.eulerAngles.x, Player.eulerAngles.y,wave.eulerAngles.z);
            wave.gameObject.SetActive(true);
            _swordMeshRenderer.material = _defaultMaterial;
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
            _wavesPool.Enqueue(wave);
        }
    }
}
