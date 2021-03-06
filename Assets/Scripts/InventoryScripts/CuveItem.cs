using UnityEngine;

namespace InventoryScripts
{
   public class CuveItem : Item
   {
      public override void TakeItem(Slot slot)
      {
         base.TakeItem(slot);
         gameObject.SetActive(false);
      }

      public override void Use()
      {
         Debug.Log(name + " was used.");
      }
   }
}
