using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IndieGamePractice
{
    public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T instance;

        public static T _GetInstance
        {
            get
            {
                if (null == instance)
                {
                    GameObject obj = new GameObject();
                    obj.name = typeof(T).ToString();
                    instance = obj.AddComponent<T>();
                }
                return instance;
            }
        }
    }
}
