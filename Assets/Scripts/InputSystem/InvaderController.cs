using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// This class is responsible for the player's movement, aiming, shooting, reloading, and weapon switching. 
/// It utilizes input events from the new Unity Input System to handle various actions 
/// and interfaces with the WeaponManager to control the active weapon.
/// This class is specific to the genre: Invader
/// </summary>
public class InvaderController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private LayerMask groundLayer;

    private WeaponManager _weaponManager;
    private Rigidbody2D rb;
    private Vector2 moveDirection;
    private Camera _mainCamera;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        _weaponManager = FindObjectOfType<WeaponManager>();
        _mainCamera = Camera.main;
    }

    private void FixedUpdate()
    {
        Move();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveDirection = context.ReadValue<Vector2>();
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
}

