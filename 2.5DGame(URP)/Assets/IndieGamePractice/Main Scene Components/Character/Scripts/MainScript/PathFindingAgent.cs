using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace IndieGamePractice
{
    public class PathFindingAgent : MonoBehaviour
    {
        public GameObject _Target;
        public bool _PlayableCharacter;
        private NavMeshAgent agent;
        private List<Coroutine> moveCoroutines = new List<Coroutine>();

        [HideInInspector] public bool _StartWalk;

        public GameObject _StartSphere;
        public GameObject _EndSphere;
        private void Awake()
        {
            agent = GetComponent<NavMeshAgent>();
        }

        public void _GotoTarget()
        {
            agent.enabled = true;

            _StartSphere.transform.parent = null;
            _EndSphere.transform.parent = null;

            _StartWalk = false;

            agent.isStopped = false;

            if (_PlayableCharacter)
            {
                _Target = CharacterManager._GetInstance._GetPlayableCharacters().gameObject;
            }

            agent.SetDestination(_Target.transform.position);

            if (moveCoroutines.Count != 0)
            {
                StopCoroutine(moveCoroutines[0]);
                moveCoroutines.RemoveAt(0);
            }

            moveCoroutines.Add(StartCoroutine(move()));
        }

        private IEnumerator move()
        {
            while (true)
            {
                if (agent.isOnOffMeshLink)
                {
                    _StartSphere.transform.position = agent.currentOffMeshLinkData.startPos;
                    _EndSphere.transform.position = agent.currentOffMeshLinkData.endPos;
                    agent.CompleteOffMeshLink();
                    agent.isStopped = true;
                    _StartWalk = true;
                    yield break;
                }

                float dist = (transform.position - agent.destination).sqrMagnitude;

                if (dist < 0.5f)
                {
                    _StartSphere.transform.position = agent.destination;
                    _EndSphere.transform.position = agent.destination;
                    agent.isStopped = true;
                    _StartWalk = true;
                    yield break;
                }

                yield return new WaitForEndOfFrame();
            }
        }
    }
}

