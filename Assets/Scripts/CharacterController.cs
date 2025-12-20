using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.GraphicsBuffer;
using static UnityEngine.UI.Image;

public class CharacterController : MonoBehaviour
{
    /// MAKE A SEPARATE LAYER FOR SPHERE!!!!!
    public float speed;
    public float mouse_sensitivity;
    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        
    }

    void FixedUpdate()
    {
        RaycastHit ray;
        Vector3 forward = transform.forward;
        Vector3 up = transform.up;
        Vector3 right = transform.right;
        Vector3 pos = transform.position;

        if (Physics.Raycast(pos, -up, out ray, Mathf.Infinity, LayerMask.GetMask("Sphere")))
        {
            Vector3 normal = ray.normal;
            adjustRotationBySphere(normal);

            float yaw = Input.GetAxis("Mouse X") * mouse_sensitivity;
            adjustRotation(normal, yaw);

            Vector3 moveDir;
            if (Input.GetKey(KeyCode.W))
            {
                moveDir = forward;
                move(moveDir, normal);
            }

            if (Input.GetKey(KeyCode.S))
            {
                moveDir = -forward;
                move(moveDir, normal);
            }

            if (Input.GetKey(KeyCode.A))
            {
                moveDir = -right;
                move(moveDir, normal);
            }

            if (Input.GetKey(KeyCode.D))
            {
                moveDir = right;
                move(moveDir, normal);
            }
        }
    }

    void adjustRotationBySphere(Vector3 normal)
    {
        Vector3 up = transform.up;
        Quaternion targetRotation = Quaternion.FromToRotation(up, normal) * transform.rotation;
        rb.MoveRotation(targetRotation);
    }

    void adjustRotation(Vector3 normal, float yaw)
    {
        Vector3 forward = transform.forward;
        Quaternion turnRotation = Quaternion.AngleAxis(yaw, normal);
        Vector3 newForward = turnRotation * forward;
        Vector3 projectedForward = Vector3.ProjectOnPlane(newForward, normal).normalized;
        if (projectedForward != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(projectedForward, normal);
            rb.MoveRotation(targetRotation);
        }
    }

    void move(Vector3 move, Vector3 normal)
    {
        Vector3 projection = Vector3.ProjectOnPlane(move, normal).normalized;
        rb.MovePosition(rb.position + projection * speed * Time.deltaTime);
    }
}
