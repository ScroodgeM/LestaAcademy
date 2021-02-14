using System;
using UnityEngine;

public class VehicleController : MonoBehaviour
{
    [Serializable]
    private struct Wheel
    {
        public WheelCollider wheelCollider;
        public bool drive;
        public bool steer;
        public Transform graphics;
    }

    [SerializeField] private Wheel[] wheels;
    [SerializeField] private float maxSteerAngle;
    [SerializeField] private float enginePower;

    private void FixedUpdate()
    {
        foreach (Wheel wheel in wheels)
        {
            if (wheel.steer)
            {
                wheel.wheelCollider.steerAngle = maxSteerAngle * Input.GetAxis("Horizontal");
            }

            if (wheel.drive)
            {
                wheel.wheelCollider.motorTorque = enginePower * Input.GetAxis("Vertical");
            }
        }
    }
}
