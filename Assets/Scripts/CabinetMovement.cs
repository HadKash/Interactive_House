using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CabinetMovement : MonoBehaviour
{
    public Transform door; // The Transform of the door
    public float openAngle = 90f; // Angle to rotate the door
    public float moveSpeed = 2f; // Speed of the door movement

    public AudioClip openSound; // Sound to play when opening
    public AudioClip closeSound; // Sound to play when closing

    private bool isOpen = false; // Track the door state
    private Vector3 closedRotation; // Original rotation
    private Quaternion openRotation; // Rotation when open
    private AudioSource audioSource; // AudioSource to play sounds

    void Start()
    {
        // Initialize the closedRotation and openRotation
        closedRotation = door.localRotation.eulerAngles;
        openRotation = Quaternion.Euler(closedRotation.x, closedRotation.y + openAngle, closedRotation.z);
        audioSource = GetComponent<AudioSource>(); // Get the AudioSource component

        if (audioSource == null)
        {
            Debug.LogError("AudioSource component not found on the hinge object.");
        }
    }

    void Update()
    {
        // Smoothly rotate the door towards the target rotation
        Quaternion targetRotation = isOpen ? openRotation : Quaternion.Euler(closedRotation);
        door.localRotation = Quaternion.RotateTowards(door.localRotation, targetRotation, moveSpeed * Time.deltaTime * 100);
    }

    void OnMouseDown()
    {
        // Toggle between open and closed states
        if (isOpen)
        {
            PlaySound(closeSound); // Play close sound
        }
        else
        {
            PlaySound(openSound); // Play open sound
        }

        isOpen = !isOpen; // Toggle the state
    }

    private void PlaySound(AudioClip clip)
    {
        if (clip != null && audioSource != null)
        {
            audioSource.PlayOneShot(clip); // Play the audio clip
        }
    }
}
