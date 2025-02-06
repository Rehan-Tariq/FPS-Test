using UnityEngine;

public class FPSPlayerController : MonoBehaviour
{
    public float runSpeed = 5f;
    public float walkSpeed = 2.5f;

    public float jumpHeight = 2f;
   // public Joystick joystick; // Mobile joystick

    CharacterController controller;
    float gravity = -9.81f;
    Vector3 velocity;

    public Camera playerCamera; 

    public float lookSpeed = 2f;
    public float lookXLimit = 45f;

    Vector3 moveDirection = Vector3.zero;
    float rotationX = 0f;

    public bool canMove = true;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

    }

    private void Update()
    {
        #region Movement Handles Code (WASD + Joystick)
        float moveX = /*joystick ? joystick.Horizontal :*/ Input.GetAxis("Horizontal");
        float moveZ = /*joystick ? joystick.Vertical :*/ Input.GetAxis("Vertical");
        
        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        float curSpeedX = canMove ? (isRunning ? runSpeed : walkSpeed) * moveZ : 0;
        float curSpeedY = canMove ? (isRunning ? runSpeed : walkSpeed) * moveX : 0;
        float moveDirectionY = moveDirection.y;

        moveDirection = (transform.TransformDirection(Vector3.forward ) * curSpeedX) +
                         (transform.TransformDirection(Vector3.right) * curSpeedY);
  //      Vector3 move = transform.right * moveX + transform.forward * moveZ;
//        controller.Move(move * speed * Time.deltaTime);



        #endregion


        #region Jumping Handles Code
        // Jumpsing
        if (Input.GetButtonDown("Jump") && controller.isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        // Apply gravity
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
        #endregion

        #region Handles Rotation

        controller.Move(moveDirection * Time.deltaTime);

        if (canMove)
        {

            rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
        }


        #endregion
    }
}
