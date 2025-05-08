using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IndieGamePractice
{
    public class Ledge : MonoBehaviour
    {
        public Vector3 _Offset;
        public Vector3 _EndPosition;

        public static bool _IsLedge(GameObject obj)
        {
            if (null != obj.GetComponent<Ledge>())
            {
                return true;
            }
            return false;
        }
    }
}