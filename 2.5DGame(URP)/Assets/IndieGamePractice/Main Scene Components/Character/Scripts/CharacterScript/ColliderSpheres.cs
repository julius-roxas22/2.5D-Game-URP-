using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IndieGamePractice
{
    public class ColliderSpheres : MonoBehaviour
    {
        [Header("Collider Setup")]
        public List<GameObject> _BottomSpheres = new List<GameObject>();
        public List<GameObject> _FrontSpheres = new List<GameObject>();
        public List<GameObject> _BackSpheres = new List<GameObject>();

        [HideInInspector] public CharacterControl _Owner;

        public void _CreateSphereEdge()
        {
            for (int i = 0; i < 10; i++)
            {
                GameObject obj = Instantiate(Resources.Load("ColliderEdge", typeof(GameObject)), Vector3.zero, Quaternion.identity, transform) as GameObject;
                obj.transform.parent = transform.Find("Front");
                _FrontSpheres.Add(obj);
            }
            _RepositionFrontSpheres();

            for (int i = 0; i < 5; i++)
            {
                GameObject obj = Instantiate(Resources.Load("ColliderEdge", typeof(GameObject)), Vector3.zero, Quaternion.identity, transform) as GameObject;
                obj.transform.parent = transform.Find("Bottom");
                _BottomSpheres.Add(obj);
            }
            _RepositionBottomSpheres();

            for (int i = 0; i < 10; i++)
            {
                GameObject obj = Instantiate(Resources.Load("ColliderEdge", typeof(GameObject)), Vector3.zero, Quaternion.identity, transform) as GameObject;
                obj.transform.parent = transform.Find("Back");
                _BackSpheres.Add(obj);
            }
            _RepositionBackSpheres();
        }

        public void _RepositionFrontSpheres()
        {
            float top = _Owner._GetBoxCollider.bounds.center.y + (_Owner._GetBoxCollider.bounds.size.y / 2f);
            float bottom = _Owner._GetBoxCollider.bounds.center.y - (_Owner._GetBoxCollider.bounds.size.y / 2f);
            float front = _Owner._GetBoxCollider.bounds.center.z + (_Owner._GetBoxCollider.bounds.size.z / 2f);

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
            float bottom = _Owner._GetBoxCollider.bounds.center.y - (_Owner._GetBoxCollider.bounds.size.y / 2f);
            float front = _Owner._GetBoxCollider.bounds.center.z + (_Owner._GetBoxCollider.bounds.size.z / 2f);
            float back = _Owner._GetBoxCollider.bounds.center.z - (_Owner._GetBoxCollider.bounds.size.z / 2f);

            _BottomSpheres[0].transform.localPosition = new Vector3(0, bottom, back) - transform.position;
            _BottomSpheres[1].transform.localPosition = new Vector3(0, bottom, front) - transform.position;

            float interval = (front - back) / 4;

            for (int i = 2; i < _BottomSpheres.Count; i++)
            {
                _BottomSpheres[i].transform.localPosition = new Vector3(0, bottom, back + (interval * (i - 1))) - transform.position;
            }
        }

        public void _RepositionBackSpheres()
        {
            float top = _Owner._GetBoxCollider.bounds.center.y + (_Owner._GetBoxCollider.bounds.size.y / 2f);
            float bottom = _Owner._GetBoxCollider.bounds.center.y - (_Owner._GetBoxCollider.bounds.size.y / 2f);
            float back = _Owner._GetBoxCollider.bounds.center.z - (_Owner._GetBoxCollider.bounds.size.z / 2f);

            _BackSpheres[0].transform.localPosition = new Vector3(0, bottom, back) - transform.position;
            _BackSpheres[1].transform.localPosition = new Vector3(0, top, back) - transform.position;
            float interval = (top - bottom) / 9;

            for (int i = 2; i < _BackSpheres.Count; i++)
            {
                _BackSpheres[i].transform.localPosition = new Vector3(0, bottom + (interval * (i - 1)), back) - transform.position;
            }
        }
    }
}