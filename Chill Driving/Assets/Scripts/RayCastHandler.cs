using System;
using UnityEngine;

public class RayCastHandler : MonoBehaviour
{
    private LayerMask _groundLayer;
    [SerializeField] private float rayDistance;

    private void Start()
    {
        _groundLayer = LayerMask.GetMask("Ground");
    }

    void FixedUpdate()
    {
        SurfaceAligment();
    }
    private void SurfaceAligment()
    {
        Ray ray = new Ray(transform.position, Vector3.down);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, rayDistance, _groundLayer))
        {
            Vector3 targetNormal = hit.normal;
            Quaternion targetRotation = Quaternion.FromToRotation(transform.up, targetNormal) * transform.rotation;
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 10 * Time.deltaTime);
        }
    }

    private void OnDrawGizmos()
    {
        Debug.DrawRay(transform.position, Vector3.down * rayDistance, Color.red);
    }
}
