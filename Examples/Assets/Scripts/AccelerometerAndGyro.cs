
using UnityEngine;
using UnityEngine.InputSystem;

public class AccelerometerAndGyro : MonoBehaviour
{
    void Update()
    {
        Accelerometer accelerometer = Accelerometer.current;
        GravitySensor gravitySensor = GravitySensor.current;
        AttitudeSensor attitudeSensor = AttitudeSensor.current;

        // check for game over condition
        if (accelerometer != null)
        {
            Vector3 acceleration = accelerometer.acceleration.ReadValue();
            if (acceleration.sqrMagnitude > 1f)
            {
                Debug.LogWarning("you move device too fast, game over");
            }
        }

        // mode 1 - modify world's gravity
        if (gravitySensor != null)
        {
            Vector3 gravity = gravitySensor.gravity.ReadValue();
            Physics.gravity = gravity;
        }

        // mode 2 - compense device rotation
        if (attitudeSensor != null)
        {
            Quaternion attitude = attitudeSensor.attitude.ReadValue();
            transform.rotation = Quaternion.Inverse(attitude);
        }
    }
}
