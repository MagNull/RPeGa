using UniRx;
using UnityEngine;
using Zenject;

namespace Others
{
    public class PlayerSpeedManipulator : MonoBehaviour
    {
        [SerializeField] private float _baseSpeed = 1;
        [SerializeField] private float _currentSpeed;
        private float _speedBonus = 0;
        [Inject] private PlayerBonuses _playerBonuses;

        public float Speed
        {
            get => _currentSpeed;
        }

        private void Start()
        {
            _playerBonuses.SpeedBonus.Where(x => x != null).Subscribe(_ =>
            {
                _speedBonus = _playerBonuses.SpeedBonus.Value;
                RecalculateSpeed();
            });
            RecalculateSpeed();
        }

        private void RecalculateSpeed()
        {
            _currentSpeed = _baseSpeed + _speedBonus;
            _currentSpeed = Mathf.Clamp(_currentSpeed, 0, 100);
        }
    
    
    }
}
