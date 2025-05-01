using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IndieGamePractice
{
    public enum DeathType
    {
        None,
        LaunchIntoAir,
        GroundShock
    }

    public class DeathLoader : MonoBehaviour
    {
        public List<DeathData> _DeathData = new List<DeathData>();
    }
}