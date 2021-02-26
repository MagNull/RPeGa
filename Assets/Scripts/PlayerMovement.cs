using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _jumpHeight = 1.5f;
    [SerializeField] private float _gravityForce = -9.81f;
    
    private InputHandler _inputHandler;
    private PlayerSpeedManipulator _playerSpeedManipulator;
    
    private CharacterController _characterController;
    private Vector3 _movement = Vector3.zero;

    [Inject]
    public void Construct(InputHandler inputHandler, PlayerSpeedManipulator playerSpeedManipulator)
    {
        _inputHandler = inputHandler;
        _playerSpeedManipulator = playerSpeedManipulator;
    }

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
    }

    private void OnEnable()
    {
        _inputHandler.OnMove += Move;
    }

    private void OnDisable()
    {
        _inputHandler.OnMove -= Move;
    }

    private void Jump()
    {
        if (Input.GetButtonDown("Jump") && _characterController.isGrounded)
        {
            _movement.y = Mathf.Sqrt(_jumpHeight * -2f * _gravityForce);
        }
    }

    private void Move()
    {
        if (_characterController.isGrounded)
        {
            _movement = new Vector3(Input.GetAxis("Horizontal"), 0,
                Input.GetAxis("Vertical"));
            if (_movement.magnitude > 1) _movement.Normalize();
            _movement = transform.TransformDirection(_movement * _playerSpeedManipulator.Speed);
        }

        _movement.y += _gravityForce * Time.deltaTime;
        Jump();
        _characterController.Move(_movement * Time.deltaTime);
    }
}
