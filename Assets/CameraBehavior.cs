using UnityEngine;

public class CameraController : MonoBehaviour
{ 
    public Transform playerTransform;
    public Vector3 offset = new Vector3(0, 5, -10);
    public float mouseSensitivity = 100f;

    float minPitch = -30f;
    float maxPitch = 60f;

    float yaw = 0f;
    float pitch = 0f;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        yaw = playerTransform.eulerAngles.y;
    }

    void LateUpdate()
    {
        yaw += Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        pitch -= Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        pitch = Mathf.Clamp(pitch, minPitch, maxPitch);

        Quaternion rotation = Quaternion.Euler(pitch, yaw, 0);
        transform.position = playerTransform.position + rotation * offset;

        transform.LookAt(playerTransform.position + Vector3.up * 1.5f);
    }
}