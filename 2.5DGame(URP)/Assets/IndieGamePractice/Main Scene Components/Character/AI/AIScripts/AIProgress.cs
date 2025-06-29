using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IndieGamePractice
{
    public class AIProgress : MonoBehaviour
    {
        [HideInInspector] public PathFindingAgent agent;

        private CharacterControl control;

        private void Awake()
        {
            control = GetComponentInParent<CharacterControl>();
        }

        public float _GetDistanceToDistanation()
        {
            return (agent._StartSphere.transform.position - control.transform.position).sqrMagnitude;
        }
    }
}

