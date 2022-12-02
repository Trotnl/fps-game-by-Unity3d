﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Motion : MonoBehaviour
{
    #region Variables
    public float speed;
    public float sprintModifier;
    public float jumpForce;
    public Camera normalCam;
    public Transform groundDetector;
    public LayerMask ground;

    private Rigidbody rig;
    private float baseFOV;
    private float sprintFOVModifier = 1.5f;
    #endregion

    #region MonoBehaviour Callbacks

    // Start is called before the first frame update
    private void Start()
    {
        baseFOV = normalCam.fieldOfView;
        
        Camera.main.enabled = false;
        rig = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        //Axles
        float t_hmove = Input.GetAxisRaw("Horizontal");
        float t_vmove = Input.GetAxisRaw("Vertical");

        //Controls
        bool jump = Input.GetKeyDown(KeyCode.Space);
        bool sprint = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);

        //States
        bool isGrounded = Physics.Raycast(groundDetector.position, Vector3.down, 0.1f, ground);
        bool isJumping = jump;
        bool isSprinting = sprint && t_vmove > 0 && !isJumping && isGrounded;

        //Jumping
        if (isJumping)
        {
            rig.AddForce(Vector3.up * jumpForce);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Axles
        float t_hmove = Input.GetAxisRaw("Horizontal");
        float t_vmove = Input.GetAxisRaw("Vertical");

        //Controls
        bool jump = Input.GetKeyDown(KeyCode.Space);
        bool sprint = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);

        //States
        bool isGrounded = Physics.Raycast(groundDetector.position, Vector3.down, 0.1f, ground);
        bool isJumping = jump;
        bool isSprinting = sprint && t_vmove > 0 && !isJumping && isGrounded;

        //Movement
        Vector3 t_direction = new Vector3(t_hmove,0, t_vmove);
        t_direction.Normalize();

        float t_adjustedSpeed = speed;
        if (isSprinting) t_adjustedSpeed *= sprintModifier;

        Vector3 t_targetVelocity = transform.TransformDirection(t_direction) * t_adjustedSpeed * Time.deltaTime;
        t_targetVelocity.y = rig.velocity.y;

        rig.velocity = t_targetVelocity;


        //Field of View
        if (isSprinting)
        {
            normalCam.fieldOfView = Mathf.Lerp(normalCam.fieldOfView, baseFOV * sprintFOVModifier, Time.deltaTime * 8f);
        }
        else normalCam.fieldOfView = Mathf.Lerp(normalCam.fieldOfView, baseFOV, Time.deltaTime * 8f);

        
    }
    #endregion

}
