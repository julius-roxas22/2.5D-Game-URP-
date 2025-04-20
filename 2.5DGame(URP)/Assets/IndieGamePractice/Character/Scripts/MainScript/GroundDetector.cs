using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IndieGamePractice
{
    [CreateAssetMenu(fileName = "New Ability Data", menuName = "IndieGamePractice/Create/Ability/GroundDetector")]
    public class GroundDetector : StateData
    {
        [SerializeField] private float distance;
        [SerializeField] private float checkTime;

        public override void _OnEnterAbility(CharacterStateBase characterStateBase, Animator animator, AnimatorStateInfo animatorStateInfo)
        {

        }

        public override void _OnUpdateAbility(CharacterStateBase characterStateBase, Animator animator, AnimatorStateInfo animatorStateInfo)
        {
            CharacterControl control = characterStateBase._GetCharacterControl(animator);

            if (animatorStateInfo.normalizedTime >= checkTime)
            {
                if (isGrounded(control))
                {
                    animator.SetBool(TransitionParameters.Grounded.ToString(), true);
                }
                else
                {
                    animator.SetBool(TransitionParameters.Grounded.ToString(), false);
                }
            }
        }

        public override void _OnExitAbility(CharacterStateBase characterStateBase, Animator animator, AnimatorStateInfo animatorStateInfo)
        {

        }

        private bool isGrounded(CharacterControl control)
        {
            if (control._GetRigidBody.velocity.y >= -0.001f && control._GetRigidBody.velocity.y <= 0f)
            {
                return true;
            }

            if (control._GetRigidBody.velocity.y < 0f)
            {
                foreach (GameObject obj in control._BottomSpheres)
                {
                    Debug.DrawRay(obj.transform.position, -Vector3.up * distance, Color.red);
                    if (Physics.Raycast(obj.transform.position, -Vector3.up, out RaycastHit hit, distance))
                    {
                        if (!control._RagdollParts.Contains(hit.collider))
                        {
                            return true;
                        }
                    }
                }
            }

            return false;
        }

    }
}


