
using System.Collections.Generic;
using UnityEngine;

public class ImageCombiner : MonoBehaviour
{
    [SerializeField] private Material imageDrawerMaterial;
    [SerializeField] private Vector2Int renderSize;

    public Texture2D Combine(IEnumerable<TextureWithCode.Layer> layers)
    {
        RenderTexture combinedImage = new RenderTexture(renderSize.x, renderSize.y, 0, RenderTextureFormat.ARGB32)
        {
            wrapMode = TextureWrapMode.Repeat
        };

        Draw(imageDrawerMaterial, layers, combinedImage);

        return Texture2DExtensions.RenderTextureToTexture2D(combinedImage, true);
    }

    private static void Draw(Material imageDrawerMaterial, IEnumerable<TextureWithCode.Layer> layers, RenderTexture rt)
    {
        RenderTexture originalActiveRenderTexture = RenderTexture.active;

        RenderTexture.active = rt;

        GL.PushMatrix();

        GL.LoadPixelMatrix(
            -0.5f * rt.width + 0.5f,
            +0.5f * rt.width - 0.5f,
            -0.5f * rt.height + 0.5f,
            +0.5f * rt.height - 0.5f
            );

        DrawContent(imageDrawerMaterial, layers);

        GL.PopMatrix();

        RenderTexture.active = originalActiveRenderTexture;
    }

    private static void DrawContent(Material imageDrawerMaterial, IEnumerable<TextureWithCode.Layer> layers)
    {
        foreach (TextureWithCode.Layer layer in layers)
        {
            DrawElement(imageDrawerMaterial, layer.texture, layer.rotation, layer.position, layer.size);
        }
    }

    private static void DrawElement(Material imageDrawerMaterial, Texture texture, float rotation, Vector2 center, Vector2 size)
    {
        imageDrawerMaterial.SetTexture("_MainTex", texture);

        imageDrawerMaterial.SetPass(0);

        DrawQuad(rotation, center, size);
    }

    private static void DrawQuad(float rotation, Vector2 center, Vector2 size)
    {
        GL.Begin(GL.QUADS);
        DrawVertex(0f, 0f, rotation, center, size);
        DrawVertex(1f, 0f, rotation, center, size);
        DrawVertex(1f, 1f, rotation, center, size);
        DrawVertex(0f, 1f, rotation, center, size);
        GL.End();
    }

    private static void DrawVertex(float uvX, float uvY, float rotation, Vector2 center, Vector2 size)
    {
        // mesh uv vertex
        Vector3 vert = new Vector3(uvX - 0.5f, uvY - 0.5f, 0f);
        // scale to size
        vert = Vector3.Scale(vert, size);
        // rotate
        vert = Quaternion.AngleAxis(rotation, Vector3.forward) * vert;
        // add center
        vert = vert + (Vector3)center;
        // draw
        GL.TexCoord2(uvX, uvY);
        GL.Vertex3(vert.x, vert.y, vert.z);
    }
}
