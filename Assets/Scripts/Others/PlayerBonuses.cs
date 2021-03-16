using StatScripts;
using UniRx;
using UnityEngine;

namespace Others
{
    public class PlayerBonuses : MonoBehaviour
    {
        public ReactiveProperty<float> DamageBonus = new ReactiveProperty<float>();
        public ReactiveProperty<float> SpeedBonus = new ReactiveProperty<float>();
        public ReactiveProperty<float> HealthBonus = new ReactiveProperty<float>();
        public ReactiveProperty<float> ManaBonus = new ReactiveProperty<float>();
        public ReactiveProperty<float> ArmorBonus = new ReactiveProperty<float>();
        public Stats PlayerStats;
    }
}
