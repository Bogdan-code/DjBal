using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayerMotor : MonoBehaviour
{
    // Start is called before the first frame update

    private PlayerInput playerInput;
    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool isGrounded, isSprinting;
    public float gravity = -9.8f;
    public float jumpHeight = 1.5f;
    public float sprintSpeed = 7f;
    public float walkSpeed = 5f;
    private float moveSpeed;

    private void SprintPressed()
    {
        if (isGrounded)
        {
            isSprinting = true;
            moveSpeed = sprintSpeed;
        }
    }
    private void SprintReleased()
    {
        if (isGrounded)
        {
            isSprinting = false;
            moveSpeed = walkSpeed;
        }
    }

    void Start()
    {
        controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        playerInput = new PlayerInput();
        OnEnable();
        playerInput.OnFoot.Sprint.performed += x => SprintPressed();
        playerInput.OnFoot.Sprint.canceled += x => SprintReleased();

        moveSpeed = walkSpeed;

    }

    private void OnEnable()
    {
        playerInput.Enable();
    }
    private void OnDisable()
    {
        playerInput.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = controller.isGrounded;
    }
    public void ProcessMove(Vector2 input)
    {


        Vector3 moveDirection = Vector3.zero;
        moveDirection.x = input.x;
        moveDirection.z = input.y;

        controller.Move(transform.TransformDirection(moveDirection) * moveSpeed * Time.deltaTime);

        if (isGrounded && playerVelocity.y < 0)
            playerVelocity.y = -2f;
        playerVelocity.y += gravity * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);


    }
    public void Jump()
    {
        if (isGrounded)
        {
            playerVelocity.y = Mathf.Sqrt(jumpHeight * -3f * gravity);
        }
    }

    public void Sprint(Button input)
    {

    }
}
