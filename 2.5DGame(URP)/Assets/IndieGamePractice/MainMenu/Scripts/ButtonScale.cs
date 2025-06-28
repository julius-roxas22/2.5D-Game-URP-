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

        private void Awake()
        {
            listener = GetComponent<GameEventListener>();
        }

        public void _ScaleUpButton()
        {
            listener._GameEvent._GameObjectEvent.GetComponent<Animator>().SetBool(_UIParameters.ScaleUp.ToString(), true);
        }

        public void _ScaleDefaultButton()
        {
            listener._GameEvent._GameObjectEvent.GetComponent<Animator>().SetBool(_UIParameters.ScaleUp.ToString(), false);
        }
    }
}