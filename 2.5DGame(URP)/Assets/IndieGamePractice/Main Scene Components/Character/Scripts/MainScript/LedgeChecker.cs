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
            if (null != checkingLedge)
            {
                _IsGrabbingLedge = false;
            }
        }
    }
}