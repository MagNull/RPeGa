using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private Transform start;
    
    [Header("Mouse Look")]
    [SerializeField] private float mouseSens = 1;
    [SerializeField] private Transform targetFollow;
    private float rotationX;
    private float rotationY;
    
    [Header("Player Move")]
    [SerializeField] private float walkingSpeed = 1;
    [SerializeField] private float runSpeed = 2;
    [SerializeField] private float jumpHeight;
    private CharacterController _characterController;
    [SerializeField] private Vector3 movement;
    
    [Header("Gravity")]
    [SerializeField] private float gravityForce = -19.62f;
    private Vector3 velocity;

    private Animator _animator;

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        rotationX = targetFollow.rotation.x;
        rotationY = transform.rotation.y;

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            _animator.SetBool("isRunning", false);
        }
        Move();
        Rotate();
        if (Input.GetKeyDown(KeyCode.R))
        {
            transform.position = start.position;
        }
    }


    private void Rotate()
    {
        rotationX -= Input.GetAxis("Mouse Y") * mouseSens;
        rotationY += Input.GetAxis("Mouse X") * mouseSens;
        rotationX = Mathf.Clamp(rotationX, -60, 60);
        
        transform.localEulerAngles = new Vector3(0, rotationY, 0);
        targetFollow.localEulerAngles = new Vector3(rotationX, 0, 0);
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
            float movementX = Input.GetAxisRaw("Horizontal");
            float movementZ = Input.GetAxisRaw("Vertical");
            float speed = 0;
            if (movementZ == 0 && movementZ == 0)
            {
                _animator.SetInteger("Move State", 0);
            }
            else
            if (movementZ > 0)
            {
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    float blend = Mathf.Clamp01(_animator.GetFloat("Blend") + 0.05f);
                    speed = runSpeed * blend;
                    _animator.SetFloat("Blend", blend );
                }
                else
                {
                    float blend = Mathf.Clamp01(_animator.GetFloat("Blend") - 0.01f);
                    speed = walkingSpeed * (1 - blend);
                    _animator.SetFloat("Blend", blend );
                }
                _animator.SetInteger("Move State", 1);
            }
            else
            if (movementZ < 0)
            {
                _animator.SetInteger("Move State", -1);
            }
            movement = new Vector3(movementX * speed,0, movementZ * speed);
            movement = transform.TransformDirection(movement);
            
        }
        Jump();
        movement.y += gravityForce * Time.deltaTime;
        _characterController.Move(movement * Time.deltaTime);
    }
}
