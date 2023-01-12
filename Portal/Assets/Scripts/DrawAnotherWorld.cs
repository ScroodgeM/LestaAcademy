using UnityEngine;

public class DrawAnotherWorld : MonoBehaviour
{
    [SerializeField] private Material material;

    public void OnPostRender()
    {
        GL.PushMatrix();
        GL.LoadOrtho();

        for (int i = 0; i < material.passCount; i++)
        {
            material.SetPass(i);
            
            GL.Begin(GL.QUADS);
            GL.Vertex3(0, 0, 0);
            GL.Vertex3(1, 0, 0);
            GL.Vertex3(1, 1, 0);
            GL.Vertex3(0, 1, 0);
            GL.End();
        }

        GL.PopMatrix();
    }
}
