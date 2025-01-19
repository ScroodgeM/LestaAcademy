using Battlegrounds;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private DeformationModes mode;
    [SerializeField] private float boomPower;

    public void Init(Vector3 initialSpeed)
    {
        GetComponent<Rigidbody>().linearVelocity = initialSpeed;
    }

    private void Update()
    {
        // in case we falling to nowhere
        if (transform.position.y < -100)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        LowPolyTerrain_Deformations terrain = collision.gameObject.GetComponentInParent<LowPolyTerrain_Deformations>();
        if (terrain != null)
        {
            terrain.Deformate(mode, collision.contacts[0].point, boomPower, boomPower);
            Destroy(gameObject);
        }
    }
}
