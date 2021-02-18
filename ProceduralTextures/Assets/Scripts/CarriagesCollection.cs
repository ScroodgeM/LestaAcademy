
using UnityEngine;

public class CarriagesCollection : MonoBehaviour
{
    [SerializeField] private Transform elementsParent;
    [SerializeField] private CarriagePortrait elementPrefab;

    private void Start()
    {
        foreach (GameObject photoTarget in Root.PhotoTargets)
        {
            Instantiate(elementPrefab, elementsParent).Init(photoTarget);
        }
    }
}
