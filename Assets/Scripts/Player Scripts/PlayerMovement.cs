using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class PlayerMovement : MonoBehaviour
{

    private CharacterController characterController;
    public Vector3 moveDirection;

    public float speed = 5f;
    public float gravity;

    public float jumpForce;
    private float verticalVelocity;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MoveThePlayer();   
    }

    private void MoveThePlayer()
    {
        // X axis -  left right  Y axis - up and down(jump)  Z axis foward and backword 
        moveDirection = new Vector3(Input.GetAxis(Axis.HORIZONTAL), 0f, Input.GetAxis(Axis.VERTICAL));

        moveDirection = transform.TransformDirection(moveDirection);
        moveDirection *= speed * Time.deltaTime;

        ApllyGravity();
        characterController.Move(moveDirection);
    }

    private void ApllyGravity()
    {
        PlayerJump();

        verticalVelocity -= gravity * Time.deltaTime;
        moveDirection.y = verticalVelocity * Time.deltaTime;
    }

    private void PlayerJump()
    {
        if(characterController.isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            verticalVelocity = jumpForce * Time.deltaTime;
        }
    }
}
