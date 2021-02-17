using UnityEditor;
using UnityEngine;

namespace Battlegrounds
{
    [CustomEditor(typeof(LowPolyTerrain), true)]
    public class LowPolyTerrainEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

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
        }
    }
}
