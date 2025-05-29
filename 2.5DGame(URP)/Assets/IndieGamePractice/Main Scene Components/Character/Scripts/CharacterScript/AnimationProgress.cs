using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IndieGamePractice
{
    public class AnimationProgress : MonoBehaviour
    {
        private CharacterControl control;
        [SerializeField] private float pressTime;
        [SerializeField] private float maxPressTime;

        [HideInInspector] public List<PoolObjectType> _PoolObjectTypeList = new List<PoolObjectType>();
        [HideInInspector] public bool _IsJumped;
        [HideInInspector] public bool _IsCameraShaken;
        [HideInInspector] public bool _AttackTriggered;
        [HideInInspector] public bool _DisAllowEarlyTurn;
        /*[HideInInspector]*/
        public float _AirMomentum;

        private void Awake()
        {
            control = GetComponent<CharacterControl>();
            pressTime = 0f;
        }

        private void Update()
        {
            if (control._Attack)
            {
                pressTime += Time.deltaTime;
            }
            else
            {
                pressTime = 0f;
            }

            if (pressTime == 0f)
            {
                _AttackTriggered = false;
            }
            else if (pressTime > maxPressTime)
            {
                _AttackTriggered = false;
            }
            else
            {
                _AttackTriggered = true;
            }
        }
    }
}


