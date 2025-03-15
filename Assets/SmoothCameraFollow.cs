using UnityEngine;

public class SmoothCameraFollow : MonoBehaviour
{
    public Vector3 offset;

    public float damping;
    
    public Transform target;
    
    private Vector3 vel = Vector3.zero;

    private void Update()
    {
        var targetPos = target.position + offset;
        targetPos.z = transform.position.z;
        transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref vel, damping);
    }
}
