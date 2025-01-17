using UnityEditor;
using UnityEngine;

namespace Battlegrounds
{
    [CustomEditor(typeof(DungeonCreator))]
    public class DungeonCreatorEditor : Editor
    {
        private Texture2D debugDungeonTexture;

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

            EditorGUILayout.BeginHorizontal();
            {
                if (GUILayout.Button("Debug"))
                {
                    GenerateDebugDungeonTexture();
                }

                if (GUILayout.Button("Debug +1 step"))
                {
                    serializedObject.FindProperty("steps").intValue += 1;
                    serializedObject.ApplyModifiedProperties();
                    GenerateDebugDungeonTexture();
                }

                if (GUILayout.Button("Debug w/ new seed"))
                {
                    serializedObject.FindProperty("seed").intValue = Random.Range(int.MinValue, int.MaxValue);
                    serializedObject.ApplyModifiedProperties();
                    GenerateDebugDungeonTexture();
                }
            }
            EditorGUILayout.EndHorizontal();
        }

        private void GenerateDebugDungeonTexture()
        {
            if (debugDungeonTexture != null)
            {
                DestroyImmediate(debugDungeonTexture);
            }

            debugDungeonTexture = ((DungeonCreator)target).DebugDungeon();
        }

        public override bool HasPreviewGUI() => debugDungeonTexture != null;

        public override void OnPreviewGUI(Rect r, GUIStyle background)
        {
            GUI.DrawTexture(r, debugDungeonTexture);
        }
    }
}
