using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnobController : MonoBehaviour
{
    public Transform knob; // The Transform of the knob
    public Transform flames; // The Transform of the flames
    public Renderer[] eggsRenderers; // Array of Renderers for the eggs
    public float rotationAngle = 45f; // Starting angle of the knob
    public float rotationSpeed = 2f; // Speed of rotation
    public float flameScaleSpeed = 2f; // Speed of flame scaling
    public float flameScaleMax = 1f; // Maximum scale of flames
    public float flameScaleMin = 0f; // Minimum scale of flames
    public float maxOpacity = 1f; // Maximum opacity of eggs

    public AudioClip flameSound; // Audio clip for flames turning on
    public AudioClip sizzlingSound; // Audio clip for sizzling oil

    private bool isRotated = false; // Track the knob's rotation state
    private Vector3 originalScale; // Original scale of the flames
    private Color[] eggsColors; // Array to store colors of the eggs
    private AudioSource audioSource; // AudioSource to play sounds

    void Start()
    {
        // Set initial values
        knob.localRotation = Quaternion.Euler(rotationAngle, 0, 0);
        originalScale = flames.localScale;
        eggsColors = new Color[eggsRenderers.Length];

        // Initialize the eggs' colors with zero opacity
        for (int i = 0; i < eggsRenderers.Length; i++)
        {
            eggsColors[i] = eggsRenderers[i].material.color;
            eggsColors[i].a = 0; // Set initial opacity to 0
            eggsRenderers[i].material.color = eggsColors[i];
        }

        flames.localScale = new Vector3(flameScaleMin, flameScaleMin, flameScaleMin);

        // Get the AudioSource component
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        // Handle knob rotation and flame scaling
        if (isRotated)
        {
            knob.localRotation = Quaternion.RotateTowards(knob.localRotation, Quaternion.Euler(0, 0, 0), rotationSpeed * Time.deltaTime * 100);
            flames.localScale = Vector3.MoveTowards(flames.localScale, new Vector3(flameScaleMax, flameScaleMax, flameScaleMax), flameScaleSpeed * Time.deltaTime);
        }
        else
        {
            knob.localRotation = Quaternion.RotateTowards(knob.localRotation, Quaternion.Euler(rotationAngle, 0, 0), rotationSpeed * Time.deltaTime * 100);
            flames.localScale = Vector3.MoveTowards(flames.localScale, new Vector3(flameScaleMin, flameScaleMin, flameScaleMin), flameScaleSpeed * Time.deltaTime);

            // Stop all sounds when the knob is turned off
            if (audioSource.isPlaying)
            {
                audioSource.Stop();
            }

            // Reset eggs opacity to 0
            for (int i = 0; i < eggsRenderers.Length; i++)
            {
                Color color = eggsRenderers[i].material.color;
                color.a = 0;
                eggsRenderers[i].material.color = color;
            }
        }
    }

    void OnMouseDown()
    {
        isRotated = !isRotated; // Toggle the rotation state

        if (isRotated)
        {
            // Start the flame sound and then the sizzling sound, and handle the egg opacity transition
            StartCoroutine(PlayFlameAndSizzleSounds());
        }
        else
        {
            // Stop the sizzling sound when the knob is turned off
            if (audioSource.clip == sizzlingSound && audioSource.isPlaying)
            {
                audioSource.Stop();
            }
        }
    }

    private IEnumerator PlayFlameAndSizzleSounds()
    {
        // Play the flame sound
        PlaySound(flameSound);

        // Wait for the flame sound to finish before playing the sizzling sound
        yield return new WaitForSeconds(flameSound.length);

        // Play the sizzling sound
        PlaySound(sizzlingSound);

        // Transition the eggs to full opacity
        StartCoroutine(TransitionEggsOpacity(maxOpacity, flameScaleSpeed));
    }

    private void PlaySound(AudioClip clip)
    {
        if (clip != null && audioSource != null)
        {
            audioSource.clip = clip;
            audioSource.Play();
        }
    }

    private IEnumerator TransitionEggsOpacity(float targetOpacity, float duration)
    {
        float startOpacity = GetEggsOpacity();
        float elapsedTime = 0;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float currentOpacity = Mathf.Lerp(startOpacity, targetOpacity, elapsedTime / duration);

            for (int i = 0; i < eggsRenderers.Length; i++)
            {
                Color color = eggsRenderers[i].material.color;
                color.a = currentOpacity;
                eggsRenderers[i].material.color = color;
            }

            yield return null;
        }

        // Ensure final opacity is set
        for (int i = 0; i < eggsRenderers.Length; i++)
        {
            Color color = eggsRenderers[i].material.color;
            color.a = targetOpacity;
            eggsRenderers[i].material.color = color;
        }
    }

    private float GetEggsOpacity()
    {
        if (eggsRenderers.Length > 0)
        {
            return eggsRenderers[0].material.color.a; // Assuming all eggs have the same opacity
        }
        return 0;
    }
}
