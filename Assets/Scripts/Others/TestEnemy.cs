using UnityEngine;
using UnityEngine.UI;

namespace Others
{
    public class TestEnemy : MonoBehaviour, IDamageable
    {
        [SerializeField] private float _maxHealth;
        [SerializeField] private float _currentHealth = 10;
        [SerializeField] private Slider _healthSlider;
        [SerializeField] private Canvas _canvas;
        private Transform _cameraTransform;


        private void Awake()
        {
            _cameraTransform = Camera.main.transform;
        }

        private void OnEnable()
        {
            _currentHealth = _maxHealth;
        }

        private void LateUpdate()
        {
            _canvas.transform.LookAt(_cameraTransform);
            _canvas.transform.Rotate(0,180,0);
        }

        public void TakeDamage(float damage)
        {
            _currentHealth -= damage;
            if (_currentHealth <= 0)
            {
                Die();
            }
            OnDamageTaken(damage);
        }

        public void OnDamageTaken(float damage)
        {
            _healthSlider.value = _currentHealth / _maxHealth;
        }

        private void Die()
        {
            Debug.Log(gameObject.name + " died.");
        }
    }
}
