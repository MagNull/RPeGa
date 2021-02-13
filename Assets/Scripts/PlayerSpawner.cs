using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using Zenject;

public class PlayerSpawner : MonoBehaviour
{
   [SerializeField] private GameObject warriorPrefab;
   [SerializeField] private GameObject archerPrefab;
   [SerializeField] private GameObject wizardPrefab;
   [SerializeField] private Transform playerTransform;
   [SerializeField] private GameObject classChooseUI;
   
   public void Create(ClassesChooseComponent component)
   {
      GameObject player = null;
      switch (component.classType)
      {
         case Classes.Warrior:
            player = Instantiate(warriorPrefab, playerTransform.position, playerTransform.rotation);
            break;
         case Classes.Archer:
            player = Instantiate(archerPrefab, playerTransform.position, playerTransform.rotation);
            break;
         case Classes.Wizard:
            player = Instantiate(wizardPrefab, playerTransform.position, playerTransform.rotation);
            break;
      }
      if(!(player is null)) player.transform.parent = playerTransform;
      Cursor.lockState = CursorLockMode.Locked;
      Cursor.visible = false;
      playerTransform.GetComponent<MouseRotation>().enabled = true;
      classChooseUI.SetActive(false);
   }
}