using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MetroidvaniaTools
{
    public class Weapon : Abilities
    {
        [SerializeField] protected List<WeaponTypes> weaponTypes;

        protected override void Initialization()
        {
            base.Initialization();
            foreach (WeaponTypes weapon in weaponTypes)
            {
                objectPooler.CreatePool(weapon);
            }
        }
    }
}

