using System;
using UnityEngine;
namespace WeaponScripts
{
    public abstract class Weapon : DamageDealer
    {
        public WeaponType WeaponType;
    }
}