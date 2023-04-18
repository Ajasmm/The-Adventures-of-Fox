using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    Transform myTransform;
    Animator animator;
    CharacterMovement characterMovement;

    int movement_Hash;
    int crouch_Hash;
    int jump_Hash;
    int fall_Hash;
    int grounded_Hash;

    float prevMovement;

    bool isGrounded = false;

    private void Awake()
    {
        myTransform = GetComponent<Transform>();
        animator = GetComponent<Animator>();
        characterMovement = GetComponent<CharacterMovement>();

        movement_Hash = Animator.StringToHash("Movement");
        crouch_Hash = Animator.StringToHash("Crouch");
        jump_Hash = Animator.StringToHash("Jump");
        fall_Hash = Animator.StringToHash("Fall");
        grounded_Hash = Animator.StringToHash("Grounded");
    }

    public void Movement(float value)
    {
        RotatePlayer(value);

        value = Mathf.Abs(value);

        animator.SetFloat(movement_Hash, value);
    }

    public void RotatePlayer(float direction)
    {
        if (direction < 0 && prevMovement! < 0)
        {
            Vector3 scale = myTransform.localScale;
            scale.x = -1;
            myTransform.localScale = scale;
        }
        else if (direction > 0 && prevMovement! > 0)
        {
            Vector3 scale = myTransform.localScale;
            scale.x = 1;
            myTransform.localScale = scale;
        }
        prevMovement = direction;
    }

    public void Crouch(bool state)
    {
        animator.SetBool(crouch_Hash, state);
    }
    public void Jump()
    {
        animator.SetTrigger(jump_Hash);
    }
    public void Fall()
    {
        animator.SetTrigger(fall_Hash);
    }
    public void Grounded()
    {
        animator.SetTrigger(grounded_Hash);
    }

    public void SetIsGrounded(bool value)
    {
        if (value && isGrounded == false) animator.SetTrigger(grounded_Hash);
        isGrounded = value;
    }
}
