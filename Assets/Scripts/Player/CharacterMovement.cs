using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CapsuleCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class CharacterMovement : MonoBehaviour
{
    [Header("Movement and Collision")]
    [SerializeField] float movementSpeed = 3.0f;
    [SerializeField] int jumpHeight = 3;
    [SerializeField] LayerMask layerMaskforGroundCheck;

    [Header("Audio")]
    [SerializeField] private AudioControler audioControl;

    Transform myTransform;
    Rigidbody2D rBody;
    CapsuleCollider2D capsuleCollider;
    PlayerAnimationController animController;

    float horizontalMovement;
    MyInput input;

    PlayerState playerState;
    Vector3 velocity, platformVelocity;

    bool crouch = false;
    bool isGrounded = false;
    bool isFalling = false;

    private void Awake()
    {
        myTransform = transform;
        rBody = GetComponent<Rigidbody2D>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();

        playerState = PlayerState.Normal;
    }

    private void OnEnable()
    {
        input = GameManager.Instance.input;
        input.GamePlay.Enable();

        input.GamePlay.Jump.performed += JumpAction;
        input.GamePlay.Crouch.performed += CrouchAction;
        input.GamePlay.Crouch.canceled += CrouchAction;
    }
    private void OnDisable()
    {
        input.GamePlay.Jump.performed -= JumpAction;
        input.GamePlay.Crouch.performed -= CrouchAction;
        input.GamePlay.Crouch.canceled -= CrouchAction;
    }

    private void Start()
    {
        animController = GetComponent<PlayerAnimationController>();

        GameManager.Instance.RegisterPlayer(this.gameObject);
    }

    void Update()
    {
        horizontalMovement = input.GamePlay.Horizontal.ReadValue<float>() * movementSpeed;
    }

    private void FixedUpdate()
    {
        velocity = rBody.velocity;
        isGrounded = IsGrounded();
        animController.SetIsGrounded(isGrounded);

        Crouch();
        isFalling = IsFalling();

        switch (playerState)
        {
            case PlayerState.Normal:
                WhileNormal();
                break;
            case PlayerState.Jump:
                WhileJump();
                break;
            case PlayerState.Crouch:
                WhileCrouch();
                break;
            case PlayerState.Fall:
                WhileFalling();
                break;
        }

        rBody.velocity = velocity + platformVelocity;
    }

    public bool IsGrounded()
    {
        if (Physics2D.OverlapCircle(myTransform.position + Vector3.up * 0.4F, 0.45F, layerMaskforGroundCheck))
            return true;
        else
            return false;
    }
    private void Crouch()
    {
        if (crouch && playerState != PlayerState.Crouch && isGrounded)
        {
            Vector2 colliderSize = capsuleCollider.size;
            Vector2 offset = capsuleCollider.offset;

            colliderSize.y = 1F;
            offset.y = colliderSize.y / 2;

            capsuleCollider.size = colliderSize;
            capsuleCollider.offset = offset;

            playerState = PlayerState.Crouch;
            animController.Crouch(true);
        }
        else if (!crouch && playerState == PlayerState.Crouch)
        {
            // Check room for unCrouch
            Vector3 position = myTransform.position;
            position.y += 1.5F;
            if (Physics2D.OverlapCircle(position, 0.05F, layerMaskforGroundCheck))
                return;

            Vector2 colliderSize = capsuleCollider.size;
            Vector2 offset = capsuleCollider.offset;

            colliderSize.y = 1.5F;
            offset.y = colliderSize.y / 2;

            capsuleCollider.size = colliderSize;
            capsuleCollider.offset = offset;

            playerState = PlayerState.Normal;
            animController.Crouch(false);
        }
    }
    private bool IsFalling()
    {
        if (velocity.y < -0.05F && !isGrounded && playerState != PlayerState.Fall)
        {
            playerState = PlayerState.Fall;
            animController.Fall();
        }
        return (playerState == PlayerState.Fall);
    }

    private void JumpAction(InputAction.CallbackContext context)
    {
        if (!isGrounded) return;
        else
        {
            velocity = rBody.velocity;
            velocity.y = Mathf.Sqrt(2 * 9.8F * jumpHeight);
            rBody.velocity = velocity;

            animController.Jump();
            isGrounded = false;

            playerState = PlayerState.Jump;
            audioControl.Jump();

            rBody.MovePosition(rBody.position + (Vector2.up * 0.02F));
        }
    }
    private void CrouchAction(InputAction.CallbackContext context)
    {
        crouch = (context.phase == InputActionPhase.Performed) ? true : false;
    }


    private void WhileNormal()
    {
        velocity.x = horizontalMovement;
        animController.Movement(velocity.x);
    }
    private void WhileCrouch()
    {
        velocity.x = 0F;
        animController.RotatePlayer(input.GamePlay.Horizontal.ReadValue<float>());
    }
    private void WhileJump()
    {
        velocity.x = horizontalMovement;
        if (isGrounded)
            playerState = PlayerState.Normal;
    }
    private void WhileFalling()
    {
        if (isGrounded)
        {
            playerState = PlayerState.Normal;
            animController.Grounded();
            audioControl.Land();
        }
        velocity.x = horizontalMovement;
    }

    public void SetPlatformVelocity(Vector3 platformVelocity)
    {
        this.platformVelocity = platformVelocity;
    }

}
