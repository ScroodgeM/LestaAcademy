using System;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class TextureWithCode : MonoBehaviour
{
    [Serializable]
    public struct Layer
    {
        public Texture texture;
        public Vector2Int position;
        public Vector2 size;
        public float rotation;
    }

    [SerializeField] private string pathToSaveImages;
    [SerializeField] private RawImage preview;
    [SerializeField] private Button doItButton;
    [SerializeField] private ImageCombiner imageCombiner;
    [SerializeField] private Layer[] layers;

    private void Awake()
    {
        doItButton.onClick.AddListener(() => { DoIt(); });
    }

    private void DoIt()
    {
        Texture2D texture = imageCombiner.Combine(layers);

        preview.texture = texture;

        File.WriteAllBytes(Path.Combine(pathToSaveImages, $"{DateTimeOffset.UtcNow.ToUnixTimeMilliseconds()}.png"), Texture2DExtensions.ToBytesPNG(texture));
    }
}
