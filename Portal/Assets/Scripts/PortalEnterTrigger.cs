using System;
using UnityEngine;

public class PortalEnterTrigger : MonoBehaviour
{
    public event Action OnPlayerEnterPortal = () => { };

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<CharacterMover>() != null)
        {
            OnPlayerEnterPortal();
        }
    }
}
