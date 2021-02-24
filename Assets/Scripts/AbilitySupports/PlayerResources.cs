using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

namespace AbilitySupports
{
    public class PlayerResources : MonoBehaviour
    {
        public ReactiveProperty<float> MAXHealth = new ReactiveProperty<float>();
        public ReactiveProperty<float> MAXMana = new ReactiveProperty<float>();
        [HideInInspector] public ReactiveProperty<float> Mana = new ReactiveProperty<float>(0);
        [HideInInspector] public ReactiveProperty<float> Health = new ReactiveProperty<float>(0);

        public void Init()
        {
            Mana.Value = MAXHealth.Value;
            Health.Value = MAXHealth.Value;
        }
    }
}
