using System.Collections;
using System.Collections.Generic;

namespace Viking.Scripts.Monster
{
    using UnityEngine;
    using System.Collections.Generic;

    public class ObjectPool : MonoBehaviour
    {
        private GameObject prefab;
        private int initialPoolSize;
        private List<GameObject> pool = new List<GameObject>();

        public void Initialize(GameObject prefab, int initialPoolSize)
        {
            this.prefab = prefab;
            this.initialPoolSize = initialPoolSize;
            CreatePool();
        }

        private void CreatePool()
        {
            for (int i = 0; i < initialPoolSize; i++)
            {
                GameObject obj = Instantiate(prefab, transform);
                obj.SetActive(false);
                pool.Add(obj);
            }
        }

        public GameObject GetObjectFromPool()
        {
            for (int i = 0; i < pool.Count; i++)
            {
                if (!pool[i].activeInHierarchy)
                {
                    return pool[i];
                }
            }

            GameObject newObj = Instantiate(prefab, transform);
            pool.Add(newObj);
            return newObj;
        }
    }


}