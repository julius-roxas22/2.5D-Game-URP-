using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace IndieGamePractice
{
    [CustomEditor(typeof(PathFindingAgent))]
    public class PathFindingAgentEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            PathFindingAgent agent = (PathFindingAgent)target;
            if (GUILayout.Button("Go to Agent"))
            {
                agent._GotoTarget();
            }
        }
    }

}

