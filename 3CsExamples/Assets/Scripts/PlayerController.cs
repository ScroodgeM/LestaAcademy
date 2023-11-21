
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private Animator characterAnimator;
    [SerializeField] private float rotationSpeed;

    private Vector2 moveCommand = Vector2.zero;
    private bool jumping = false;
    private bool firing = false;

    private void Awake()
    {
        playerInput.onActionTriggered += OnPlayerInputActionTriggered;
    }

    private void Update()
    {
        if (moveCommand.sqrMagnitude > 0.01f)
        {
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(moveCommand.x, 0, moveCommand.y));

            transform.rotation = Quaternion.RotateTowards(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
        }
    }

    private void OnPlayerInputActionTriggered(InputAction.CallbackContext context)
    {
        switch (context.action.name)
        {
            case "Move":
                moveCommand = context.action.ReadValue<Vector2>();
                UpdateAnimation();
                break;

            case "Jump":
                switch (context.action.phase)
                {
                    case InputActionPhase.Started:
                        jumping = true;
                        UpdateAnimation();
                        break;
                    case InputActionPhase.Canceled:
                        jumping = false;
                        UpdateAnimation();
                        break;
                }
                break;

            case "Fire":
                switch (context.action.phase)
                {
                    case InputActionPhase.Started:
                        firing = true;
                        UpdateAnimation();
                        break;
                    case InputActionPhase.Canceled:
                        firing = false;
                        UpdateAnimation();
                        break;
                }
                break;
        }
    }

    private void UpdateAnimation()
    {
        if (jumping == true)
        {
            // stop moving
            characterAnimator.SetFloat("Speed_f", 0f);
            // play jump animation
            characterAnimator.SetBool("Jump_b", true);
            return;
        }

        // disable jump animation, if enabled
        characterAnimator.SetBool("Jump_b", false);

        if (firing == true)
        {
            // stop moving
            characterAnimator.SetFloat("Speed_f", 0f);
            // play fire animation
            characterAnimator.SetInteger("Animation_int", 10);
            return;
        }

        // disable fire animation, if enabled
        characterAnimator.SetInteger("Animation_int", 0);

        // set speed based on move command
        characterAnimator.SetFloat("Speed_f", moveCommand.magnitude);
    }
}
