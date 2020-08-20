using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public static Player instance;
    public ActionSystem actionSystem;
    public bool actionCooldown;
    Camera mainCamera;
    public Transform weapon;
    public Sword sword;
    public Animator weaponAnimator;

    #region Movement Variables
    Rigidbody2D rb;
    [SerializeField]
    float moveSpeed = 1, movementAcceleration = 0.5f;
    Vector2 movementInput;
    Vector2 movement;
    Vector2 lastPosition;
    public Vector2 velocity;
    #endregion

    #region Shooting Variables
    Vector2 pointerPosition;
    public Vector2 lookDirection;
    [SerializeField] GameObject projectile;
    [SerializeField] float projectileSpeed = 10;
    #endregion

    void Awake()
    {
        instance = this;
        rb = GetComponent<Rigidbody2D>();
        mainCamera = Camera.main;
        actionSystem = GetComponent<ActionSystem>();
        weaponAnimator = weapon.GetComponent<Animator>();
        sword = GetComponentInChildren<Sword>();
    }

    void Start()
    {

    }

    void Update()
    {
        velocity = ((Vector2)transform.position - lastPosition)/Time.deltaTime;
        lookDirection = mainCamera.ScreenToWorldPoint(pointerPosition) - transform.position;
        lookDirection.Normalize();
        transform.up = lookDirection;

        movement = Vector2.Lerp(movement, movementInput, movementAcceleration * Time.deltaTime);
    }

    void LateUpdate()
    {
        lastPosition = transform.position;
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

    public void LeftAttack(InputAction.CallbackContext context)
    {
        if (context.performed && actionCooldown == false)
        {
            actionCooldown = true;
            actionSystem.LeftAction();
        }
    }

    public void RightAttack(InputAction.CallbackContext context)
    {
        if (context.performed && actionCooldown == false)
        {
            actionCooldown = true;
            actionSystem.RightAction();
        }
    }

    void ShootProjectile()
    {
        var tempBullet = Instantiate(projectile).transform;
        tempBullet.position = transform.position; //(Vector2)transform.position + lookDirection * 2;
        tempBullet.right = lookDirection;
        tempBullet.GetComponent<Projectile>().movementVector = lookDirection * projectileSpeed;
    }
}
