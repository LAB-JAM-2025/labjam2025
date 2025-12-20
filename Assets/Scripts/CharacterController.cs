using UnityEngine;

public class CharacterControllerWithCamera : MonoBehaviour
{
    [SerializeField] float speed = 2f;
    Rigidbody rb;
    Transform feedCamera;
    GameObject character;


public float rotationSmoothSpeed = 5f;
private Quaternion targetRotation;


public float rotationSmoothSpeed = 5f;
private Quaternion targetRotation;

    [SerializeField] float cameraSpeed = 5f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        feedCamera = GameObject.Find("FeedCamera").transform;
        character = GameObject.Find("Gurl");

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

            Vector3 moveDir = Vector3.zero;
            if (Input.GetKey(KeyCode.W)) moveDir += forward;
            if (Input.GetKey(KeyCode.S)) moveDir -= forward;
            if (Input.GetKey(KeyCode.A)) moveDir -= right;
            if (Input.GetKey(KeyCode.D)) moveDir += right;

            if (moveDir != Vector3.zero)
            {
                moveDir = moveDir.normalized;
                move(moveDir, normal);
                RotateCharacter(moveDir);
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

    void move(Vector3 move, Vector3 normal)
    {
        Vector3 projection = Vector3.ProjectOnPlane(move, normal).normalized;
        rb.MovePosition(rb.position + projection * speed * Time.fixedDeltaTime);
    }
}