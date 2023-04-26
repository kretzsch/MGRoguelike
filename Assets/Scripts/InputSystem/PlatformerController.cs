using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// This class is responsible for the player's movement, aiming, shooting, reloading, and weapon switching. 
/// It utilizes input events from the new Unity Input System to handle various actions 
/// and interfaces with the WeaponManager to control the active weapon.
/// This class is specific to the genre: platformer
/// </summary>
public class PlatformerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private WeaponManager weaponManager;

    private Rigidbody2D rb;
    private Vector2 moveDirection;
    private bool isGrounded;
    private float jumpCooldown;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        weaponManager = FindObjectOfType<WeaponManager>();
        Debug.Log($"WeaponManager reference: {weaponManager}");
    }

    private void Update()
    {
        GroundCheck();
        JumpCooldown();
    }

    private void FixedUpdate()
    {
        Move();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveDirection = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.started && isGrounded && jumpCooldown <= 0)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            jumpCooldown = 0.25f;
        }
    }

    public void OnAim(InputAction.CallbackContext context)
    {
        // Add platformer-specific aiming logic here
    }

    public void OnShoot(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if (weaponManager == null)
            {
                return;
            }

            if (weaponManager.CurrentWeapon == null)
            {
                return;
            }
            weaponManager.CurrentWeapon.Shoot();
        }
    }

    public void OnReload(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if (weaponManager == null)
            {
                return;
            }

            if (weaponManager.CurrentWeapon == null)
            {
                return;
            }
            weaponManager.CurrentWeapon.Reload();
        }
    }

    public void OnSwitchWeapon(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            float scrollValue = context.ReadValue<float>();
            if (scrollValue != 0 && weaponManager != null)
            {
                int direction = scrollValue > 0 ? 1 : -1;
                weaponManager.SwitchWeapon(direction);
            }
        }
    }

    private void Move()
    {
        rb.velocity = new Vector2(moveDirection.x * moveSpeed, rb.velocity.y);
    }

    private void GroundCheck()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 1.1f, groundLayer);
        isGrounded = hit.collider != null;
    }

    private void JumpCooldown()
    {
        if (jumpCooldown > 0)
        {
            jumpCooldown -= Time.deltaTime;
        }
    }
}
