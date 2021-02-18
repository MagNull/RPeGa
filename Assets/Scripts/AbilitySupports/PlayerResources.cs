using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace AbilitySupports
{
    public class PlayerResources : MonoBehaviour
    {
        [Header("UI Elements")]
        [SerializeField] private Slider manaSlider;
        [SerializeField] private Slider healthSlider;
        [SerializeField] private TextMeshProUGUI manaText;
        [SerializeField] private TextMeshProUGUI healthText;
    
        [Header("Values")]
        [SerializeField] private float maxHealth;
        [SerializeField] private float maxMana;
        private float _currentHealth = 1;
        private float _currentMana = 1;


        public float Health
        {
            get => _currentHealth;
            set
            {
                if (value <= maxHealth)
                {
                    _currentHealth = value;
                    healthText.text = (value + "/" + MAXHealth);
                    healthSlider.value = _currentHealth / MAXHealth;
                }
            }
        }

        public float Mana
        {
            get => _currentMana;
            set
            {
                if (value <= maxMana)
                {
                    _currentMana = value;
                    manaText.text = (value + "/" + MAXMana);
                    manaSlider.value = _currentMana / MAXMana;
                }
            }
        }

        public float MAXHealth
        {
            get => maxHealth;
            set
            {
                maxHealth = value;
                healthText.text = (value + "/" + MAXHealth);
                healthSlider.value = _currentHealth / MAXHealth;
            }
        }

        public float MAXMana
        {
            get => maxMana;
            set
            {
                maxMana = value;
                manaText.text = (value + "/" + MAXMana);
                manaSlider.value = _currentMana / MAXMana;
            }
        }

        public void Init()
        {
            manaSlider.gameObject.SetActive(true);
            healthSlider.gameObject.SetActive(true);
            Health = maxHealth;
            Mana = maxMana;
        }
    }
}
