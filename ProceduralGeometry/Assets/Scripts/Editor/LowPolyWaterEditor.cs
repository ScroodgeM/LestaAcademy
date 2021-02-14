using UnityEditor;
using UnityEngine;

namespace Battlegrounds
{
    [CustomEditor(typeof (LowPolyWater))]
    public class LowPolyWaterEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if (GUILayout.Button("Generate Water"))
            {
                ((LowPolyWater) target).GenerateWater();
            }
        }
    }
}