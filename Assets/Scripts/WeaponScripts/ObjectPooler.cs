using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MetroidvaniaTools
{
    public class ObjectPooler : MonoBehaviour
    {
        private static ObjectPooler instance;
        public static ObjectPooler Instance
        {
            get
            {
                if (instance == null)
                {
                    GameObject obj = new GameObject("ObjectPooler");
                    obj.AddComponent<ObjectPooler>();
                }
                return instance;
            }
        }

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
            
        }
        
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
