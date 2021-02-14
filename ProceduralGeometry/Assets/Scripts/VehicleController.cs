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

    private float[] wheelsRotationsX;

    private void Awake()
    {
        wheelsRotationsX = new float[wheels.Length];
    }

    private void Update()
    {
        for (int i = 0; i < wheels.Length; i++)
        {
            Wheel wheel = wheels[i];
            WheelCollider wc = wheel.wheelCollider;

            if (wc.GetGroundHit(out WheelHit hit) == true)
            {
                wheel.graphics.localPosition = Vector3.down * ((hit.point - wc.transform.TransformPoint(wc.center)).magnitude - wc.radius);
            }
            else
            {
                wheel.graphics.localPosition = Vector3.down * wc.suspensionDistance;
            }

            wheel.graphics.localRotation =  Quaternion.AngleAxis(wc.steerAngle, Vector3.up) * Quaternion.AngleAxis(wheelsRotationsX[i] * 360f, Vector3.right);
        }
    }

    private void FixedUpdate()
    {
        for (int i = 0; i < wheels.Length; i++)
        {
            Wheel wheel = wheels[i];

            if (wheel.steer)
            {
                wheel.wheelCollider.steerAngle = maxSteerAngle * Input.GetAxis("Horizontal");
            }

            if (wheel.drive)
            {
                wheel.wheelCollider.motorTorque = enginePower * Input.GetAxis("Vertical");
            }

            wheelsRotationsX[i] = Mathf.Repeat(wheelsRotationsX[i] + wheel.wheelCollider.rpm * Time.fixedDeltaTime / 60f, 1f);
        }
    }
}
