using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IndieGamePractice
{
    public enum _AttackPartType
    {
        LeftHand,
        RightHand,
        LeftFoot,
        RightFoot
    }

    [CreateAssetMenu(fileName = "New Ability Data", menuName = "IndieGamePractice/Create/Ability/Attack")]
    public class Attack : StateData
    {
        [Header("Set up")]
        public float _StartAttackTime;
        public float _EndAttackTime;
        //public List<string> _ColliderNames = new List<string>();
        public List<_AttackPartType> _AttackPartTypes = new List<_AttackPartType>();
        public DeathType _DeathType;
        public bool _MustCollide;
        public bool _MustFaceAttacker;
        public float _AttackRange;
        public int _MaxHits;
        private List<AttackInfo> _FinishedAttack = new List<AttackInfo>();

        [Space(10)]
        [Header("Debugging Control")]
        [SerializeField] private bool onDebug;

        [Space(10)]
        [Header("Combo")]
        [SerializeField] private float comboStartTime;
        [SerializeField] private float comboEndTime;

        public override void _OnEnterAbility(CharacterStateBase characterStateBase, Animator animator, AnimatorStateInfo animatorStateInfo)
        {
            animator.SetBool(_TransitionParameters.Attack.ToString(), false);

            GameObject obj = PoolManager._GetInstance._InstantiateObject(PoolObjectType.AttackInfo);
            AttackInfo info = obj.GetComponent<AttackInfo>();

            obj.SetActive(true);
            info._ResetAttackInfo(this, characterStateBase._CharacterControl);

            if (!AttackManager._GetInstance._CurrentAttacks.Contains(info))
            {
                AttackManager._GetInstance._CurrentAttacks.Add(info);
            }
        }

        public override void _OnUpdateAbility(CharacterStateBase characterStateBase, Animator animator, AnimatorStateInfo animatorStateInfo)
        {
            CharacterControl control = characterStateBase._CharacterControl;
            registerAttack(animatorStateInfo);
            deRegisterAttack(animatorStateInfo);
            checkCombo(control, animator, animatorStateInfo);
        }

        public override void _OnExitAbility(CharacterStateBase characterStateBase, Animator animator, AnimatorStateInfo animatorStateInfo)
        {
            animator.SetBool(_TransitionParameters.Attack.ToString(), false);
            clearAttacks();
        }

        private void checkCombo(CharacterControl control, Animator animator, AnimatorStateInfo stateInfo)
        {
            if (stateInfo.normalizedTime >= comboStartTime /*_StartAttackTime + ((_EndAttackTime - _StartAttackTime) / 3)*/)
            {
                if (stateInfo.normalizedTime <= comboEndTime /* _EndAttackTime + ((_EndAttackTime - _StartAttackTime) / 3)*/)
                {
                    if (control._GetAnimationProgress._AttackTriggered)
                    {
                        animator.SetBool(_TransitionParameters.Attack.ToString(), true);
                    }
                }
            }
        }

        private void registerAttack(AnimatorStateInfo animatorStateInfo)
        {
            if (_StartAttackTime <= animatorStateInfo.normalizedTime && _EndAttackTime > animatorStateInfo.normalizedTime)
            {
                foreach (AttackInfo info in AttackManager._GetInstance._CurrentAttacks)
                {
                    if (null == info)
                    {
                        continue;
                    }

                    if (this == info._AttackAbility && !info._IsRegistered)
                    {
                        info._RegisterAttackInfo(this);

                        if (onDebug)
                        {
                            Debug.Log("Register in " + animatorStateInfo.normalizedTime);
                        }
                    }

                }
            }
        }

        private void deRegisterAttack(AnimatorStateInfo animatorStateInfo)
        {
            if (animatorStateInfo.normalizedTime >= _EndAttackTime)
            {
                foreach (AttackInfo info in AttackManager._GetInstance._CurrentAttacks)
                {
                    if (null == info)
                    {
                        continue;
                    }

                    if (this == info._AttackAbility && !info._IsFinished)
                    {
                        info._IsFinished = true;
                        info.GetComponent<PoolObject>()._TurnOff();

                        if (onDebug)
                        {
                            Debug.Log("de - Register in " + animatorStateInfo.normalizedTime);
                        }
                    }
                }
            }
        }

        private void clearAttacks()
        {
            _FinishedAttack.Clear();

            foreach (AttackInfo info in AttackManager._GetInstance._CurrentAttacks)
            {
                if (null == info || this == info._AttackAbility)
                {
                    _FinishedAttack.Add(info);
                }
            }

            foreach (AttackInfo info in _FinishedAttack)
            {
                if (AttackManager._GetInstance._CurrentAttacks.Contains(info))
                {
                    AttackManager._GetInstance._CurrentAttacks.Remove(info);
                }
            }
        }
    }
}


