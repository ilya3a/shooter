using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    [SerializeField] private Transform playerRoot, lookRoot;

    [SerializeField] private bool invert;

    [SerializeField] private bool canUnlocked = true;

    [SerializeField]private float sensetivity = 5f;

    [SerializeField] private int smoothSteps = 10;

    [SerializeField] private float smoothWeight = 0.4f;

    [SerializeField] private float rollAngle = 10f;
    [SerializeField] private float rollSpeed = 3f;

    private Vector2 currentMouseLook;

    private float currentRollAngle;

    private float lastLookFrame;

    private Vector2 lookAngles;
    private Vector2 smoothMove;



    [SerializeField]
    private Vector2 defaultLookLimits = new Vector2(-70f,80f);


    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        LockAndUnlockCursor();
        if(Cursor.lockState == CursorLockMode.Locked)
        {
            LookAround();
        }
        
    }

    private void LockAndUnlockCursor()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(Cursor.lockState == CursorLockMode.Locked)
            {
                Cursor.lockState = CursorLockMode.None;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }
    }

    private void LookAround()
    {
        currentMouseLook = new Vector2(Input.GetAxis(MouseAxis.MOUSE_Y), Input.GetAxis(MouseAxis.MOUSE_X));

        lookAngles.x += currentMouseLook.x * sensetivity * (invert ? 1f : -1f);
        lookAngles.y += currentMouseLook.y * sensetivity;

        lookAngles.x = Mathf.Clamp(lookAngles.x, defaultLookLimits.x, defaultLookLimits.y);
        //return the roll angle on each frame on the z axis
        //currentRollAngle = Mathf.Lerp(currentRollAngle, Input.GetAxisRaw(MouseAxis.MOUSE_X) * rollAngle, Time.deltaTime * rollSpeed);
        //lookRoot.localRotation = Quaternion.Euler(lookAngles.x, 0f, currentRollAngle);
        lookRoot.localRotation = Quaternion.Euler(lookAngles.x, 0f, 0f);
        playerRoot.localRotation = Quaternion.Euler(0f, lookAngles.y, 0f);
    }
}