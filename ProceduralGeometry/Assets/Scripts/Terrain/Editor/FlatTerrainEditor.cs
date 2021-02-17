using UnityEditor;
using UnityEngine;

namespace Battlegrounds
{
    [CustomEditor(typeof(FlatTerrain))]
    public class FlatTerrainEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            EditorGUILayout.BeginHorizontal();
            {
                if (GUILayout.Button("Clear"))
                {
                    ((FlatTerrain)target).Clear();
                }

                if (GUILayout.Button("Generate"))
                {
                    ((FlatTerrain)target).GenerateTerrain();
                }
            }
            EditorGUILayout.EndHorizontal();
        }
    }
}
