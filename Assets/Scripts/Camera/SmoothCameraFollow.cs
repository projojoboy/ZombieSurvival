using UnityEngine;

public class SmoothCameraFollow : MonoBehaviour {

    [SerializeField] Transform target;
    [SerializeField] float smoothSpeed = 0.125f;
    [SerializeField] Vector3 offset;

    private void FixedUpdate()
    {
        Vector3 nextPosition = target.position + offset;
        Vector3 smoothPosition = Vector3.Lerp(transform.position, nextPosition, smoothSpeed);

        transform.position = smoothPosition;

        transform.LookAt(target);
    }
}
