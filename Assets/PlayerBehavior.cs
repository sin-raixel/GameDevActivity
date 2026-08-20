using UnityEngine;
using System; 
public class PlayerBehavior : MonoBehaviour


{
     Rigidbody rb; 
    public float speed = 5f;
    public float jumpForce = 10f;
    public bool isGrounded = true;
    public float rotationSpeed = 10f;
    public Transform cameraTransform;
    public int eggcount;
       void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal"); 
        float vertical = Input.GetAxis("Vertical");

        Vector3 forward = cameraTransform.forward;
        forward.y = 0;
        forward.Normalize();

        Vector3 right = cameraTransform.right;
        right.y = 0;
        right.Normalize();

        Vector3 direction = (forward * vertical + right * horizontal).normalized;

        if (direction.magnitude >= 0.1f)
        {
            rb.MovePosition(rb.position + direction * speed * Time.deltaTime);

            Quaternion targetRotation = Quaternion.LookRotation(direction);
            rb.rotation = Quaternion.Slerp(rb.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        } else
        {
            rb.linearVelocity = new Vector3(0, rb.linearVelocity.y, 0);
        }
         
       if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }
        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = 20f;
        } else
        {
                        speed = 5f;

        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }

        if (collision.gameObject.CompareTag("Egg"))
        {
            eggcount++;
            Debug.Log("Eggs collected: " + eggcount);
            Destroy(collision.gameObject);
            
        }
    }
}