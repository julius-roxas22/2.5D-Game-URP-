using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IndieGamePractice
{
    public class GameEvent : MonoBehaviour
    {
        private List<GameEventListener> Listeners = new List<GameEventListener>();
        private GameObject gameEventObject;

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
            gameEventObject = obj;
            foreach (GameEventListener listener in Listeners)
            {
                listener._OnRaiseEvent();
            }
        }

        public GameObject _GetEventObject
        {
            get
            {
                return gameEventObject;
            }
        }

        public void _AddListeners(GameEventListener listener)
        {
            if (!Listeners.Contains(listener))
            {
                Listeners.Add(listener);
            }
        }
    }
}


