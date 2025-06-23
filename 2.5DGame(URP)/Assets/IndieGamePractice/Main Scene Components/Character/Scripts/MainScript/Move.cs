using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IndieGamePractice
{
    [CreateAssetMenu(fileName = "New Ability Data", menuName = "IndieGamePractice/Create/Ability/Move")]
    public class Move : StateData
    {
        [SerializeField] private float movementSpeed;
        [SerializeField] private float blockDistance;
        [SerializeField] private AnimationCurve speedGraph;
        [SerializeField] private bool constantMoved;
        [SerializeField] private bool lockDirection;
        [SerializeField] private bool lockDirectionNextState;
        [SerializeField] private bool allowEarlyTurn;
        [SerializeField] private bool ignoreCharacterCollision;

        [Header("Momentum")]
        [SerializeField] private float startingMomentum;
        [SerializeField] private bool useMomentum;
        [SerializeField] private float maxMomentum;
        [SerializeField] private bool clearMomentum;

        public override void _OnEnterAbility(CharacterStateBase characterStateBase, Animator animator, AnimatorStateInfo animatorStateInfo)
        {
            CharacterControl control = characterStateBase._CharacterControl;

            if (allowEarlyTurn && !control._GetAnimationProgress._DisAllowEarlyTurn)
            {
                if (!control._GetAnimationProgress._LockDirectionNextState)
                {
                    if (control._MoveLeft)
                    {
                        control._FaceForward(false);
                    }

                    if (control._MoveRight)
                    {
                        control._FaceForward(true);
                    }
                }
                else
                {
                    control._GetAnimationProgress._LockDirectionNextState = false;
                }
            }

            control._GetAnimationProgress._DisAllowEarlyTurn = false;
            //control._GetAnimationProgress._AirMomentum = 0f;

            if (startingMomentum > 0.001f)
            {
                if (control._IsFacingForward())
                {
                    control._GetAnimationProgress._AirMomentum = startingMomentum;
                }
                else
                {
                    control._GetAnimationProgress._AirMomentum = -startingMomentum;
                }
            }
        }

        public override void _OnUpdateAbility(CharacterStateBase characterStateBase, Animator animator, AnimatorStateInfo animatorStateInfo)
        {
            CharacterControl control = characterStateBase._CharacterControl;

            control._GetAnimationProgress._LockDirectionNextState = lockDirectionNextState;

            if(control._GetAnimationProgress._IsRunningAbilities(typeof(Move) , this))
            {
                return;
            }

            if (useMomentum)
            {
                momentum(control, animatorStateInfo);
            }
            else
            {
                if (constantMoved)
                {
                    constantMove(control, animator, animatorStateInfo);
                }
                else
                {
                    controlledMove(control, animator, animatorStateInfo);
                }
            }
        }

        public override void _OnExitAbility(CharacterStateBase characterStateBase, Animator animator, AnimatorStateInfo animatorStateInfo)
        {
            CharacterControl control = characterStateBase._CharacterControl;
            if (clearMomentum)
            {
                control._GetAnimationProgress._AirMomentum = 0f;
            }
        }

        private void momentum(CharacterControl control, AnimatorStateInfo stateInfo)
        {
            if (control._MoveRight)
            {
                control._GetAnimationProgress._AirMomentum += speedGraph.Evaluate(stateInfo.normalizedTime) * movementSpeed * Time.deltaTime;
            }

            if (control._MoveLeft)
            {
                control._GetAnimationProgress._AirMomentum -= speedGraph.Evaluate(stateInfo.normalizedTime) * movementSpeed * Time.deltaTime;
            }

            if (Mathf.Abs(control._GetAnimationProgress._AirMomentum) >= maxMomentum)
            {
                if (control._GetAnimationProgress._AirMomentum > 0f)
                {
                    control._GetAnimationProgress._AirMomentum = maxMomentum;
                }
                else if (control._GetAnimationProgress._AirMomentum < 0f)
                {
                    control._GetAnimationProgress._AirMomentum = -maxMomentum;
                }
            }

            if (control._GetAnimationProgress._AirMomentum > 0f)
            {
                control._FaceForward(true);
            }
            else if (control._GetAnimationProgress._AirMomentum < 0f)
            {
                control._FaceForward(false);
            }

            if (!checkFront(control))
            {
                control._CharacterMove(movementSpeed, Mathf.Abs(control._GetAnimationProgress._AirMomentum));
            }
        }

        private void constantMove(CharacterControl control, Animator animator, AnimatorStateInfo stateInfo)
        {
            if (!checkFront(control))
            {
                control._CharacterMove(movementSpeed, speedGraph.Evaluate(stateInfo.normalizedTime));
            }

            if (!control._MoveRight && !control._MoveLeft)
            {
                animator.SetBool(_TransitionParameters.Move.ToString(), false);
            }
            else
            {
                animator.SetBool(_TransitionParameters.Move.ToString(), true);
            }
        }

        private void controlledMove(CharacterControl control, Animator animator, AnimatorStateInfo stateInfo)
        {
            if (control._Jump)
            {
                animator.SetBool(_TransitionParameters.Jump.ToString(), true);
            }

            if (control._Turbo)
            {
                animator.SetBool(_TransitionParameters.Turbo.ToString(), true);
            }
            else
            {
                animator.SetBool(_TransitionParameters.Turbo.ToString(), false);
            }

            if (control._MoveRight && control._MoveLeft)
            {
                animator.SetBool(_TransitionParameters.Move.ToString(), false);
                return;
            }

            if (!control._MoveRight && !control._MoveLeft)
            {
                animator.SetBool(_TransitionParameters.Move.ToString(), false);
                return;
            }

            if (control._MoveRight)
            {
                if (!checkFront(control))
                {
                    control._CharacterMove(movementSpeed, speedGraph.Evaluate(stateInfo.normalizedTime));
                }
            }

            if (control._MoveLeft)
            {
                if (!checkFront(control))
                {
                    control._CharacterMove(movementSpeed, speedGraph.Evaluate(stateInfo.normalizedTime));
                }
                control.transform.rotation = Quaternion.Euler(0f, 180, 0f);
            }

            CheckTurn(control);
        }

        private void CheckTurn(CharacterControl control)
        {
            if (!lockDirection)
            {
                if (control._MoveLeft)
                {
                    control.transform.rotation = Quaternion.Euler(0f, 180, 0f);
                }

                if (control._MoveRight)
                {
                    control.transform.rotation = Quaternion.Euler(0f, 0, 0f);
                }
            }
        }

        private bool ignoreCharacterBoxCollider(Collider col)
        {
            if (!ignoreCharacterCollision)
            {
                return false;
            }

            if (col.gameObject.GetComponent<CharacterControl>() != null)
            {
                return true;
            }

            return false;
        }
        private bool checkFront(CharacterControl control)
        {
            foreach (GameObject obj in control._FrontSpheres)
            {
                Debug.DrawRay(obj.transform.position, control.transform.forward * blockDistance, Color.red);
                if (Physics.Raycast(obj.transform.position, control.transform.forward, out RaycastHit hit, blockDistance))
                {
                    if (!control._RagdollParts.Contains(hit.collider))
                    {
                        if (!isBodyPart(hit.collider)
                            && !Ledge._IsLedge(hit.collider.gameObject)
                            && !LedgeChecker._IsLedgeChecker(hit.collider.gameObject)
                            && !ignoreCharacterBoxCollider(hit.collider))
                        {
                            return true;
                        }
                    }
                }
            }

            return false;
        }

        private bool isBodyPart(Collider col)
        {
            CharacterControl control = col.transform.root.GetComponent<CharacterControl>();

            if (null == control)
            {
                return false;
            }

            if (control.gameObject == col.gameObject)
            {
                return false;
            }

            if (control._RagdollParts.Contains(col))
            {
                return true;
            }

            return false;
        }
    }
}


