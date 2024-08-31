using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorMovement : MonoBehaviour
{
    public Transform doorEmpty;
    public float openAngle = 90f;
    public float speed = 2f;

    public AudioClip openSound;
    public AudioClip closeSound;

    private AudioSource audioSource;
    private Quaternion closedRotation;
    private Quaternion openRotation;
    private bool isOpen = false;
    private bool hasPlayedOpenSound;
    private bool hasPlayedCloseSound;

    void Start()
    {
        closedRotation = doorEmpty.rotation;
        openRotation = Quaternion.Euler(0, openAngle, 0) * closedRotation;
        audioSource = GetComponent<AudioSource>();

        // Reset the rotations to ensure the door starts closed
        doorEmpty.rotation = closedRotation;
    }

    void Update()
    {
        doorEmpty.rotation = Quaternion.Slerp(doorEmpty.rotation,
            isOpen ? openRotation : closedRotation, Time.deltaTime * speed);

        // Play the appropriate sound if the door state changes
        if (isOpen && !hasPlayedOpenSound)
        {
            PlaySound(openSound);
            hasPlayedOpenSound = true;
            hasPlayedCloseSound = false;
        }
        else if (!isOpen && !hasPlayedCloseSound)
        {
            PlaySound(closeSound);
            hasPlayedCloseSound = true;
            hasPlayedOpenSound = false;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isOpen = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isOpen = false;
        }
    }

    private void PlaySound(AudioClip clip)
    {
        if (clip != null && audioSource != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }
}
