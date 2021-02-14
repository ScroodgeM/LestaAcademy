using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Animator characterAnimator;
    [SerializeField] private float rotationSpeed;

    private void Update()
    {
        if (Input.GetAxis("Jump") > 0)
        {
            // stop moving
            characterAnimator.SetFloat("Speed_f", 0f);
            // play jump animation
            characterAnimator.SetBool("Jump_b", true);
            return;
        }

        // disable jump animation, if enabled
        characterAnimator.SetBool("Jump_b", false);

        if (Input.GetAxis("Fire1") > 0)
        {
            // stop moving
            characterAnimator.SetFloat("Speed_f", 0f);
            // play fire animation
            characterAnimator.SetInteger("Animation_int", 10);
            return;
        }

        // disable fire animation, if enabled
        characterAnimator.SetInteger("Animation_int", 0);

        Vector3 moveCommand = Vector3.zero;
        moveCommand.x = Input.GetAxis("Horizontal");
        moveCommand.z = Input.GetAxis("Vertical");

        if (moveCommand != Vector3.zero)
        {
            characterAnimator.SetFloat("Speed_f", 1f);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(moveCommand), Time.deltaTime * rotationSpeed);
        }
        else
        {
            characterAnimator.SetFloat("Speed_f", 0f);
        }
    }
}
