using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IndieGamePractice
{
    [CreateAssetMenu(fileName = "New Ability Data", menuName = "IndieGamePractice/Create/Ability/Attack")]
    public class Attack : StateData
    {
        public float _StartAttackTime;
        public float _EndAttackTime;
        public List<string> _ColliderNames = new List<string>();
        public bool _LaunchIntoAir;
        public bool _MustCollide;
        public bool _MustFaceAttacker;
        public float _AttackRange;
        public int _MaxHits;
        private List<AttackInfo> _FinishedAttack = new List<AttackInfo>();
        [SerializeField] private bool onDebug;

        public override void _OnEnterAbility(CharacterStateBase characterStateBase, Animator animator, AnimatorStateInfo animatorStateInfo)
        {
            animator.SetBool(TransitionParameters.Attack.ToString(), false);

            GameObject obj = PoolManager._GetInstance._InstantiateObject(PoolObjectType.AttackInfo);
            AttackInfo info = obj.GetComponent<AttackInfo>();

            obj.SetActive(true);
            info._ResetAttackInfo(this, characterStateBase._GetCharacterControl(animator));

            if (!AttackManager._GetInstance._CurrentAttacks.Contains(info))
            {
                AttackManager._GetInstance._CurrentAttacks.Add(info);
            }
        }

        public override void _OnUpdateAbility(CharacterStateBase characterStateBase, Animator animator, AnimatorStateInfo animatorStateInfo)
        {
            registerAttack(characterStateBase._GetCharacterControl(animator), animatorStateInfo);
            deRegisterAttack(animatorStateInfo);
            checkCombo(characterStateBase._GetCharacterControl(animator), animator, animatorStateInfo);
        }

        public override void _OnExitAbility(CharacterStateBase characterStateBase, Animator animator, AnimatorStateInfo animatorStateInfo)
        {
            animator.SetBool(TransitionParameters.Attack.ToString(), false);
            clearAttacks();
        }

        private void checkCombo(CharacterControl control, Animator animator, AnimatorStateInfo stateInfo)
        {
            if (stateInfo.normalizedTime >= _StartAttackTime + ((_EndAttackTime - _StartAttackTime) / 3))
            {
                if (stateInfo.normalizedTime < _EndAttackTime + ((_EndAttackTime - _StartAttackTime) / 3))
                {
                    if (control._Attack)
                    {
                        animator.SetBool(TransitionParameters.Attack.ToString(), true);
                    }
                }
            }
        }

        private void registerAttack(CharacterControl control, AnimatorStateInfo animatorStateInfo)
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


