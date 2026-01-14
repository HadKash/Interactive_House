using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawerMovement : MonoBehaviour
{
    public float openPositionX = -8.5f; // Target position when opened
    public float closedPositionX = -7.6f; // Original position
    public float moveSpeed = 2f; // Speed of the sliding animation

    public AudioClip openSound;
    public AudioClip closeSound;

    private bool isOpen = false; // Track the drawer state
    private Vector3 targetPosition; // The position to move towards
    private AudioSource audioSource;

    void Start()
    {
        // Initialize the target position to the closed position
        targetPosition = new Vector3(closedPositionX, transform.position.y, transform.position.z);
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        // Smoothly move the drawer towards the target position
        if (transform.position.x != targetPosition.x)
        {
            Vector3 newPosition = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
            transform.position = newPosition;
        }
    }

    void OnMouseDown()
    {
        // Toggle between open and closed states
        if (isOpen)
        {
            targetPosition = new Vector3(closedPositionX, transform.position.y, transform.position.z);
            PlaySound(closeSound);
        }
        else
        {
            targetPosition = new Vector3(openPositionX, transform.position.y, transform.position.z);
            PlaySound(openSound);
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
