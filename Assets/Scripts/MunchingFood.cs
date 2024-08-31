using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MunchingFood : MonoBehaviour
{
    public AudioClip munchingSound; // Sound to play when the object is clicked
    public GameObject[] foodItems; // Array of food items to disappear

    private AudioSource audioSource; // AudioSource to play sounds

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        if (foodItems == null || foodItems.Length == 0)
        {
            Debug.LogError("No food items assigned to the script.");
        }
    }

    void OnMouseDown()
    {
        // Play munching sound
        if (munchingSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(munchingSound);
        }

        // Hide the food items
        foreach (GameObject foodItem in foodItems)
        {
            if (foodItem != null)
            {
                Renderer objectRenderer = foodItem.GetComponent<Renderer>();
                Collider objectCollider = foodItem.GetComponent<Collider>();

                if (objectRenderer != null)
                {
                    objectRenderer.enabled = false;
                }
                if (objectCollider != null)
                {
                    objectCollider.enabled = false;
                }
            }
        }

        // Optionally, destroy the parent object after the sound has played
        Destroy(gameObject, munchingSound.length);
    }
}
