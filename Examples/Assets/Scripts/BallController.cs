using UnityEngine;

public class BallController : MonoBehaviour
{
    private void Update()
    {
        Vector3 moveCommand;
        moveCommand.x = Input.GetAxis("Horizontal");
        moveCommand.y = Input.GetAxis("Jump");
        moveCommand.z = Input.GetAxis("Vertical");
        transform.position = moveCommand;
    }
}
