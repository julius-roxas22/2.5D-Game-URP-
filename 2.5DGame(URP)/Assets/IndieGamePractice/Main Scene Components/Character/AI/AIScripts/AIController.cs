using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IndieGamePractice
{
    public enum AI_Type
    {
        WALK_AND_JUMP,
        RUN
    }

    public class AIController : MonoBehaviour
    {
        public List<AISubset> _AISubsets = new List<AISubset>();
        public AI_Type _InitialAIType;

        private void Awake()
        {
            AISubset[] arr = GetComponentsInChildren<AISubset>();

            foreach (AISubset s in arr)
            {
                if (!_AISubsets.Contains(s))
                {
                    s.gameObject.SetActive(false);
                    _AISubsets.Add(s);
                }
            }
        }

        private IEnumerator Start()
        {
            yield return new WaitForEndOfFrame();
            TriggerAI(_InitialAIType);
        }

        public void TriggerAI(AI_Type aiType)
        {
            AISubset aiSubset = null;

            foreach (AISubset s in _AISubsets)
            {
                s.gameObject.SetActive(false);
                if (s._AIType == aiType)
                {
                    aiSubset = s;
                }
            }

            if (null != aiSubset)
            {
                aiSubset.gameObject.SetActive(true);
            }
        }
    }
}