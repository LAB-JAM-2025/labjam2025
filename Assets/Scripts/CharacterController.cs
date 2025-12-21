using UnityEngine;

public class CharacterControllerWithCamera : MonoBehaviour
{
    public float speed;
    public float mouse_sensitivity;
    Rigidbody rb;
    Transform feedCamera;
    GameObject character;
    Animator animator;

    public float rotationSmoothSpeed = 5f;
    private Quaternion targetRotation;

    public float cameraSpeed = 5f;
    public float cameraMouseSensitivity = 2f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        feedCamera = GameObject.Find("FeedCamera").transform;
        character = GameObject.Find("Gurl");
        animator = character.GetComponent<Animator>();

        targetRotation = character.transform.rotation;
        
    }

    void FixedUpdate()
    {
        MoveCharacter();
        MoveFeedCameraXZ();
        //smooth transition between rotations
        character.transform.rotation = Quaternion.Slerp(character.transform.rotation,  targetRotation, rotationSmoothSpeed * Time.fixedDeltaTime);

    }


    void MoveCharacter()
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

            Vector3 moveDir = Vector3.zero;
            if (Input.GetKey(KeyCode.W)) moveDir += forward;
            if (Input.GetKey(KeyCode.S)) moveDir -= forward;
            if (Input.GetKey(KeyCode.A)) moveDir -= right;
            if (Input.GetKey(KeyCode.D)) moveDir += right;

            if (moveDir != Vector3.zero)
            {
                animator.SetBool("Running", true);
                moveDir = moveDir.normalized;
                move(moveDir, normal);
                RotateCharacter(moveDir);


            }
            else
            {
                animator.SetBool("Running", false);
            }
            

            // Ensure character stays on sphere surface
            if (Physics.Raycast(rb.position, -transform.up, out RaycastHit surfaceHit, Mathf.Infinity, LayerMask.GetMask("Sphere")))
            {
                rb.position = surfaceHit.point;
            }
        }
    }
    void RotateCharacter(Vector3 normal)
    {
        targetRotation = Quaternion.LookRotation(normal, transform.up);
        
    }

    void MoveFeedCameraXZ()
    {
        if (feedCamera == null) return;

        Vector3 camMove = Vector3.zero;

        // Use same WASD input as player
        if (Input.GetKey(KeyCode.W)) camMove += Vector3.forward;
        if (Input.GetKey(KeyCode.S)) camMove += Vector3.back;
        if (Input.GetKey(KeyCode.A)) camMove += Vector3.left;
        if (Input.GetKey(KeyCode.D)) camMove += Vector3.right;

        // Apply speed and deltaTime
        camMove = camMove.normalized * cameraSpeed * Time.fixedDeltaTime;

        // Move camera in world XZ plane
        feedCamera.position += new Vector3(camMove.x, 0, -camMove.z);//it was easier to invert z here then rotate everything lmao
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
        rb.MovePosition(rb.position + projection * speed * Time.fixedDeltaTime);
    }
}

