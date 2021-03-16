using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.Utilities;
using UniRx;
using UnityEngine;

namespace Questing
{
    [Serializable]
    public class Quest
    {
        public List<Goal> _goals = new List<Goal>();
        public string QuestName;
        public string Description;
        public bool Completed;
        public event Action<Quest> OnComplete;
        public void Init()
        {
            _goals.ToList().ForEach(g =>
            {
                g.Init();
                g.OnComplete += CheckGoals;
            });
            Completed = false;
        }
        
        private void CheckGoals()
        {
            if (_goals.All(g => g.Completed)) Complete();
        }

        private void Complete()
        {
            Completed = true;
            OnComplete?.Invoke(this);
        }
    }
}