using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IndieGamePractice
{
    public class GameEvent : MonoBehaviour
    {
        [HideInInspector] public List<GameEventListener> Listeners = new List<GameEventListener>();
        [HideInInspector] public GameObject _GameObjectEvent;

        private void Awake()
        {
            Listeners.Clear();
        }

        public void _Raised()
        {
            foreach (GameEventListener listener in Listeners)
            {
                listener._OnRaiseEvent();
            }
        }

        public void _Raised(GameObject obj)
        {
            foreach (GameEventListener listener in Listeners)
            {
                _GameObjectEvent = obj;
                listener._OnRaiseEvent();
            }
        }
    }
}


