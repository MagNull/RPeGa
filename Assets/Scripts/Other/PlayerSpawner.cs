using UnityEngine;
using Zenject;

namespace Other
{
   public class PlayerSpawner : MonoBehaviour
   {
      [SerializeField] private GameObject _warriorPrefab;
      [SerializeField] private GameObject _archerPrefab;
      [SerializeField] private GameObject _wizardPrefab;
      [SerializeField] private Transform _playerTransform;
      [SerializeField] private GameObject _classChooseUI;
      [Inject] private PlayerMovement _playerMovement;
   
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

         _playerMovement.PlayerAnimator = player.GetComponent<Animator>();
         player.transform.parent = _playerTransform;
         player.transform.position = new Vector3(
            player.transform.position.x,
            0,
            player.transform.position.z);
         Cursor.lockState = CursorLockMode.Locked;
         Cursor.visible = false;
         _playerTransform.GetComponent<MouseRotation>().enabled = true;
         _classChooseUI.SetActive(false);
      }
   }
}