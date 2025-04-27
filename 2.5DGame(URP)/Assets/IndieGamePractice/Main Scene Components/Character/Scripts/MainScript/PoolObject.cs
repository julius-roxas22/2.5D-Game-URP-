using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IndieGamePractice
{
    public class PoolObject : MonoBehaviour
    {
        [SerializeField] private float timeScheduledOff;
        public PoolObjectType objectType;
        private Coroutine coroutine;

        public void _TurnOff()
        {
            PoolManager._GetInstance._AddObject(this);
        }

        private IEnumerator scheduledOff()
        {
            yield return new WaitForSeconds(timeScheduledOff);
            if (!PoolManager._GetInstance._PoolDictionary[objectType].Contains(gameObject))
            {
                _TurnOff();
            }
        }

        private void OnEnable()
        {
            if(null != coroutine)
            {
                StopCoroutine(coroutine);
            }

            if(timeScheduledOff > 0f)
            {
                coroutine = StartCoroutine(scheduledOff());
            }
        }
    }
}

