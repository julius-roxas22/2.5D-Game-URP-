using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IndieGamePractice
{
    public class GameEventListener : MonoBehaviour
    {
        public GameEvent _GameEvent;
        [Space(10)]
        [SerializeField] private UnityEngine.Events.UnityEvent _Response;

        private void Start()
        {
            if (null != _GameEvent)
            {
                _GameEvent._AddListeners(this);
            }
        }

        public void _OnRaiseEvent()
        {
            _Response.Invoke();
        }
    }
}