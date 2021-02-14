using UnityEditor;
using UnityEngine;

namespace Battlegrounds
{
    [CustomEditor(typeof(LowPolyTerrain))]
    public class LowPolyTerrainEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            serializedObject.Update();

            EditorGUILayout.BeginHorizontal();
            {
                if (GUILayout.Button("Clear"))
                {
                    ((LowPolyTerrain)target).Clear();
                }

                if (GUILayout.Button("Generate"))
                {
                    ((LowPolyTerrain)target).GenerateTerrain();
                }
            }
            EditorGUILayout.EndHorizontal();

            serializedObject.ApplyModifiedProperties();
        }
    }
}
