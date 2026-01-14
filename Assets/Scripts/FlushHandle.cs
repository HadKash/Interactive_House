using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlushHandle : MonoBehaviour
{
    public float targetZRotation = -39f; // Target z rotation for the handle
    public float moveSpeed = 2f; // Speed of the handle movement
    public AudioClip flushSound; // Sound to play when flushing

    private bool isFlushing = false; // Track the flush state
    private Quaternion initialRotation; // Original rotation
    private Quaternion targetRotation; // Rotation when flushed
    private AudioSource audioSource; // AudioSource to play sounds

    void Start()
    {
        // Initialize the initialRotation and targetRotation
        initialRotation = transform.localRotation;
        targetRotation = Quaternion.Euler(transform.localRotation.eulerAngles.x, transform.localRotation.eulerAngles.y, targetZRotation);
        audioSource = GetComponent<AudioSource>();

        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    void Update()
    {
        // Smoothly rotate the handle towards the target rotation
        Quaternion currentTargetRotation = isFlushing ? targetRotation : initialRotation;
        transform.localRotation = Quaternion.RotateTowards(transform.localRotation, currentTargetRotation, moveSpeed * Time.deltaTime * 100);

        // If the handle has reached the target rotation, reverse the state after a short delay
        if (isFlushing && Quaternion.Angle(transform.localRotation, targetRotation) < 0.1f)
        {
            StartCoroutine(ReverseFlush());
        }
    }

    void OnMouseDown()
    {
        if (!isFlushing)
        {
            isFlushing = true; // Start the flush sequence
            PlaySound(flushSound); // Play flush sound
        }
    }

    private void PlaySound(AudioClip clip)
    {
        if (clip != null && audioSource != null)
        {
            audioSource.PlayOneShot(clip); // Play the audio clip
        }
    }

    private IEnumerator ReverseFlush()
    {
        yield return new WaitForSeconds(1f); // Wait for 1 second before reversing
        isFlushing = false; // Reverse the flush sequence
    }
}
