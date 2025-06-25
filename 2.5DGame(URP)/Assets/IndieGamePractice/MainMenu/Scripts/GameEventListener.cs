using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IndieGamePractice
{
    public class GameEventListener : MonoBehaviour
    {
        public GameEvent _GameEvent;
        [Space(10)]
        public UnityEngine.Events.UnityEvent _Response;

        private void Start()
        {
            if (null != _GameEvent)
            {
                if (!_GameEvent.Listeners.Contains(this))
                {
                    _GameEvent.Listeners.Add(this);
                }
            }
        }

        public void _OnRaiseEvent()
        {
            _Response.Invoke();
        }
    }
}