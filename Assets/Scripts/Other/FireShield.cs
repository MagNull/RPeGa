using UnityEngine;

namespace Other
{
    public class FireShield : MonoBehaviour
    {
        [SerializeField] private float _sinKoef = .5f;
        [SerializeField] private float _minAlpha;
        [SerializeField] private float _fireSpeed = 1;
        private MeshRenderer _meshRenderer;
        private void Awake()
        {
            _meshRenderer = GetComponent<MeshRenderer>();
        }

        private void Start()
        {
            gameObject.SetActive(false);
        }

        private void Update()
        {
            Color c = _meshRenderer.material.color;
            c.a = (_sinKoef * Mathf.Sin(Time.realtimeSinceStartup * _fireSpeed) + _minAlpha) / 100;
            _meshRenderer.material.color = c;
        }
    }
}