using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IndieGamePractice
{
    public enum PoolObjectType
    {
        AttackInfo,
        Hammer,
        Hammer_VFX
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
                case PoolObjectType.Hammer:
                    {
                        obj = Instantiate(Resources.Load("ThorHammer", typeof(GameObject))) as GameObject;
                        break;
                    }
                case PoolObjectType.Hammer_VFX:
                    {
                        obj = Instantiate(Resources.Load("VFX_HammerDown", typeof(GameObject))) as GameObject;
                        break;
                    }
            }
            return obj.GetComponent<PoolObject>();
        }
    }
}