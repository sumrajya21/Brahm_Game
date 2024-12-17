using UnityEngine;

public class ThirdPersonCameraController : MonoBehaviour
{
    public Transform playerTransform;
    public Vector3 offset;
    public float smoothFactor = 0.5f;

    void LateUpdate()
    {
        Vector3 newPosition = playerTransform.position + offset;
        transform.position = Vector3.Lerp(transform.position, newPosition, smoothFactor);
        transform.LookAt(playerTransform);
    }
}
