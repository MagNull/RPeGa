using System;
using Questing;
using UnityEngine;
using Zenject;

namespace DialogSystemScripts
{
    [CreateAssetMenu(fileName = "Dialog", menuName = "Dialog/Quest Phrase")]
    public class QuestDialog : UsualDialog
    {
        [SerializeField] private Quest _quest;

        public override void ChooseDialog(Dialoger dialoger)
        {
            base.ChooseDialog(dialoger);
            if(dialoger.GetComponent<QuestGiver>().GiveQuest(_quest)) _quest.Init();
            
        }
    }
}