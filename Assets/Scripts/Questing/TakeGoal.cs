using System;
using Others;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

namespace Questing
{
    [CreateAssetMenu(fileName = "Take Goal", menuName = "Goals/Take Goal")]
    public class TakeGoal : Goal
    {
        [SerializeField] private string _itemName;

        public override void Init()
        {
            UnityAction listener = () =>
            {
                CurrentAmount++;
                Evaluate();
            };
            EventBus.StartListening( "Take " + _itemName, listener);
        }

        public override void Complete()
        {
            base.Complete();
            EventBus.RemoveEvent("Take " + _itemName);
        }
    }
}