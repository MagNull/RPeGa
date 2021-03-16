using System;
using UnityEngine;

namespace Questing
{
    [Serializable]
    public abstract class Goal : ScriptableObject
    {
        public string Description { get; set; }
        public bool Completed { get; set; }
        public int CurrentAmount { get; set; }
        public int RequiredAmount { get; set; }
        
        public event Action OnComplete;

        public abstract void Init();

        public void Evaluate()
        {
            if (CurrentAmount >= RequiredAmount) Complete();
        }

        public virtual void Complete()
        {
            Completed = true;
            OnComplete?.Invoke();
        }
    }
}
