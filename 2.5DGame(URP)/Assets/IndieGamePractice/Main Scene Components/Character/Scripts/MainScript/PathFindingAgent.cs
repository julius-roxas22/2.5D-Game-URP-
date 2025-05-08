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


        private void Awake()
        {
            agent = GetComponent<NavMeshAgent>();
        }

        public void _GotoTarget()
        {
            if (_PlayableCharacter)
            {
                _Target = CharacterManager._GetInstance._GetPlayableCharacters().gameObject;
            }
            agent.SetDestination(_Target.transform.position);
        }
    }
}

