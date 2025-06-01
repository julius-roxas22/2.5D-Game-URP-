using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

        [HideInInspector] public bool _MoveUp;
        [HideInInspector] public bool _MoveDown;
        [HideInInspector] public bool _MoveRight;
        [HideInInspector] public bool _MoveLeft;
        [HideInInspector] public bool _Jump;
        [HideInInspector] public bool _Attack;
        [HideInInspector] public bool _Turbo;

        [HideInInspector] public float _GravityMultiplier;
        [HideInInspector] public float _PullMultiplier;

        private AnimationProgress animationProgress;

        private Rigidbody rigidBody;
        private LedgeChecker ledgeChecker;
        private AIProgress aIProgress;
        private DamageDetector damageDetector;
        private AIController aiController;
        private BoxCollider boxCollider;

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
            bool switchBack = false;

            if (!_IsFacingForward())
            {
                switchBack = true;
            }

            _FaceForward(true);
            createSphereEdge();

            if (switchBack)
            {
                _FaceForward(false);
            }

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
            }
        }

        private void FixedUpdate()
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

            if (_GetAnimationProgress._RagdollTriggered)
            {
                _TurnOnRagdoll();
                _GetAnimationProgress._RagdollTriggered = false;
            }

            updateCenter();
            updateSize();
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
            GetComponent<BoxCollider>().enabled = false;
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
            BoxCollider box = GetComponent<BoxCollider>();

            float top = box.bounds.center.y + box.bounds.extents.y;
            float bottom = box.bounds.center.y - box.bounds.extents.y;
            float front = box.bounds.center.z + box.bounds.extents.z;
            float back = box.bounds.center.z - box.bounds.extents.z;

            GameObject bottomFrontHor = createColliderEdge(new Vector3(0f, bottom, front));
            GameObject bottomFrontVer = createColliderEdge(new Vector3(0f, bottom + 0.05f, front));
            GameObject bottomBack = createColliderEdge(new Vector3(0f, bottom, back));
            GameObject topFront = createColliderEdge(new Vector3(0f, top, front));

            _BottomSpheres.Add(bottomFrontHor);
            _BottomSpheres.Add(bottomBack);
            _FrontSpheres.Add(bottomFrontVer);
            _FrontSpheres.Add(topFront);

            float horSec = (bottomBack.transform.position - bottomFrontHor.transform.position).magnitude / 5f;
            createSphereEdges(bottomBack.transform.position, Vector3.forward, horSec, 4, _BottomSpheres);

            float verSec = (bottomFrontVer.transform.position - topFront.transform.position).magnitude / 9f;
            createSphereEdges(bottomFrontVer.transform.position, Vector3.up, verSec, 8, _FrontSpheres);
        }

        private void createSphereEdges(Vector3 startPos, Vector3 dir, float sec, float iteration, List<GameObject> spheres)
        {
            for (int i = 0; i < iteration; i++)
            {
                Vector3 pos = startPos + dir * (sec * (i + 1));
                GameObject obj = createColliderEdge(pos);
                spheres.Add(obj);
            }
        }

        private GameObject createColliderEdge(Vector3 pos)
        {
            GameObject obj = Instantiate(Resources.Load("ColliderEdge", typeof(GameObject)), pos, Quaternion.identity, transform) as GameObject;
            return obj;
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