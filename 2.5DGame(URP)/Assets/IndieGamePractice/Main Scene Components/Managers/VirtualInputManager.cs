using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IndieGamePractice
{
    public enum InputKeyType
    {
        UP,
        DOWN,
        LEFT,
        RIGHT,

        ATTACK,
        JUMP,
        TURBO
    }

    public class VirtualInputManager : Singleton<VirtualInputManager>
    {
        public bool _MoveUp;
        public bool _MoveDown;
        public bool _MoveLeft;
        public bool _MoveRight;
        public bool _Attack;
        public bool _Jump;
        public bool _Turbo;

        [Header("Custom Key Binding")]
        [SerializeField] private bool useCustomKeys;

        [Space(5)]
        public bool _Bind_MoveUp;
        public bool _Bind_MoveDown;
        public bool _Bind_MoveLeft;
        public bool _Bind_MoveRight;
        public bool _Bind_Attack;
        public bool _Bind_Jump;
        public bool _Bind_Turbo;

        private PlayerInput playerInput;

        [Space(10)]
        public Dictionary<InputKeyType, KeyCode> _DictionaryKeys = new Dictionary<InputKeyType, KeyCode>();
        [SerializeField] private KeyCode[] possibleKeys;

        private void Awake()
        {
            possibleKeys = System.Enum.GetValues(typeof(KeyCode)) as KeyCode[];
            if (null == playerInput)
            {
                GameObject obj = Instantiate(Resources.Load("PlayerInput", typeof(GameObject))) as GameObject;
                playerInput = obj.GetComponent<PlayerInput>();
            }
        }

        private void Update()
        {
            if (!useCustomKeys)
            {
                return;
            }

            if (useCustomKeys)
            {
                if (_Bind_MoveUp)
                {
                    if (keyIsChanged(InputKeyType.UP))
                    {
                        _Bind_MoveUp = false;
                    }
                }

                if (_Bind_MoveDown)
                {
                    if (keyIsChanged(InputKeyType.DOWN))
                    {
                        _Bind_MoveDown = false;
                    }
                }

                if (_Bind_MoveLeft)
                {
                    if (keyIsChanged(InputKeyType.LEFT))
                    {
                        _Bind_MoveLeft = false;
                    }
                }

                if (_Bind_MoveRight)
                {
                    if (keyIsChanged(InputKeyType.RIGHT))
                    {
                        _Bind_MoveRight = false;
                    }
                }

                if (_Bind_Attack)
                {
                    if (keyIsChanged(InputKeyType.ATTACK))
                    {
                        _Bind_Attack = false;
                    }
                }

                if (_Bind_Jump)
                {
                    if (keyIsChanged(InputKeyType.JUMP))
                    {
                        _Bind_Jump = false;
                    }
                }

                if (_Bind_Turbo)
                {
                    if (keyIsChanged(InputKeyType.TURBO))
                    {
                        _Bind_Turbo = false;
                    }
                }
            }
        }

        public void _LoadKeys()
        {
            if (playerInput._SavedKeys._KeyCodeList.Count > 0)
            {
                foreach (KeyCode k in playerInput._SavedKeys._KeyCodeList)
                {
                    if (k == KeyCode.None)
                    {
                        _SetDefaultKeys();
                        break;
                    }
                }
            }
            else
            {
                _SetDefaultKeys();
            }

            for (int i = 0; i < playerInput._SavedKeys._KeyCodeList.Count; i++)
            {
                _DictionaryKeys[(InputKeyType)i] = playerInput._SavedKeys._KeyCodeList[i];
            }
        }

        private void savedKeys()
        {
            playerInput._SavedKeys._KeyCodeList.Clear();

            int count = System.Enum.GetValues(typeof(InputKeyType)).Length;

            for (int i = 0; i < count; i++)
            {
                playerInput._SavedKeys._KeyCodeList.Add(_DictionaryKeys[(InputKeyType)i]);
            }
        }

        public void _SetDefaultKeys()
        {
            _DictionaryKeys.Clear();

            _DictionaryKeys.Add(InputKeyType.UP, KeyCode.W);
            _DictionaryKeys.Add(InputKeyType.DOWN, KeyCode.S);
            _DictionaryKeys.Add(InputKeyType.LEFT, KeyCode.A);
            _DictionaryKeys.Add(InputKeyType.RIGHT, KeyCode.D);
            _DictionaryKeys.Add(InputKeyType.ATTACK, KeyCode.Return);
            _DictionaryKeys.Add(InputKeyType.JUMP, KeyCode.Space);
            _DictionaryKeys.Add(InputKeyType.TURBO, KeyCode.LeftShift);

            savedKeys();
        }

        private void setCustomKey(InputKeyType keyType, KeyCode keyCode)
        {
            Debug.Log("key changed: " + keyType.ToString() + " - > " + keyCode.ToString());

            if (!_DictionaryKeys.ContainsKey(keyType))
            {
                _DictionaryKeys.Add(keyType, keyCode);
            }
            else
            {
                _DictionaryKeys[keyType] = keyCode;
            }

            savedKeys();
        }

        private bool keyIsChanged(InputKeyType keyType)
        {
            if (Input.anyKey)
            {
                foreach (KeyCode k in possibleKeys)
                {
                    if (Input.GetKeyDown(KeyCode.Mouse0))
                    {
                        continue;
                    }

                    if (Input.GetKeyDown(k))
                    {
                        setCustomKey(keyType, k);
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
