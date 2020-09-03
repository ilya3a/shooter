
using UnityEngine;

public class PlayerFootSteps : MonoBehaviour
{
    private AudioSource footstepSound;

    [SerializeField] private AudioClip[] footstempClip;

    private CharacterController characterController;
    
    [HideInInspector] public float volumeMin, volumeMax;

    [SerializeField]private float accumulatedDistance;//accumulated distance is the vale how far can we go e.g. make a step or sprint or couch move until footstep sound

    [HideInInspector] public float stepDistance;// the distance of step to make sound


    void Awake()
    {
        footstepSound = GetComponent<AudioSource>();
        characterController = GetComponentInParent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckToPlayFootstampSound();
    }

    private void CheckToPlayFootstampSound()
    {
        if(!characterController.isGrounded)
        {
            return;
        }
        //we on the ground
        if (characterController.velocity.sqrMagnitude > 0)
        {
            accumulatedDistance += Time.deltaTime;
            if(accumulatedDistance > stepDistance)
            {
                footstepSound.volume = Random.Range(volumeMin, volumeMax);
                footstepSound.clip = footstempClip[Random.Range(0, footstempClip.Length)];
                footstepSound.Play();
                accumulatedDistance = 0f;
            }
        }
        else
        {
            accumulatedDistance = 0f;
        }
    }
}
