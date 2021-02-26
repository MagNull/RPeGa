using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using Zenject;

public class PlayerSpawner : MonoBehaviour
{
   [SerializeField] private GameObject _warriorPrefab;
   [SerializeField] private GameObject _archerPrefab;
   [SerializeField] private GameObject _wizardPrefab;
   [SerializeField] private Transform _playerTransform;
   [SerializeField] private GameObject _classChooseUI;
   
   public void Create(ClassesChooseComponent component)
   {
      GameObject player = null;
      switch (component._classType)
      {
         case Classes.Warrior:
            player = Instantiate(_warriorPrefab, _playerTransform.position, _playerTransform.rotation);
            break;
         case Classes.Archer:
            player = Instantiate(_archerPrefab, _playerTransform.position, _playerTransform.rotation);
            break;
         case Classes.Wizard:
            player = Instantiate(_wizardPrefab, _playerTransform.position, _playerTransform.rotation);
            break;
      }
      if(!(player is null)) player.transform.parent = _playerTransform;
      Cursor.lockState = CursorLockMode.Locked;
      Cursor.visible = false;
      _playerTransform.GetComponent<MouseRotation>().enabled = true;
      _classChooseUI.SetActive(false);
   }
}