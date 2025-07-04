using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IndieGamePractice
{
    public enum AI_Type
    {
        NONE,
        WALK_AND_JUMP
    }

    public class AIController : MonoBehaviour
    {
        public AI_Type _InitialAIType;
        private Vector3 targetDir;

        private Coroutine aiRoutine;
        private List<AISubset> _AISubsets = new List<AISubset>();
        private CharacterControl control;

        private void Awake()
        {
            control = GetComponentInParent<CharacterControl>();
        }

        private void Start()
        {
            _InitializeAI();
        }

        public void _InitializeAI()
        {
            if (_AISubsets.Count == 0)
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

            aiRoutine = StartCoroutine(_IEInitAi());
        }

        private void OnEnable()
        {
            if (null != aiRoutine)
            {
                StopCoroutine(aiRoutine);
            }
        }

        private IEnumerator _IEInitAi()
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

        public void _WalkStraightTowardsToStartsSPhere()
        {
            targetDir = control._GetAiProgress.agent._StartSphere.transform.position - control.transform.position;

            if (targetDir.z > 0)
            {
                control._MoveRight = true;
                control._MoveLeft = false;
            }
            else
            {
                control._MoveRight = false;
                control._MoveLeft = true;
            }
        }
    }
}