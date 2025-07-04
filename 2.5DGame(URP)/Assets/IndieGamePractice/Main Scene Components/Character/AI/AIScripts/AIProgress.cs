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

        public float _GetDistanceToStartSphere()
        {
            return (agent._StartSphere.transform.position - control.transform.position).sqrMagnitude;
        }

        public float _GetDistanceToEndSphere()
        {
            return (agent._EndSphere.transform.position - control.transform.position).sqrMagnitude;
        }

        public bool _EndSphereIsHigher()
        {

            if (endSphereStraight())
            {
                return false;
            }

            if ((agent._EndSphere.transform.position.y - agent._StartSphere.transform.position.y) > 0f)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool _EndSphereIsLower()
        {
            if (endSphereStraight())
            {
                return false;
            }

            if ((agent._EndSphere.transform.position.y - agent._StartSphere.transform.position.y) < 0f)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool endSphereStraight()
        {
            if (Mathf.Abs(agent._EndSphere.transform.position.y - agent._StartSphere.transform.position.y) > 0.01f)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}

