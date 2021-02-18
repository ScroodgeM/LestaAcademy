using System;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Serializable]
    private struct IconSettings
    {
        public RenderTexture renderTexture;
        public Vector3 cameraPosition;
        public Vector3 cameraRotation;
        public float cameraSize;
    }

    [SerializeField] private IconSettings iconSettings;

    [SerializeField] private new Camera camera;

    public Texture2D MakeIcon()
    {
        RenderTexture rTex = iconSettings.renderTexture;

        transform.rotation = Quaternion.Euler(iconSettings.cameraRotation);
        transform.position = iconSettings.cameraPosition;
        camera.orthographicSize = iconSettings.cameraSize;
        camera.targetTexture = rTex;

        camera.Render();

        camera.targetTexture = null;

        Texture2D result = new Texture2D(rTex.width, rTex.height, TextureFormat.RGB24, false);
        result.filterMode = FilterMode.Point;

        RenderTexture.active = rTex;
        result.ReadPixels(new Rect(0, 0, rTex.width, rTex.height), 0, 0);
        result.Apply();
        RenderTexture.active = null;

        return result;
    }
}
