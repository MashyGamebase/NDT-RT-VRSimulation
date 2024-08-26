using UnityEngine;
using UnityEngine.InputSystem;
using MyBox;

public class PlayerFPSController : MonoBehaviour
{
    [SerializeField]
    CharacterController controller;

    [SerializeField]
    Camera playerCamera;

    [SerializeField] private float _moveSpeed = 6.0f;
    [SerializeField] private float _jumpHeight = 5f;

    [SerializeField]
    private float gravity = -9.81f;

    private Vector3 velocity;
    private Vector2 moveInput;

    [SerializeField] private bool isGrounded;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        //Check if we're grounded
        isGrounded = controller.isGrounded;
        if(isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        // Movement based on input
        Vector3 move = playerCamera.transform.right * moveInput.x + playerCamera.transform.forward * moveInput.y;
        move.y = 0;
        controller.Move(move * _moveSpeed * Time.deltaTime);

        // Apply gravity
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    #region CALLBACK_CONTEXT
    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if(context.performed && isGrounded)
        {
            velocity.y = Mathf.Sqrt(_jumpHeight * -2f * gravity);
        }
    }
    #endregion
}