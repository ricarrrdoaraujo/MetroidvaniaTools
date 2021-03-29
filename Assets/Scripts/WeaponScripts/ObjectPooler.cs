using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MetroidvaniaTools
{
    public class ObjectPooler : MonoBehaviour
    {
        private GameObject currentItem;
        
        public void CreatePool(WeaponTypes weapon)
        {
            for (int i = 0; i < weapon.amountToPool; i++)
            {
                currentItem = Instantiate(weapon.projectile);
                currentItem.SetActive(false);
            }
        }
    }
}
