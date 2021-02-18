
using System;
using UnityEngine;

public static class Texture2DExtensions
{
    public static Texture2D FromBytes(string textureName, byte[] rawBytes)
    {
        Texture2D tex = new Texture2D(4, 4);
        tex.LoadImage(rawBytes);
        tex.name = textureName;
        return tex;
    }

    public static byte[] ToBytesPNG(Texture2D texture)
    {
        if (texture == null)
        {
            return null;
        }
        try
        {
            return texture.EncodeToPNG();
        }
        catch (Exception ex)
        {
            Debug.LogException(new Exception($"texture {texture.name} isn't readable", ex));
            return null;
        }
    }

    public static byte[] ToBytesJPEG(Texture2D texture, int jpegCompressionQuality)
    {
        if (texture == null)
        {
            return null;
        }
        try
        {
            return texture.EncodeToJPG(jpegCompressionQuality);
        }
        catch (Exception ex)
        {
            Debug.LogException(new Exception($"texture {texture.name} isn't readable", ex));
            return null;
        }
    }

    public static Texture2D RenderTextureToTexture2D(RenderTexture rt, bool withAlpha)
    {
        Texture2D result = new Texture2D(rt.width, rt.height, withAlpha ? TextureFormat.ARGB32 : TextureFormat.RGB24, false);

        RenderTexture originalActiveRenderTexture = RenderTexture.active;

        RenderTexture.active = rt;
        result.ReadPixels(new Rect(0, 0, rt.width, rt.height), 0, 0);
        result.Apply();

        RenderTexture.active = originalActiveRenderTexture;

        return result;
    }
}
