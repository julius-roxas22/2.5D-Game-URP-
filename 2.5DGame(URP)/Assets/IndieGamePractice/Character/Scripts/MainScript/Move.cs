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

        public override void _OnEnterAbility(CharacterStateBase characterStateBase, Animator animator, AnimatorStateInfo animatorStateInfo)
        {

        }

        public override void _OnUpdateAbility(CharacterStateBase characterStateBase, Animator animator, AnimatorStateInfo animatorStateInfo)
        {
            CharacterControl control = characterStateBase._GetCharacterControl(animator);

            if (constantMoved)
            {
                constantMove(control, animator, animatorStateInfo);
            }
            else
            {
                controlledMove(control, animator, animatorStateInfo);
            }
        }

        public override void _OnExitAbility(CharacterStateBase characterStateBase, Animator animator, AnimatorStateInfo animatorStateInfo)
        {

        }

        private void constantMove(CharacterControl control, Animator animator, AnimatorStateInfo stateInfo)
        {
            if (!checkFront(control))
            {
                control._CharacterMove(movementSpeed, speedGraph.Evaluate(stateInfo.normalizedTime));
            }
        }

        private void controlledMove(CharacterControl control, Animator animator, AnimatorStateInfo stateInfo)
        {
            if (control._Jump)
            {
                animator.SetBool(TransitionParameters.Jump.ToString(), true);
            }

            if (control._MoveRight && control._MoveLeft)
            {
                animator.SetBool(TransitionParameters.Move.ToString(), false);
                return;
            }

            if (!control._MoveRight && !control._MoveLeft)
            {
                animator.SetBool(TransitionParameters.Move.ToString(), false);
                return;
            }

            if (control._MoveRight)
            {
                if (!checkFront(control))
                {
                    control._CharacterMove(movementSpeed, speedGraph.Evaluate(stateInfo.normalizedTime));
                }
                control.transform.rotation = Quaternion.Euler(0f, 0, 0f);
            }

            if (control._MoveLeft)
            {
                if (!checkFront(control))
                {
                    control._CharacterMove(movementSpeed, speedGraph.Evaluate(stateInfo.normalizedTime));
                }
                control.transform.rotation = Quaternion.Euler(0f, 180, 0f);
            }
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
                        if (!isBodyPart(hit.collider))
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


