using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IndieGamePractice
{
    public enum PoolObjectType
    {
        AttackInfo,
    }

    public class PoolObjectLoader : MonoBehaviour
    {
        public static PoolObject _GetObjectType(PoolObjectType objectType)
        {
            GameObject obj = null;

            switch (objectType)
            {
                case PoolObjectType.AttackInfo:
                    {
                        obj = Instantiate(Resources.Load("AttackInfo", typeof(GameObject))) as GameObject;
                        break;
                    }
            }
            return obj.GetComponent<PoolObject>();
        }
    }
}