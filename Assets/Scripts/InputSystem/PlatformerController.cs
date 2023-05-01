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

    private WeaponManager _weaponManager;
    private Rigidbody2D rb;
    private Vector2 moveDirection;
    private bool isGrounded;
    private float jumpCooldown;
    private Camera _mainCamera;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        _weaponManager = FindObjectOfType<WeaponManager>();
        _mainCamera = Camera.main;
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
        if (_weaponManager == null || _weaponManager.CurrentWeapon == null) return;

        Vector2 mousePosition = context.ReadValue<Vector2>();
        Vector3 worldMousePosition = _mainCamera.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, _mainCamera.nearClipPlane));
        Vector2 aimDirection = (Vector2)worldMousePosition - (Vector2)transform.position; // Change this line

        float aimAngle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
        _weaponManager.CurrentWeapon.transform.rotation = Quaternion.Euler(0, 0, aimAngle);

        Vector2 weaponPosition = (Vector2)transform.position + (aimDirection.normalized ); //* weaponDistance
        _weaponManager.CurrentWeapon.transform.position = new Vector3(weaponPosition.x, weaponPosition.y, _weaponManager.CurrentWeapon.transform.position.z);
    }

    public void OnShoot(InputAction.CallbackContext context)
    {
        if (_weaponManager != null && _weaponManager.CurrentWeapon != null && _weaponManager.isActiveAndEnabled && _weaponManager.CurrentWeapon.isActiveAndEnabled)
        {
            _weaponManager.CurrentWeapon.Shoot();
        }
    }

    public void OnReload(InputAction.CallbackContext context)
    {
            _weaponManager?.CurrentWeapon?.Reload();
    }

    public void OnSwitchWeapon(InputAction.CallbackContext context)
    {
        if (context.performed && _weaponManager != null)
        {
            float scrollValue = context.ReadValue<float>();
            if (scrollValue != 0)
            {
                int direction = scrollValue > 0 ? 1 : -1;
                _weaponManager?.SwitchWeapon(direction);
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
