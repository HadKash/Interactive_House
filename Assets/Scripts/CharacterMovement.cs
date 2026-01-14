using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public float speed = 5.0f;
    public float turnSpeed = 100.0f;
    private CharacterController controller;
    private OVRCameraRig cameraRig;
    private bool isInteracting = false;
    public Transform interactableObject; // 拖放要旋转的物体
    public float interactTurnSpeed = 50.0f;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        cameraRig = GetComponentInChildren<OVRCameraRig>();

        if (cameraRig == null)
        {
            Debug.LogError("OVRCameraRig not found as a child of the player object.");
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            isInteracting = !isInteracting; // 切换互动模式
        }

        if (isInteracting)
        {
            InteractWithObject();
        }
        else
        {
            MoveCharacter();
        }
    }

    void MoveCharacter()
    {
        float horizontal = Input.GetAxis("Horizontal") * turnSpeed * Time.deltaTime;
        float vertical = Input.GetAxis("Vertical") * speed * Time.deltaTime;

        Vector3 move = transform.forward * vertical;
        controller.Move(move * Time.deltaTime);
        transform.Rotate(Vector3.up, horizontal);
    }

    void InteractWithObject()
    {
        if (interactableObject != null)
        {
            float horizontal = Input.GetAxis("Horizontal") * interactTurnSpeed * Time.deltaTime;
            interactableObject.Rotate(Vector3.up, horizontal);
        }
    }
}


