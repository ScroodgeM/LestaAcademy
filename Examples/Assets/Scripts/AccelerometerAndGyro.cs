using UnityEngine;

public class AccelerometerAndGyro : MonoBehaviour
{
    void Update()
    {
        Vector3 acceleration = Input.gyro.userAcceleration;

        Quaternion orientation = Input.gyro.attitude;

        if (acceleration.sqrMagnitude > 1f)
        {
            Debug.LogWarning("you move device too fast, game over");
        }

        // mode 1 - modify world's gravity
        Physics.gravity = orientation * Vector3.down;

        // mode 2 - compense device rotation
        transform.rotation = Quaternion.Inverse(orientation);
    }
}
