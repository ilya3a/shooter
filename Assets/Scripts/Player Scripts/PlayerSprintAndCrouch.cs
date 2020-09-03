using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSprintAndCrouch : MonoBehaviour
{
    private PlayerMovement playerMovement;
    public float sprintSpeed = 10f;
    public float moveSpeed = 5f;
    public float crouchSpeed = 2f;

    private Transform lookRoot;
    private float standHeight = 1.6f;
    private float crouchHeight = 1f;

    private bool isCouching = false;

    [SerializeField] private float sprintVolume = 1f;
    [SerializeField] private float crouchVolume = 0.1f;
    [SerializeField] private float walkVolumeMin = 0.2f;
    [SerializeField] private float walkVolumeMax = 0.6f;
    [SerializeField] private float walkstepDistance = 0.4f;
    [SerializeField] private float sprintStepDistance = 0.25f;
    [SerializeField] private float crouchStepDistance = 0.5f;

    /*[SerializeField]*/ private PlayerFootSteps playerFootSteps;

    // Start is called before the first frame update
    private void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
        lookRoot = transform.GetChild(0);
        //if only one child has the player footsteps
        playerFootSteps = GetComponentInChildren<PlayerFootSteps>();
    }
    private void Start()
    {
        playerFootSteps.volumeMin = walkVolumeMin;
        playerFootSteps.volumeMax = walkVolumeMax;
        playerFootSteps.stepDistance = walkstepDistance;
    }

    // Update is called once per frame
    void Update()
    {
        Sprint();
        Crouch();
    }

    private void Crouch()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        { 
            isCouching = true;
            //set the height
            var position = lookRoot.localPosition;
            position.y = crouchHeight;
            lookRoot.localPosition = position;
            //set the speed
            playerMovement.speed = crouchSpeed;
            //set the sound volume
            playerFootSteps.volumeMin = crouchVolume;
            playerFootSteps.volumeMax = crouchVolume;
            playerFootSteps.stepDistance = crouchStepDistance;
        }
        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            isCouching = false;
            //set the height
            var position = lookRoot.localPosition;
            position.y = standHeight;
            lookRoot.localPosition = position;
            //set the speed
            playerMovement.speed = moveSpeed;
            //set the sound volume
            playerFootSteps.volumeMin = walkVolumeMin;
            playerFootSteps.volumeMax = walkVolumeMax;
            playerFootSteps.stepDistance = walkstepDistance;
        }
      
    }

    private void Sprint()
    {
        if(Input.GetKeyDown(KeyCode.LeftShift) && !isCouching)
        {
            playerMovement.speed = sprintSpeed;
            //set the sound volume
            playerFootSteps.volumeMin = sprintVolume;
            playerFootSteps.volumeMax = sprintVolume;
            playerFootSteps.stepDistance = sprintStepDistance;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift) && !isCouching)
        {
            playerMovement.speed = moveSpeed;
            //set the sound volume
            playerFootSteps.volumeMin = walkVolumeMin;
            playerFootSteps.volumeMax = walkVolumeMax;
            playerFootSteps.stepDistance = walkstepDistance;
        }
       
    }
}
