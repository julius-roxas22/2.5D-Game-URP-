using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IndieGamePractice
{
    public class AttackInfo : MonoBehaviour
    {
        public CharacterControl _Attacker;
        public Attack _AttackAbility;
        public List<_AttackPartType> _AttackPartTypes = new List<_AttackPartType>();
        //public List<string> _ColliderNames = new List<string>();

        public DeathType _DeathType;
        public bool _MustCollide;
        public bool _MustFaceAttacker;
        public bool _IsRegistered;
        public bool _IsFinished;

        public float _AttackRange;
        public int _MaxHits;
        public int _CurrentHits;

        public void _ResetAttackInfo(Attack attackAbility, CharacterControl control)
        {
            _IsRegistered = false;
            _IsFinished = false;

            _Attacker = control;
            _AttackAbility = attackAbility;
        }

        public void _RegisterAttackInfo(Attack attackAbility)
        {
            _IsRegistered = true;

            _AttackAbility = attackAbility;
            //_ColliderNames = attackAbility._ColliderNames;
            _AttackPartTypes = attackAbility._AttackPartTypes;
            _DeathType = attackAbility._DeathType;
            _MustCollide = attackAbility._MustCollide;
            _MustFaceAttacker = attackAbility._MustFaceAttacker;
            _AttackRange = attackAbility._AttackRange;
            _MaxHits = attackAbility._MaxHits;
            _CurrentHits = 0;
        }

        private void OnDisable()
        {
            _IsFinished = true;
        }
    }
}