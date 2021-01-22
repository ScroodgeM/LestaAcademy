using UnityEngine;

public class UnitMover : MonoBehaviour
{
    [SerializeField] private Alliance alliance;
    [SerializeField] private Vector3 moveDirection;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float goalPositionX;

    void Update()
    {
        transform.position += Time.deltaTime * moveDirection.normalized * moveSpeed;
    }

    private void OnTriggerStay(Collider other)
    {
        GameObject otherObject = other.gameObject;

        UnitMover otherObjectAsUnitMover = otherObject.GetComponent<UnitMover>();

        if (otherObjectAsUnitMover != null)
        {
            // we are in collision with other unit

            if (otherObjectAsUnitMover.alliance == this.alliance)
            {
                //ally - move away to avoid colliding

                transform.position += (transform.position - other.transform.position).normalized * Time.deltaTime * moveSpeed;
            }
            else
            {
                //enemy - die (and we except that enemy will die too)

                Destroy(gameObject);
            }
        }
    }
}
