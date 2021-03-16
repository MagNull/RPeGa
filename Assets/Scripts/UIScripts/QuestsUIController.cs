using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Questing;
using UnityEngine;
using UnityEngine.UI;

namespace UIScripts
{
    public class QuestsUIController : MonoBehaviour
    {
        [SerializeField] private GameObject _questNotificationPanel;
        [SerializeField] private Text _notificationText;
        [SerializeField] private float _showAndZipSpeed = 1;
        [SerializeField] private float _notificationLifeTime = 1;

        private void Awake()
        {
            _questNotificationPanel.SetActive(false);
            _notificationText = _questNotificationPanel.GetComponentInChildren<Text>();
        }

        public void ShowQuestNotification(Quest quest)
        {
            _questNotificationPanel.SetActive(true);
            _notificationText.text = quest.Description;
            StartCoroutine(ShowAndZip(_questNotificationPanel.transform));
        }

        public void ShowQuestCompleteNotification(Quest quest)
        {
            _questNotificationPanel.SetActive(true);
            _notificationText.text = quest.QuestName + " completed";
            StartCoroutine(ShowAndZip(_questNotificationPanel.transform));
        }

        private IEnumerator ShowAndZip(Transform target)
        {
            target.localScale = Vector3.zero;
            target.DOScale(Vector3.one, _showAndZipSpeed);
            yield return new WaitForSeconds(_notificationLifeTime);
            target.DOScale(Vector3.zero, _showAndZipSpeed);
        }
    }
}