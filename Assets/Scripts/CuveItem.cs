using System.Collections;
using System.Collections.Generic;
using InventoryScripts;
using UnityEngine;

public class CuveItem : Item
{
   public override void Use()
   {
      Debug.Log(name + " was used.");
   }
   
   
}
