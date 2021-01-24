using UnityEngine;

public class DrawAnotherWorld : MonoBehaviour
{
    [SerializeField] private Material material;

    public void OnPostRender()
    {
        GL.PushMatrix();
        GL.LoadOrtho();

        // activate the first shader pass (in this case we know it is the only pass)
        material.SetPass(0);
        // draw a quad over whole screen
        GL.Begin(GL.QUADS);
        GL.Vertex3(0, 0, 0);
        GL.Vertex3(1, 0, 0);
        GL.Vertex3(1, 1, 0);
        GL.Vertex3(0, 1, 0);
        GL.End();

        GL.PopMatrix();
    }
}
