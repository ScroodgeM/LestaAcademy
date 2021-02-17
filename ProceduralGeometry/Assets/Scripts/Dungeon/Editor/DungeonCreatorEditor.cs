using UnityEditor;
using UnityEngine;

namespace Battlegrounds
{
    [CustomEditor(typeof(DungeonCreator))]
    public class DungeonCreatorEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            EditorGUILayout.BeginHorizontal();
            {
                if (GUILayout.Button("Clear"))
                {
                    ((DungeonCreator)target).Clear();
                }

                if (GUILayout.Button("Generate"))
                {
                    ((DungeonCreator)target).GenerateDungeon();
                }

                if (GUILayout.Button("Generate w/ new seed"))
                {
                    serializedObject.FindProperty("seed").intValue = Random.Range(int.MinValue, int.MaxValue);
                    serializedObject.ApplyModifiedProperties();
                    ((DungeonCreator)target).GenerateDungeon();
                }
            }
            EditorGUILayout.EndHorizontal();
        }
    }
}
