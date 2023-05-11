using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private float rotationSpeed;
    [SerializeField]
    private float jumpHeight;
    [SerializeField]
    private CharacterController characterController;
    [SerializeField]
    private Animator animator;
    
    private float ySpeed;

    private float horizontalInput;
    private float verticalInput;
    private float originalStepOffset;
    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        originalStepOffset = characterController.stepOffset;
    }
    private void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        Vector3 moveDirection = new Vector3 (horizontalInput, 0,verticalInput);
        float magnitute = Mathf.Clamp01(moveDirection.magnitude)*moveSpeed;
        moveDirection.Normalize();

        ySpeed += Physics.gravity.y * Time.deltaTime;
        if (characterController.isGrounded)
        {
            ySpeed = -0.5f;
            characterController.stepOffset = originalStepOffset;
            if (Input.GetButtonDown("Jump"))
            {
                Debug.Log("jump");
                ySpeed = jumpHeight;
                characterController.stepOffset = 0;
            }
        }
        
        Vector3 velocity = moveDirection * magnitute;
        velocity.y = ySpeed;
        characterController.Move(velocity * Time.deltaTime);
        //characterController.SimpleMove(moveDirection * Time.deltaTime);

        if (moveDirection != Vector3.zero)
        {
            animator.SetBool("isMoving", true);
            Quaternion toRotation = Quaternion.LookRotation(moveDirection, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed*Time.deltaTime);
        }
        else
        {
            animator.SetBool("isMoving", false);
        }
    }
}
