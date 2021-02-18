using System.Collections.Generic;
using UnityEngine;

public class Root : MonoBehaviour
{
    [SerializeField] private PreviewStage previewStage;
    [SerializeField] private List<GameObject> photoTargets;

    private static Root instance;

    private void Awake()
    {
        instance = this;
    }

    public static PreviewStage PreviewStage => instance.previewStage;

    public static IEnumerable<GameObject> PhotoTargets => instance.photoTargets;
}
