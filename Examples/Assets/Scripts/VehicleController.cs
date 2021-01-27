using UnityEngine;

public class VehicleController : MonoBehaviour
{
    [SerializeField]
    private enum ControlMode
    {
        Local,
        World,
    }

    [SerializeField] private float rotationSpeed;
    [SerializeField] private float movementSpeed;
    [SerializeField] private ControlMode controlMode;

    private void Update()
    {
        Vector3 moveCommand = Vector3.zero;
        moveCommand.x = Input.GetAxis("Horizontal");
        moveCommand.z = Input.GetAxis("Vertical");

        if (moveCommand != Vector3.zero)
        {
            switch (controlMode)
            {
                case ControlMode.Local:
                    transform.rotation *= Quaternion.AngleAxis(Time.deltaTime * rotationSpeed * moveCommand.x, Vector3.up);
                    transform.position += transform.forward * movementSpeed * moveCommand.z * Time.deltaTime;
                    break;

                case ControlMode.World:
                    transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(moveCommand), Time.deltaTime * rotationSpeed);
                    transform.position += transform.forward * Mathf.Min(moveCommand.magnitude, 1f) * movementSpeed * Time.deltaTime;
                    break;
            }
        }
    }
}
