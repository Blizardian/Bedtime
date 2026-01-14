using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float movementSpeed;
    [SerializeField] private float originalMovementSpeed;

    public GameObject Player;
    public Transform cameraTransform;

    private float xRotation = 0f;
    [SerializeField] private float sensitivity = 80f;

    [SerializeField] private bool onGround;

    private Rigidbody rb;
    [SerializeField] private float jumpForce;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        AssignForgottenAtStart();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        Movement();
    }

    /// <summary>
    /// Includes the movement of the player and the mouse movement
    /// </summary>
    public void Movement()
    {
        transform.Translate(new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")) * movementSpeed * Time.deltaTime);

        float MouseX = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
        float MouseY = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;

        transform.Rotate(Vector3.up * MouseX);

        xRotation -= MouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        cameraTransform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        SprintLogic();

        JumpLogic();
    }

    /// <summary>
    /// Makes sure the player can run when shift is pressed
    /// </summary>
    public void SprintLogic()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            movementSpeed = originalMovementSpeed * 2f;
        }
        else
        {
            movementSpeed = originalMovementSpeed;
        }
    }

    /// <summary>
    /// When space is pressed and the player is on the ground, the player can jump
    /// </summary>
    public void JumpLogic()
    {
        if (Input.GetKeyDown(KeyCode.Space) && onGround)
        {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);

            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    /// <summary>
    /// Assigns Gameobjects, transforms in the inspectorints and sets values if they haven't been set yet.
    /// </summary>
    public void AssignForgottenAtStart()
    {
        // Automaticly sets these values
        if (originalMovementSpeed == 0)
        {
            originalMovementSpeed = 5;
        }

        if (sensitivity == 0)
        {
            sensitivity = 80;
        }

        if (jumpForce == 0)
        {
            jumpForce = 5;
        }

        // Automaticly sets these in the inspector
        if (Player == null)
        {
            Player = GameObject.Find("Player");
            Debug.Log("INFO: Player has been automatically set");
        }
        else
        {
            Debug.Log("INFO: Player has already been set manually");
        }

        if (cameraTransform == null)
        {
            cameraTransform = Player.transform.Find("Main Camera");
            Debug.Log("INFO: Camera has been automatically set");
        }
        else
        {
            Debug.Log("INFO: Camera has already been set manually");
        }
    }
    public void OnCollisionEnter(Collision collision)
    {
        onGround = true;
        Debug.Log("Player is on the ground");
    }

    public void OnCollisionExit(Collision collision)
    {
        onGround = false;
        Debug.Log("Player is NOT on the ground");
    }
}
