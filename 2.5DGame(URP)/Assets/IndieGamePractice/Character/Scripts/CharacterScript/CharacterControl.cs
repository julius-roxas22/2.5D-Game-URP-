using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IndieGamePractice
{
    public enum TransitionParameters
    {
        Move,
        Jump,
        ForceTransition,
        Grounded,
        Attack,
    }

    public class CharacterControl : MonoBehaviour
    {
        public Animator _SkinnedMesh;
        public List<GameObject> _BottomSpheres = new List<GameObject>();
        public List<GameObject> _FrontSpheres = new List<GameObject>();
        public List<Collider> _RagdollParts = new List<Collider>();
        public List<Collider> _CollidingParts = new List<Collider>();

        private List<TriggerDetector> _AllTriggers = new List<TriggerDetector>();

        [HideInInspector] public bool _MoveRight;
        [HideInInspector] public bool _MoveLeft;
        [HideInInspector] public bool _Jump;
        [HideInInspector] public bool _Attack;

        [HideInInspector] public float _GravityMultiplier;
        [HideInInspector] public float _PullMultiplier;

        private Rigidbody rigidBody;

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

            //setUpRagdoll();
            createSphereEdge();

            if (switchBack)
            {
                _FaceForward(false);
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
                    col.isTrigger = true;
                    _RagdollParts.Add(col);

                    if (null == col.GetComponent<TriggerDetector>())
                    {
                        col.gameObject.AddComponent<TriggerDetector>();
                    }

                }
            }
        }

        public void _TurnOnRagdoll()
        {
            _GetRigidBody.useGravity = false;
            _GetRigidBody.velocity = Vector3.zero;
            GetComponent<BoxCollider>().enabled = false;
            _SkinnedMesh.enabled = false;
            _SkinnedMesh.avatar = null;
            foreach (Collider col in _RagdollParts)
            {
                col.isTrigger = false;
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

            GameObject topFront = createColliderEdge(new Vector3(0f, top, front));

            GameObject bottomFront = createColliderEdge(new Vector3(0f, bottom, front));
            GameObject bottomBack = createColliderEdge(new Vector3(0f, bottom, back));
            _BottomSpheres.Add(bottomFront);
            _BottomSpheres.Add(bottomBack);
            _FrontSpheres.Add(bottomFront);
            _FrontSpheres.Add(topFront);

            float horSec = (bottomBack.transform.position - bottomFront.transform.position).magnitude / 5f;
            float verSec = (bottomFront.transform.position - topFront.transform.position).magnitude / 9f;

            createSphereEdges(bottomBack.transform.position, Vector3.forward, horSec, 4, _BottomSpheres);
            createSphereEdges(bottomFront.transform.position, Vector3.up, verSec, 8, _FrontSpheres);
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

        public void _FaceForward(bool isFacingForward)
        {
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