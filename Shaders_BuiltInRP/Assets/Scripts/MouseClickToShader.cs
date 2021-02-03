using UnityEngine;
using System.Collections;

public class MouseClickToShader : MonoBehaviour
{
    public Material[] ShieldMaterials;
    void OnMouseDown()
    {
        Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit mouseHit;
        if (GetComponent<Collider>().Raycast(mouseRay, out mouseHit, Mathf.Infinity))
        {
            Vector3 localHit = transform.InverseTransformPoint(mouseHit.point);
            Vector4 hit = localHit;
            hit.w = 0.002f;
            foreach (var material in ShieldMaterials)
            {
                material.SetVector("_PowerHitPoint", hit);
                material.SetFloat("_PowerHitTime", Time.time);
            }
        }
    }
}
