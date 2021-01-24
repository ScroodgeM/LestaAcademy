using UnityEngine;

public class AnimatonWrapper : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private int Animation_int;

    private void Start()
    {
        animator.SetInteger(nameof(Animation_int), Animation_int);
    }
}
