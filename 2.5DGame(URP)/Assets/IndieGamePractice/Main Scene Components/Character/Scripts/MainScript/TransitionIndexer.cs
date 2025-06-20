using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IndieGamePractice
{
    public enum _TransitionConditionType
    {
        UP,
        DOWN,
        LEFT,
        RIGHT,
        ATTACK,
        JUMP,
        GRABBING_LEDGE,
        LEFT_OR_RIGHT
    }

    [CreateAssetMenu(fileName = "New Ability Data", menuName = "IndieGamePractice/Create/Ability/TransitionIndexer")]
    public class TransitionIndexer : StateData
    {

        [SerializeField] private List<_TransitionConditionType> conditionTypes = new List<_TransitionConditionType>();
        [SerializeField] private int index;

        public override void _OnEnterAbility(CharacterStateBase characterStateBase, Animator animator, AnimatorStateInfo animatorStateInfo)
        {
            CharacterControl control = characterStateBase._CharacterControl;
            if (makeTransition(control))
            {
                animator.SetInteger(_TransitionParameters.TransitionIndex.ToString(), index);
            }
            else
            {
                animator.SetInteger(_TransitionParameters.TransitionIndex.ToString(), 0);
            }
        }

        public override void _OnUpdateAbility(CharacterStateBase characterStateBase, Animator animator, AnimatorStateInfo animatorStateInfo)
        {
            CharacterControl control = characterStateBase._CharacterControl;

            if (animator.GetInteger(_TransitionParameters.TransitionIndex.ToString()) == 0)
            {
                if (makeTransition(control))
                {
                    animator.SetInteger(_TransitionParameters.TransitionIndex.ToString(), index);
                }
            }
        }

        public override void _OnExitAbility(CharacterStateBase characterStateBase, Animator animator, AnimatorStateInfo animatorStateInfo)
        {
            animator.SetInteger(_TransitionParameters.TransitionIndex.ToString(), 0);
        }

        private bool makeTransition(CharacterControl control)
        {
            foreach (_TransitionConditionType type in conditionTypes)
            {
                switch (type)
                {
                    case _TransitionConditionType.UP:
                        {
                            if (!control._MoveUp)
                            {
                                return false;
                            }
                            break;
                        }
                    case _TransitionConditionType.DOWN:
                        {
                            if (!control._MoveDown)
                            {
                                return false;
                            }
                            break;
                        }
                    case _TransitionConditionType.LEFT:
                        {
                            if (!control._MoveLeft)
                            {
                                return false;
                            }
                            break;
                        }
                    case _TransitionConditionType.RIGHT:
                        {
                            if (!control._MoveRight)
                            {
                                return false;
                            }
                            break;
                        }
                    case _TransitionConditionType.ATTACK:
                        {
                            if (!control._GetAnimationProgress._AttackTriggered)
                            {
                                return false;
                            }
                            break;
                        }
                    case _TransitionConditionType.JUMP:
                        {
                            if (!control._Jump)
                            {
                                return false;
                            }
                            break;
                        }
                    case _TransitionConditionType.GRABBING_LEDGE:
                        {
                            if (!control._GetLedgeChecker._IsGrabbingLedge)
                            {
                                return false;
                            }
                            break;
                        }
                    case _TransitionConditionType.LEFT_OR_RIGHT:
                        {
                            if (!control._MoveLeft && !control._MoveRight)
                            {
                                return false;
                            }
                            break;
                        }
                }
            }
            return true;
        }
    }
}


