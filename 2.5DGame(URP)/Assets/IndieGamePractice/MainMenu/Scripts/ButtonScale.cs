using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IndieGamePractice
{
    public enum _UIParameters
    {
        ScaleUp
    }
    public class ButtonScale : MonoBehaviour
    {
        private GameEventListener listener;
        private Dictionary<GameObject, Animator> dicAnimator = new Dictionary<GameObject, Animator>();

        private void Awake()
        {
            listener = GetComponent<GameEventListener>();
        }

        public void _ScaleUpButton()
        {
            _GetAnimator(listener._GameEvent._GetEventObject).SetBool(_UIParameters.ScaleUp.ToString(), true);
        }

        public void _ScaleDefaultButton()
        {
            _GetAnimator(listener._GameEvent._GetEventObject).SetBool(_UIParameters.ScaleUp.ToString(), false);
        }

        private Animator _GetAnimator(GameObject gameEventObject)
        {
            if (!dicAnimator.ContainsKey(gameEventObject))
            {
                Animator anim = gameEventObject.GetComponent<Animator>();
                dicAnimator.Add(gameEventObject, anim);
                return anim;
            }
            else
            {
                return dicAnimator[gameEventObject];
            }
        }
    }
}