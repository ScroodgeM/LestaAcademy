using UnityEngine;

[ExecuteInEditMode]
public class DrawAnotherWorld : MonoBehaviour
{
    [SerializeField] private Texture anotherWorldTexture;
    [SerializeField] private Shader shader;

    private Material material;

    private void Awake()
    {
        material = new Material(shader);
    }

    private void OnPostRender()
    {
        Graphics.Blit(anotherWorldTexture, material, -1);
    }
}
