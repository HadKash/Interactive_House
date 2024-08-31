using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource ambientAudio; // Reference to the Ambient AudioSource
    public AudioSource jazzMusic; // Reference to the Jazz Music AudioSource

    private float ambientVolume; // Store the original ambient volume

    void Start()
    {
        // Store the initial volume of the ambient audio
        ambientVolume = ambientAudio.volume;
        // Ensure jazz music is not playing initially
        jazzMusic.Stop();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Reduce the ambient audio volume
            ambientAudio.volume = 0;
            // Play the jazz music
            jazzMusic.Play();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Restore the ambient audio volume
            ambientAudio.volume = ambientVolume;
            // Stop the jazz music
            jazzMusic.Stop();
        }
    }
}
