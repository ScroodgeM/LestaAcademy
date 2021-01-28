using UnityEngine;

public class FarmerController : MonoBehaviour
{
    [SerializeField] private Animator characterAnimator;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float movementSpeed;

    private void Update()
    {
        Vector3 moveCommand = Vector3.zero;
        moveCommand.x = Input.GetAxis("Horizontal");
        moveCommand.z = Input.GetAxis("Vertical");

        if (moveCommand != Vector3.zero)
        {
            transform.rotation *= Quaternion.AngleAxis(Time.deltaTime * rotationSpeed * moveCommand.x, Vector3.up);
            transform.position += transform.forward * movementSpeed * moveCommand.z * Time.deltaTime;
        }

        characterAnimator.SetFloat("Speed_f", moveCommand.z);
    }
}
