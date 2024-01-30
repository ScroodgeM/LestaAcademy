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
    [SerializeField] private Transform gunTransform;
    [SerializeField] private float gunFireCooldown;
    [SerializeField] private Bullet bullet1;
    [SerializeField] private Bullet bullet2;

    private float[] wheelsRotationsX;
    private float nextGunFirePossibleTime;

    private void Awake()
    {
        GetComponent<Rigidbody>().centerOfMass = Vector3.zero;

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

            wheel.graphics.localRotation = Quaternion.AngleAxis(wc.steerAngle, Vector3.up) * Quaternion.AngleAxis(wheelsRotationsX[i] * 360f, Vector3.right);
        }

        if (Input.GetAxis("Fire1") > 0.5f)
        {
            FireWithBullet(bullet1);
        }

        if (Input.GetAxis("Fire2") > 0.5f)
        {
            FireWithBullet(bullet2);
        }
    }

    private void FireWithBullet(Bullet bullet)
    {
        if (Time.time > nextGunFirePossibleTime)
        {
            nextGunFirePossibleTime = Time.time + gunFireCooldown;
            Instantiate(bullet, gunTransform.position, gunTransform.rotation);
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
