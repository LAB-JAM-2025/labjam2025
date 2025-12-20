using UnityEngine;

public class Adjuster : MonoBehaviour
{
    Rigidbody rb;
    public float rotationSpeed;
    GameObject sphere;
    public float distanceFromSphere;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        sphere = GameObject.FindGameObjectWithTag("Sphere");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        RaycastHit ray;
        Vector3 pos = transform.position;
        Vector3 toSphere = sphere.transform.position - pos;

        if (Physics.Raycast(pos, toSphere, out ray, Mathf.Infinity, LayerMask.GetMask("Sphere")))
        {
            Vector3 normal = ray.normal;
            adjustRotationBySphere(normal);

            if (gameObject.tag == "Player") // DISABLED
            {
                float yaw = Input.GetAxis("Mouse X") * rotationSpeed;
                adjustRotation(normal, yaw);
            }
            else if(gameObject.tag == "Enemy")
            {
                //float yaw = Vector3.Angle(transform.position, player.transform.position);
                //adjustRotation(normal, yaw);
            }

            // Ensure object stays on sphere surface
            adjustPositionBySurface(toSphere);
        }
    }


    void adjustRotationBySphere(Vector3 normal)
    {
        Vector3 up = transform.up;
        Quaternion targetRotation = Quaternion.FromToRotation(up, normal) * transform.rotation;
        rb.MoveRotation(targetRotation);
    }

    void adjustRotation(Vector3 normal, float yaw) /// DO NOT DELETE
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

    void adjustPositionBySurface(Vector3 toSphere)
    {
        if (!Physics.Raycast(rb.position, toSphere, out RaycastHit surfaceHit, distanceFromSphere, LayerMask.GetMask("Sphere")))
        {
            if (Physics.Raycast(rb.position, toSphere, out RaycastHit surface, Mathf.Infinity, LayerMask.GetMask("Sphere")))
            {
                Vector3 point = surface.point;
                Vector3 new_pos = point + distanceFromSphere * surface.normal;
                rb.position = new_pos;
            }
        }
    }
}
