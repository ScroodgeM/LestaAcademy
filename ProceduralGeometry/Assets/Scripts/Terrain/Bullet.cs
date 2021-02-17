using Battlegrounds;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private DeformationModes mode;
    [SerializeField] private float boomPower;
    [SerializeField] private float startSpeed;

    private void Start()
    {
        GetComponent<Rigidbody>().AddForce(transform.forward * startSpeed, ForceMode.VelocityChange);
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
        LowPolyTerrain terrain = collision.gameObject.GetComponentInParent<LowPolyTerrain>();
        if (terrain != null)
        {
            terrain.Deformate(mode, collision.contacts[0].point, boomPower, boomPower);
            Destroy(gameObject);
        }
    }
}
