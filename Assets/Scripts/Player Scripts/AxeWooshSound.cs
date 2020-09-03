using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeWooshSound : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip[] wooshSounds;

    void PlayWooshSounds()
    {
        audioSource.clip = wooshSounds[Random.Range(0, wooshSounds.Length)];
        audioSource.Play();
    }
}
