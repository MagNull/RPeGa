using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 1;
    [SerializeField] private float jumpHeight = 1.5f;
    [SerializeField] private float gravityForce = -9.81f;
    private InputHandler inputHandler;
    private CharacterController _characterController;
    private Vector3 movement = Vector3.zero;

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
        inputHandler = GetComponent<InputHandler>();
    }

    private void Start()
    {
        inputHandler.OnMove += Move;
    }

    private void Jump()
    {
        if (Input.GetButtonDown("Jump") && _characterController.isGrounded)
        {
            movement.y = Mathf.Sqrt(jumpHeight * -2f * gravityForce);
        }
    }

    private void Move()
    {
        if (_characterController.isGrounded)
        {
            movement = new Vector3(Input.GetAxis("Horizontal") * speed, 0,
                Input.GetAxis("Vertical") * speed);
            movement = transform.TransformDirection(movement);
        }

        movement.y += gravityForce * Time.deltaTime;
        Jump();
        _characterController.Move(movement * Time.deltaTime);
    }
}
