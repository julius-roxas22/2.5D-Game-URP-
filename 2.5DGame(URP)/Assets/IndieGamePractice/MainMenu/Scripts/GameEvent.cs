using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IndieGamePractice
{
    public class GameEvent : MonoBehaviour
    {
        public List<GameEventListener> Listeners = new List<GameEventListener>();

        private void Awake()
        {
            Listeners.Clear();
        }

        public void _Raised()
        {
            foreach(GameEventListener listner in Listeners)
            {
                listner._OnRaiseEvent();
            }
        }
    }
}


