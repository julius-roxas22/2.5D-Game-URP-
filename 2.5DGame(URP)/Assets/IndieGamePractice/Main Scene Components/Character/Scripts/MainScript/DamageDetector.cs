using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IndieGamePractice
{
    public class DamageDetector : MonoBehaviour
    {
        private CharacterControl control;
        private BodyPart damagedBodyPart;
        [HideInInspector] public int _DamageTaken;

        private void Awake()
        {
            _DamageTaken = 0;
            control = GetComponent<CharacterControl>();
        }

        void Update()
        {
            if (AttackManager._GetInstance._CurrentAttacks.Count > 0)
            {
                checkAttack();
            }
        }

        private void checkAttack()
        {
            foreach (AttackInfo info in AttackManager._GetInstance._CurrentAttacks)
            {
                if (null == info)
                {
                    continue;
                }

                if (!info._IsRegistered)
                {
                    continue;
                }

                if (info._IsFinished)
                {
                    continue;
                }

                if (info._CurrentHits >= info._MaxHits)
                {
                    continue;
                }

                if (info._Attacker == control)
                {
                    continue;
                }

                if (info._MustFaceAttacker)
                {
                    Vector3 vec = control.transform.position - info._Attacker.transform.position;
                    if (vec.z * info._Attacker.transform.forward.z < 0f)
                    {
                        continue;
                    }
                }

                if (info._MustCollide)
                {
                    if (isCollided(info))
                    {
                        takeDamage(info);
                    }
                }
                else
                {
                    float dist = (control.transform.position - info._Attacker.transform.position).sqrMagnitude;
                    if (dist <= info._AttackRange)
                    {
                        takeDamage(info);
                    }
                }
            }
        }

        private bool isCollided(AttackInfo info)
        {
            foreach (TriggerDetector trigger in control._GetAllTriggers())
            {
                foreach (Collider col in trigger._CollidingParts)
                {
                    foreach (_AttackPartType attackPart in info._AttackPartTypes)
                    {
                        if (attackPart == _AttackPartType.LeftHand)
                        {
                            if (info._Attacker._LeftHand == col.gameObject)
                            {
                                damagedBodyPart = trigger.bodyPart;
                                return true;
                            }
                        }
                        else if (attackPart == _AttackPartType.RightHand)
                        {
                            if (info._Attacker._RightHand == col.gameObject)
                            {
                                damagedBodyPart = trigger.bodyPart;
                                return true;
                            }
                        }
                    }
                }
            }
            return false;
        }

        private void takeDamage(AttackInfo info)
        {
            if (_DamageTaken > 0)
            {
                return;
            }

            if (info._MustCollide)
            {
                CameraManager._GetInstance._ShakeCamera(0.45f);
            }

            control._SkinnedMesh.runtimeAnimatorController = DeathAnimationManager._GetInstance._GetDeathController(damagedBodyPart, info);
            info._CurrentHits++;
            control._GetRigidBody.useGravity = false;
            control.GetComponent<BoxCollider>().enabled = false;
            control._GetLedgeChecker.GetComponent<BoxCollider>().enabled = false;

            _DamageTaken++;
        }
    }
}


