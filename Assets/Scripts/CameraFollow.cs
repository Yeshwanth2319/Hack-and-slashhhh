using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;

    public float distance = 5f;
    public float height = 2.5f;
    public float sensitivity = 3f;

    float mouseX;
    float mouseY;

    [Header("Camera Shake")]
    public float shakeAmount = 0.2f;
    public float shakeDuration = 0.2f;

    float shakeTimer = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        if (player != null)
        {
            mouseX = player.eulerAngles.y;
        }
    }

    void LateUpdate()
    {
        if (player == null)
            return;

        mouseX += Input.GetAxis("Mouse X") * sensitivity;
        mouseY -= Input.GetAxis("Mouse Y") * sensitivity;

        mouseY = Mathf.Clamp(mouseY, -30f, 60f);

        Quaternion rotation =
            Quaternion.Euler(mouseY, mouseX, 0f);

        Vector3 position =
            player.position +
            Vector3.up * height -
            rotation * Vector3.forward * distance;

        transform.position = position;

        transform.LookAt(
            player.position + Vector3.up * 1.5f
        );

        // Camera Shake
        if (shakeTimer > 0)
        {
            transform.position += Random.insideUnitSphere * shakeAmount;

            shakeTimer -= Time.deltaTime;
        }
    }

    public void ShakeCamera()
    {
        shakeTimer = shakeDuration;
    }
}