﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    Camera mainCamera;

    #region Movement Variables
    Rigidbody2D rb;
    [SerializeField]
    float moveSpeed = 1, movementAcceleration = 0.5f;
    Vector2 movementInput;
    Vector2 movement;
    #endregion

    #region Shooting Variables
    Vector2 pointerPosition;
    Vector2 lookDirection;
    [SerializeField] GameObject projectile;
    [SerializeField] float projectileSpeed = 10;
    #endregion

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        mainCamera = Camera.main;
    }

    void Start()
    {
        
    }

    void Update()
    {
        lookDirection = mainCamera.ScreenToWorldPoint(pointerPosition) - transform.position;
        lookDirection.Normalize();
        
        movement = Vector2.Lerp(movement, movementInput, movementAcceleration * Time.deltaTime);
    }


    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

    public void Move(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>();
    }

    public void Look(InputAction.CallbackContext context)
    {
        pointerPosition = context.ReadValue<Vector2>();
    }

    public void Shoot(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            var tempBullet = Instantiate(projectile).transform;
            tempBullet.position = transform.position; //(Vector2)transform.position + lookDirection * 2;
            tempBullet.right = lookDirection;
            tempBullet.GetComponent<Projectile>().movementVector = lookDirection * projectileSpeed;
        }
    }
}