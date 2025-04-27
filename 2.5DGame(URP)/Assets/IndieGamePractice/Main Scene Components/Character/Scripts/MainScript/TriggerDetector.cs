using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IndieGamePractice
{
    public enum BodyPart
    {
        Upper,
        Lower,
        Arm,
        Leg
    }

    public class TriggerDetector : MonoBehaviour
    {
        public List<Collider> _CollidingParts = new List<Collider>();
        public BodyPart bodyPart;
        private CharacterControl owner;

        private void Awake()
        {
            owner = GetComponentInParent<CharacterControl>();
        }

        private void OnTriggerEnter(Collider col)
        {
            if (owner._RagdollParts.Contains(col))
            {
                return;
            }

            CharacterControl attacker = col.transform.root.GetComponent<CharacterControl>();

            if (null == attacker)
            {
                return;
            }

            if (col.gameObject == attacker.gameObject)
            {
                return;
            }

            if (!_CollidingParts.Contains(col))
            {
                _CollidingParts.Add(col);
            }
        }

        private void OnTriggerExit(Collider attacker)
        {
            if (_CollidingParts.Contains(attacker))
            {
                _CollidingParts.Remove(attacker);
            }
        }
    }
}