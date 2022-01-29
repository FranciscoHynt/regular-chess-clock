using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ObjectPool
{
    public class ObjectPool : MonoBehaviour
    {
        private PooledObject prefab;
        private readonly List<PooledObject> availableObjects = new List<PooledObject>();

        private int i = 0;

        public PooledObject GetObject()
        {
            PooledObject obj;

            int lastAvailableIndex = availableObjects.Count - 1;
            if (lastAvailableIndex >= 0)
            {
                obj = availableObjects[lastAvailableIndex];
                availableObjects.RemoveAt(lastAvailableIndex);
                obj.gameObject.SetActive(true);
            }
            else
            {
                obj = Instantiate<PooledObject>(prefab);
                i++;
                obj.transform.SetParent(transform, false);
                obj.Pool = this;
            }

            return obj;
        }

        public void AddObject(PooledObject obj)
        {
            obj.gameObject.SetActive(false);
            availableObjects.Add(obj);
        }

        public static ObjectPool GetPool(PooledObject prefab)
        {
            ObjectPool pool;
            GameObject obj = GameObject.Find(prefab.name + " Pool");
            if (obj)
            {
                pool = obj.GetComponent<ObjectPool>();
                if (pool)
                {
                    return pool;
                }
            }

            obj = new GameObject(prefab.name + " Pool");

            DontDestroyOnLoad(obj);
            pool = obj.AddComponent<ObjectPool>();
            pool.prefab = prefab;

            return pool;
        }
    }
}