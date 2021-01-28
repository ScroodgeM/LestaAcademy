using UnityEngine;

public class BallControllerExtended : MonoBehaviour
{
    [SerializeField] private GameObject myGameInputGameObject;

    private IMyGameInput myGameInput;

    private void Awake()
    {
         myGameInput = myGameInputGameObject .GetComponent< IMyGameInput>();

        myGameInput.OnJumpCommand += MyGameInput_OnJumpCommand;
    }

    private void MyGameInput_OnJumpCommand()
    {
        Vector3 pos = transform.position;
        pos.y = 1f;
        transform.position = pos;
    }

    private void Update()
    {
        Vector3 pos = transform.position;
        pos.x = myGameInput.MovementCommand.x;
        pos.y = Mathf.MoveTowards(pos.y, 0f, Time.deltaTime);
        pos.z = myGameInput.MovementCommand.y;
        transform.position = pos;
    }
}
