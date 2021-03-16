using System;
using System.Collections.Generic;
using UIScripts;
using UnityEngine;
using Zenject;

namespace Questing
{
    public class PlayerQuester : MonoBehaviour
    {
        [SerializeField] private List<Quest> _currentQuests = new List<Quest>();
        [Inject] private QuestsUIController _questsUIController;
        public bool AddQuest(Quest quest)
        {
            if (_currentQuests.Contains(quest)) return false;
            _currentQuests.Add(quest);
            _questsUIController.ShowQuestNotification(quest);
            quest.OnComplete += QuestComplete;
            return true;
        }

        private void QuestComplete(Quest quest)
        {
            quest.OnComplete -= QuestComplete;
            _questsUIController.ShowQuestCompleteNotification(quest);
        }
    }
}