using UnityEngine;

public class InteractableObjectController : MonoBehaviour
{
    public Transform interactableObject;
    public KeyCode interactKey = KeyCode.E;

    private bool isInteracting = false;

    void Update()
    {
        // Check for interact key press
        if (Input.GetKeyDown(interactKey))
        {
            isInteracting = !isInteracting;
        }

        if (isInteracting)
        {
            // Rotate the interactable object using WASD keys
            float rotationSpeed = 100f;
            float horizontal = Input.GetAxis("Horizontal") * rotationSpeed * Time.deltaTime;
            float vertical = Input.GetAxis("Vertical") * rotationSpeed * Time.deltaTime;

            interactableObject.Rotate(Vector3.up, horizontal, Space.World);
            interactableObject.Rotate(Vector3.right, -vertical, Space.World);
        }
    }
}
