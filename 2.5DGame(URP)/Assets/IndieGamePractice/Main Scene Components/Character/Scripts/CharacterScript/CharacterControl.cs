using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace IndieGamePractice
{
    public enum _TransitionParameters
    {
        Move,
        Jump,
        ForceTransition,
        Grounded,
        Attack,
        ClickAnimation,
        TransitionIndex,
        Turbo,
        Turn,
    }

    public enum _IndieGamePracticeScenes
    {
        CharacterSelectionScene,
        MainScene,
        DayScene,
        MainMenu
    }

    public class CharacterControl : MonoBehaviour
    {
        [Header("Setup")]
        public _PlayableCharacterType characterType;
        public Animator _SkinnedMesh;
        public List<Collider> _RagdollParts = new List<Collider>();

        public GameObject _LeftHand;
        public GameObject _RightHand;
        public GameObject _LeftFoot;
        public GameObject _RightFoot;

        private List<TriggerDetector> _AllTriggers = new List<TriggerDetector>();
        private Dictionary<string, GameObject> childObjDictionaries = new Dictionary<string, GameObject>();

        [Header("Controller")]
        public bool _MoveUp;
        public bool _MoveDown;
        public bool _MoveRight;
        public bool _MoveLeft;
        public bool _Jump;
        public bool _Attack;
        public bool _Turbo;

        [HideInInspector] public ContactPoint[] _ContactPoints;
        //[HideInInspector] public float _GravityMultiplier;
        //[HideInInspector] public float _PullMultiplier;

        [Header("Sub Components")]
        public AnimationProgress _GetAnimationProgress;
        public Rigidbody _GetRigidBody;
        public LedgeChecker _GetLedgeChecker;
        public AIProgress _GetAiProgress;
        public DamageDetector _GetDamageDetector;
        public AIController _GetAiController;
        public BoxCollider _GetBoxCollider;
        public NavMeshObstacle _GetNavMeshObstacle;
        public ColliderSpheres _GetColliderSpheres;

        private void Awake()
        {
            _GetRigidBody = GetComponent<Rigidbody>();
            _GetLedgeChecker = GetComponentInChildren<LedgeChecker>();
            _GetAnimationProgress = GetComponent<AnimationProgress>();
            _GetAiProgress = GetComponentInChildren<AIProgress>();
            _GetDamageDetector = GetComponent<DamageDetector>();
            _GetBoxCollider = GetComponent<BoxCollider>();
            _GetNavMeshObstacle = GetComponent<NavMeshObstacle>();
            _GetColliderSpheres = GetComponentInChildren<ColliderSpheres>();
            _GetColliderSpheres._Owner = this;
            _GetColliderSpheres._CreateSphereEdge();
            _GetAiController = GetComponentInChildren<AIController>();

            if (null == _GetAiController)
            {
                if (_PlayableCharacterType.NONE == characterType)
                {
                    _GetNavMeshObstacle.carving = true;
                }
            }

            registerCharacter();
        }

        public void _CacheCharacterControl(Animator anim)
        {
            CharacterStateBase[] stateBase = anim.GetBehaviours<CharacterStateBase>();
            foreach (CharacterStateBase data in stateBase)
            {
                data._CharacterControl = this;
            }
        }

        private void registerCharacter()
        {
            if (!CharacterManager._GetInstance._AllCharacters.Contains(this))
            {
                CharacterManager._GetInstance._AllCharacters.Add(this);
            }
        }

        private void updateSize()
        {
            if (!_GetAnimationProgress._UpdatingBoxCollider)
            {
                return;
            }

            if (Vector3.SqrMagnitude(_GetBoxCollider.size - _GetAnimationProgress._TargetSize) > 0.01f)
            {
                _GetBoxCollider.size = Vector3.Lerp(_GetBoxCollider.size, _GetAnimationProgress._TargetSize, Time.deltaTime * _GetAnimationProgress._SizeSpeed);
                _GetAnimationProgress._UpdatingSpheres = true;
            }
        }

        private void updateCenter()
        {
            if (!_GetAnimationProgress._UpdatingBoxCollider)
            {
                return;
            }

            if (Vector3.SqrMagnitude(_GetBoxCollider.center - _GetAnimationProgress._TargetCenter) > 0.01f)
            {
                _GetBoxCollider.center = Vector3.Lerp(_GetBoxCollider.center, _GetAnimationProgress._TargetCenter, Time.deltaTime * _GetAnimationProgress._CenterSpeed);
                _GetAnimationProgress._UpdatingSpheres = true;
            }
        }

        private void FixedUpdate()
        {
            if (!_GetAnimationProgress._CancelPullGravity)
            {
                if (_GetRigidBody.velocity.y > 0f && !_Jump)
                {
                    _GetRigidBody.velocity += Vector3.down * _GetRigidBody.velocity.y * 0.1f;
                }
            }

            if (_GetAnimationProgress._RagdollTriggered)
            {
                _TurnOnRagdoll();
                _GetAnimationProgress._RagdollTriggered = false;
            }

            _GetAnimationProgress._UpdatingSpheres = false;

            updateCenter();
            updateSize();

            if (_GetAnimationProgress._UpdatingSpheres)
            {
                _GetColliderSpheres._RepositionFrontSpheres();
                _GetColliderSpheres._RepositionBottomSpheres();
                _GetColliderSpheres._RepositionBackSpheres();
            }
        }

        private void OnCollisionStay(Collision collision)
        {
            _ContactPoints = collision.contacts;
        }

        public List<TriggerDetector> _GetAllTriggers()
        {
            TriggerDetector[] triggers = GetComponentsInChildren<TriggerDetector>();
            foreach (TriggerDetector t in triggers)
            {
                if (!_AllTriggers.Contains(t))
                {
                    _AllTriggers.Add(t);
                }
            }
            return _AllTriggers;
        }

        public void _SetUpRagdoll()
        {
            _RagdollParts.Clear();

            Collider[] colliders = GetComponentsInChildren<Collider>();

            foreach (Collider col in colliders)
            {
                if (col.gameObject != gameObject)
                {
                    if (null == col.gameObject.GetComponent<LedgeChecker>())
                    {
                        col.isTrigger = true;
                        _RagdollParts.Add(col);

                        col.attachedRigidbody.interpolation = RigidbodyInterpolation.None; /*interpolate*/
                        col.attachedRigidbody.collisionDetectionMode = CollisionDetectionMode.Discrete/*continous dynamic*/;

                        CharacterJoint joint = col.GetComponent<CharacterJoint>();
                        if (null != joint)
                        {
                            joint.enableProjection = false;
                        }

                        if (null == col.GetComponent<TriggerDetector>())
                        {
                            col.gameObject.AddComponent<TriggerDetector>();
                        }
                    }
                }
            }
        }

        public void _TurnOnRagdoll()
        {
            Transform[] arr = GetComponentsInChildren<Transform>();

            foreach (Transform t in arr)
            {
                t.gameObject.layer = LayerMask.NameToLayer("DEADBODY");
            }

            foreach (Collider col in _RagdollParts)
            {
                TriggerDetector trigger = col.GetComponent<TriggerDetector>();
                trigger._LastPosition = col.gameObject.transform.localPosition;
                trigger._LastRotation = col.gameObject.transform.localRotation;
            }

            _GetRigidBody.useGravity = false;
            _GetRigidBody.velocity = Vector3.zero;
            _GetBoxCollider.enabled = false;
            _SkinnedMesh.enabled = false;
            _SkinnedMesh.avatar = null;

            foreach (Collider col in _RagdollParts)
            {
                col.isTrigger = false;

                TriggerDetector trigger = col.GetComponent<TriggerDetector>();
                col.transform.localPosition = trigger._LastPosition;
                col.transform.localRotation = trigger._LastRotation;

                col.attachedRigidbody.velocity = Vector3.zero;
            }
        }

        public void _CharacterMove(float movementSpeed, float speedGraph)
        {
            transform.Translate(Vector3.forward * movementSpeed * speedGraph * Time.deltaTime);
        }

        public bool _IsFacingForward()
        {
            if (transform.forward.z > 0f)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public Transform _GetBodyPartTransform(string bodyPart)
        {
            foreach (Collider col in _RagdollParts)
            {
                if (col.name.Contains(bodyPart))
                {
                    return col.transform;
                }
            }
            return null;
        }

        public GameObject _GetChildObject(string target)
        {
            if (childObjDictionaries.ContainsKey(target))
            {
                return childObjDictionaries[target];
            }

            Transform[] arr = GetComponentsInChildren<Transform>();

            foreach (Transform t in arr)
            {
                if (t.name == target)
                {
                    childObjDictionaries.Add(target, t.gameObject);
                    return t.gameObject;
                }
            }

            return null;
        }

        public void _FaceForward(bool isFacingForward)
        {
            if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == _IndieGamePracticeScenes.CharacterSelectionScene.ToString())
            {
                return;
            }

            if (!_SkinnedMesh.enabled)
            {
                return;
            }

            if (isFacingForward)
            {
                transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            }
            else
            {
                transform.rotation = Quaternion.Euler(0f, 180f, 0f);
            }
        }
    }
}