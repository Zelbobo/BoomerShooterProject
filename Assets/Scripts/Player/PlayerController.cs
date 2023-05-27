using UnityEngine;

[RequireComponent(typeof(Rigidbody),
    typeof(CapsuleCollider))]
public class PlayerController : MoveController
{
    [SerializeField] private float crouchSpeed;
    [SerializeField] private float forceJump;

    [Header("Ground detection")] 
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float radius;
    [SerializeField] private LayerMask groundLayer;

    [Header("Crouch")]
    [SerializeField] private float crouchingTime;
    [SerializeField] private float defaultHeight;
    [SerializeField] private float crouchHeight;

    [Header("Inputs")]
    [SerializeField] private Joystick moveJoystick;

    #region [PrivateVars]

    private float currentSpeed;

    private float moveVertical;
    private float moveHorizontal;
    private Vector3 move;

    private bool isCrouch;

    private CapsuleCollider _collider;
    private Rigidbody _rigidbody;

    #endregion

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _collider = GetComponent<CapsuleCollider>();
    }

    private void Update()
    {
        Inputs();

        _collider.height = Mathf.LerpUnclamped(_collider.height, isCrouch ? crouchHeight : defaultHeight, crouchingTime * Time.deltaTime);
        currentSpeed = isCrouch ? crouchSpeed : defaultSpeed;
    }

    private void FixedUpdate()
    {
        Move();
    }

    protected override void Move()
    {
        move = transform.forward * moveVertical + transform.right * moveHorizontal;

        _rigidbody.MovePosition(transform.position + move * currentSpeed * Time.deltaTime);
    }

    public void Jump()
    {
        if (!IsGrounded())
        {
            return;
        }

        _rigidbody.AddForce(forceJump * Vector3.up, ForceMode.Impulse);
    }

    public void Crouch()
    {
        isCrouch = !isCrouch;
    }

    private void Inputs()
    {
        moveVertical = moveJoystick.Vertical;
        moveHorizontal = moveJoystick.Horizontal;
    }

    private bool IsGrounded()
    {
        return Physics.CheckSphere(groundCheck.position, radius, groundLayer);
    }
}
