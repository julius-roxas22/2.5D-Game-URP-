using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IndieGamePractice
{
    public class PoolManager : Singleton<PoolManager>
    {
        public Dictionary<PoolObjectType, List<GameObject>> _PoolDictionary = new Dictionary<PoolObjectType, List<GameObject>>();

        private void setUpDictionary()
        {
            PoolObjectType[] arr = System.Enum.GetValues(typeof(PoolObjectType)) as PoolObjectType[];

            foreach (PoolObjectType p in arr)
            {
                if (!_PoolDictionary.ContainsKey(p))
                {
                    _PoolDictionary.Add(p, new List<GameObject>());
                }
            }
        }

        public GameObject _InstantiateObject(PoolObjectType objectType)
        {
            if (_PoolDictionary.Count == 0)
            {
                setUpDictionary();
            }

            List<GameObject> list = _PoolDictionary[objectType];
            GameObject obj = null;

            if (list.Count > 0)
            {
                obj = list[0];
                list.RemoveAt(0);
            }
            else
            {
                obj = PoolObjectLoader._GetObjectType(objectType).gameObject;
            }

            return obj;
        }

        public void _AddObject(PoolObject poolObject)
        {
            List<GameObject> list = _PoolDictionary[poolObject.objectType];
            list.Add(poolObject.gameObject);
            poolObject.gameObject.SetActive(false);
        }
    }
}