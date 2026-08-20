using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public Transform cameraTransform ;

    public float ms = 10f;
    public float rotationSpeed = 10f;
    public float jumpHeight = 10f;
    public float grav = -15f;
    public float height = 2f;

    private Vector3 velocity;


    
    void Start()
    {
        
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
        
        if (move.magnitude > 0.1f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(move);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
        controller.Move(move.normalized * ms * Time.deltaTime);
        
        if (Input.GetButtonDown("Jump") && grounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * grav);

        }

        if (Input.GetKey(KeyCode.LeftShift)) {
            ms = 20f;
        }
        else
        {
            ms = 10f;

        }
        velocity.y += grav * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime); 

        if (Input.GetKey(KeyCode.LeftControl))
        {
            controller.height = 1f;
            controller.center = new Vector3(0,0.5f,0);
        }
        else
        {
            controller.height = 2f;
            controller.center = new Vector3(0,0,0);
        }
    }
}
