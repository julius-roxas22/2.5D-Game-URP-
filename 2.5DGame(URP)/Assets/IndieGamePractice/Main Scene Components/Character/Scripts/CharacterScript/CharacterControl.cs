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
        MainScene
    }

    public class CharacterControl : MonoBehaviour
    {
        [Header("Setup")]
        public _PlayableCharacterType characterType;
        public Animator _SkinnedMesh;
        public List<GameObject> _BottomSpheres = new List<GameObject>();
        public List<GameObject> _FrontSpheres = new List<GameObject>();
        public List<Collider> _RagdollParts = new List<Collider>();
        public GameObject _LeftHand;
        public GameObject _RightHand;

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
        [HideInInspector] public float _GravityMultiplier;
        [HideInInspector] public float _PullMultiplier;

        private AnimationProgress animationProgress;
        private Rigidbody rigidBody;
        private LedgeChecker ledgeChecker;
        private AIProgress aIProgress;
        private DamageDetector damageDetector;
        private AIController aiController;
        private BoxCollider boxCollider;
        private NavMeshObstacle navObstacle;

        public NavMeshObstacle _GetNavMeshObstacle
        {
            get
            {
                if (null == navObstacle)
                {
                    navObstacle = GetComponent<NavMeshObstacle>();
                }
                return navObstacle;
            }
        }

        public BoxCollider _GetBoxCollider
        {
            get
            {
                if (null == boxCollider)
                {
                    boxCollider = GetComponent<BoxCollider>();
                }
                return boxCollider;
            }
        }

        public AIController _GetAiController
        {
            get
            {
                if (null == aiController)
                {
                    aiController = GetComponentInChildren<AIController>();
                }
                return aiController;
            }
        }

        public DamageDetector _GetDamageDetector
        {
            get
            {
                if (null == damageDetector)
                {
                    damageDetector = GetComponent<DamageDetector>();
                }
                return damageDetector;
            }
        }

        public AIProgress _GetAiProgress
        {
            get
            {
                if (null == aIProgress)
                {
                    aIProgress = GetComponentInChildren<AIProgress>();
                }
                return aIProgress;
            }
        }

        public AnimationProgress _GetAnimationProgress
        {
            get
            {
                if (null == animationProgress)
                {
                    animationProgress = GetComponent<AnimationProgress>();
                }
                return animationProgress;
            }
        }

        public LedgeChecker _GetLedgeChecker
        {
            get
            {
                if (null == ledgeChecker)
                {
                    ledgeChecker = GetComponentInChildren<LedgeChecker>();
                }
                return ledgeChecker;
            }
        }

        public Rigidbody _GetRigidBody
        {
            get
            {
                if (null == rigidBody)
                {
                    rigidBody = GetComponent<Rigidbody>();
                }
                return rigidBody;
            }
        }

        private void Awake()
        {
            createSphereEdge();
            registerCharacter();
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
                if (_GetRigidBody.velocity.y < 0f)
                {
                    //_GetRigidBody.velocity -= Vector3.up * _GravityMultiplier;
                    _GetRigidBody.velocity += Vector3.down * _GravityMultiplier;
                }

                if (_GetRigidBody.velocity.y > 0f && !_Jump)
                {
                    //_GetRigidBody.velocity -= Vector3.up * _PullMultiplier;
                    _GetRigidBody.velocity += Vector3.down * _PullMultiplier;
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
                _RepositionFrontSpheres();
                _RepositionBottomSpheres();
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

        private void createSphereEdge()
        {
            for (int i = 0; i < 10; i++)
            {
                GameObject obj = Instantiate(Resources.Load("ColliderEdge", typeof(GameObject)), Vector3.zero, Quaternion.identity, transform) as GameObject;
                obj.transform.parent = transform;
                _FrontSpheres.Add(obj);
            }
            _RepositionFrontSpheres();

            for (int i = 0; i < 5; i++)
            {
                GameObject obj = Instantiate(Resources.Load("ColliderEdge", typeof(GameObject)), Vector3.zero, Quaternion.identity, transform) as GameObject;
                obj.transform.parent = transform;
                _BottomSpheres.Add(obj);
            }
            _RepositionBottomSpheres();
        }

        public void _RepositionFrontSpheres()
        {
            float top = _GetBoxCollider.bounds.center.y + (_GetBoxCollider.bounds.size.y / 2f);
            float bottom = _GetBoxCollider.bounds.center.y - (_GetBoxCollider.bounds.size.y / 2f);
            float front = _GetBoxCollider.bounds.center.z + (_GetBoxCollider.bounds.size.z / 2f);

            _FrontSpheres[0].transform.localPosition = new Vector3(0, bottom + 0.05f, front) - transform.position;
            _FrontSpheres[1].transform.localPosition = new Vector3(0, top, front) - transform.position;
            float interval = (top - bottom) / 9;

            for (int i = 2; i < _FrontSpheres.Count; i++)
            {
                _FrontSpheres[i].transform.localPosition = new Vector3(0, bottom + (interval * (i - 1)), front) - transform.position;
            }
        }

        public void _RepositionBottomSpheres()
        {
            float bottom = _GetBoxCollider.bounds.center.y - (_GetBoxCollider.bounds.size.y / 2f);
            float front = _GetBoxCollider.bounds.center.z + (_GetBoxCollider.bounds.size.z / 2f);
            float back = _GetBoxCollider.bounds.center.z - (_GetBoxCollider.bounds.size.z / 2f);

            _BottomSpheres[0].transform.localPosition = new Vector3(0, bottom, back) - transform.position;
            _BottomSpheres[1].transform.localPosition = new Vector3(0, bottom, front) - transform.position;

            float interval = (front - back) / 4;

            for (int i = 2; i < _BottomSpheres.Count; i++)
            {
                _BottomSpheres[i].transform.localPosition = new Vector3(0, bottom, back + (interval * (i - 1))) - transform.position;
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