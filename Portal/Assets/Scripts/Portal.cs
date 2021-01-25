using System;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public event Action OnPlayerEnterPortal = () => { };

    [SerializeField] private PortalEnterTrigger portalEnterTrigger;

    private void Awake()
    {
        portalEnterTrigger.OnPlayerEnterPortal += () => { OnPlayerEnterPortal(); };
    }

    public void SetActive(bool active)
    {
        gameObject.SetActive(active);
    }
}
