using UnityEngine;
using Zenject;

namespace Questing
{
    public class QuestGiver : MonoBehaviour
    {
        [Inject] private PlayerQuester _quester;

        public bool GiveQuest(Quest quest)
        {
            return _quester.AddQuest(quest);
        }
    }
}