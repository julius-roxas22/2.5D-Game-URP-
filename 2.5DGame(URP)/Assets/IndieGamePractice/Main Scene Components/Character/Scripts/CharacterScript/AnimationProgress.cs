using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IndieGamePractice
{
    public class AnimationProgress : MonoBehaviour
    {
        [HideInInspector] public List<StateData> _CurrentRunningAbilities = new List<StateData>();

        private CharacterControl control;
        [SerializeField] private float pressTime;
        [SerializeField] private float maxPressTime;

        [HideInInspector] public List<PoolObjectType> _PoolObjectTypeList = new List<PoolObjectType>();
        [HideInInspector] public bool _IsJumped;
        [HideInInspector] public bool _IsCameraShaken;
        [HideInInspector] public bool _AttackTriggered;
        [HideInInspector] public bool _DisAllowEarlyTurn;
        [HideInInspector] public bool _RagdollTriggered;
        [HideInInspector] public bool _UpdatingSpheres;

        [HideInInspector] public float _AirMomentum;
        //[HideInInspector] public bool _FrameUpdated;

        [HideInInspector] public bool _UpdatingBoxCollider;
        [HideInInspector] public float _SizeSpeed;
        [HideInInspector] public Vector3 _TargetSize;
        [HideInInspector] public float _CenterSpeed;
        [HideInInspector] public Vector3 _TargetCenter;
        [HideInInspector] public bool _CancelPullGravity;
        [HideInInspector] public bool _LockDirectionNextState;

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

        public bool _IsRunningAbilities(System.Type type, StateData data)
        {
            for (int i = 0; i < _CurrentRunningAbilities.Count; i++)
            {
                if (type == _CurrentRunningAbilities[i].GetType())
                {
                    if (_CurrentRunningAbilities[i] == data)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}


