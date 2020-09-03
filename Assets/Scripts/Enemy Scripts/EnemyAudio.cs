using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAudio : MonoBehaviour
{

    [SerializeField] private AudioSource audioSource;

    [SerializeField] private AudioClip screamClip, dieClip;

    [SerializeField] private AudioClip[] attackClips;
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayAttackSound()
    {
        audioSource.clip = attackClips[Random.Range(0, attackClips.Length)];
        audioSource.Play();
    }
    public void PlaySrceamSound()
    {
        audioSource.clip = screamClip;
        audioSource.Play();
    }
    public void PlayDeathSound()
    {
        audioSource.clip = dieClip;
        audioSource.Play();
    }
}
