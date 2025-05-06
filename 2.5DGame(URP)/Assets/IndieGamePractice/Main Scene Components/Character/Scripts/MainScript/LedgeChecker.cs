using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IndieGamePractice
{
    public class LedgeChecker : MonoBehaviour
    {
        public bool _IsGrabbingLedge;
        public Ledge _Ledge;
        private Ledge checkingLedge;

        private void OnTriggerEnter(Collider col)
        {
            checkingLedge = col.gameObject.GetComponent<Ledge>();
            if (null != checkingLedge)
            {
                _IsGrabbingLedge = true;
                _Ledge = checkingLedge;
            }
        }

        private void OnTriggerExit(Collider col)
        {
            checkingLedge = col.gameObject.GetComponent<Ledge>();
            if (null != checkingLedge)
            {
                _IsGrabbingLedge = false;
            }
        }

        public static bool _IsLedgeChecker(GameObject obj)
        {
            if (null == obj.GetComponent<LedgeChecker>())
            {
                return false;
            }
            return true;
        }
    }
}