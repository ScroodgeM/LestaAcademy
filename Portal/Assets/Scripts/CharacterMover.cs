using UnityEngine;

public class CharacterMover : MonoBehaviour
{
    [SerializeField] private CharacterController characterController;
    [SerializeField] private float moveSpeed;

    void Update()
    {
        Vector3 moveCommandNormalized = Vector3.zero;
        moveCommandNormalized.z = Input.GetAxis("Vertical");
        moveCommandNormalized.x = Input.GetAxis("Horizontal");
        characterController.SimpleMove(moveCommandNormalized * moveSpeed);
    }
}
