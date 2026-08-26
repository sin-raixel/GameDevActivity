using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public Transform cameraTransform ;
    private Animator animator;
    public float ms = 3f;
    public float rotationSpeed = 10f;
    public float jumpHeight = 1f;
    public float grav = 1f;
    public float height = 2f;
    public float sprintSpeed = 10f;
    public float crouchHeight = 1f;
    private float normalHeight;
    private Vector3 velocity;


    
    void Start()
    {
        animator = GetComponent<Animator>();
        normalHeight = controller.height;

    }

    void Update()
    {
        MovePlayer();
    }

    private void MovePlayer()
    {
        bool grounded = controller.isGrounded;
        if (grounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float horizontal = Input.GetAxis("Horizontal");
        
        float vertical = Input.GetAxis("Vertical");

        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;

        forward.y = 0;
        right.y = 0;

        forward.Normalize();
        right.Normalize();

        Vector3 move = forward* vertical+ right* horizontal;    

        bool isMoving = move.magnitude > 0.1f;
        bool isSprinting = isMoving && Input.GetKey(KeyCode.LeftShift);
        bool isCrouching =  Input.GetKey(KeyCode.LeftControl);

        animator.SetBool("isMoving", isMoving);
        animator.SetBool("isSprinting", isSprinting);
        animator.SetBool("isJumping", !grounded);
        animator.SetBool("isCrouching", isCrouching);

        if (isMoving)
        {
            Quaternion targetRotation = Quaternion.LookRotation(move);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
        controller.Move(move.normalized * ms * Time.deltaTime);
        
        if (Input.GetButtonDown("Jump") && grounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -0.2f * grav);

        }

        if (Input.GetKey(KeyCode.LeftShift)) {
            ms = sprintSpeed;
        }
        else
        {
            ms = 3f;

        }
       if (Input.GetKey(KeyCode.LeftControl))
        {
            controller.height = crouchHeight;
            controller.center = new Vector3(0, crouchHeight / 2f, 0);
        }
        else
        {
            controller.height = normalHeight;
            controller.center = new Vector3(0, normalHeight / 2f, 0);
        }
        velocity.y += grav * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime); 

      
    }
}
