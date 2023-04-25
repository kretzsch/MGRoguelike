using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// This class is responsible for the player's movement, aiming, shooting, reloading, and weapon switching. 
/// It utilizes input events from the new Unity Input System to handle various actions 
/// and interfaces with the WeaponManager to control the active weapon.
/// </summary>
public class TopDownController2D : MonoBehaviour
{
    public float moveSpeed = 5.0f;
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float bulletSpeed = 10f;
    public float fireRate = 0.25f;

    private Rigidbody2D _rigidbody2D;
    private Camera _mainCamera;
    private float _lastFireTime;
    private Vector2 _inputVector;



    [SerializeField] private WeaponManager _weaponManager;
    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _mainCamera = Camera.main;

        _weaponManager = FindObjectOfType<WeaponManager>();
        Debug.Log($"WeaponManager reference: {_weaponManager}");

    }

    private void Update()
    {
        // Move the character continuously
        Vector2 newPosition = _rigidbody2D.position + _inputVector * moveSpeed * Time.deltaTime;
        _rigidbody2D.MovePosition(newPosition);
    }

    #region input methods
    public void OnMove(InputAction.CallbackContext context)
    {
        _inputVector = context.ReadValue<Vector2>();
    }

    public void OnAim(InputAction.CallbackContext context)
    {
        Vector2 mousePosition = context.ReadValue<Vector2>();
        Vector3 worldMousePosition = _mainCamera.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, _mainCamera.nearClipPlane));
        Vector2 aimDirection = (Vector2)worldMousePosition - _rigidbody2D.position;

        float aimAngle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg - 0f;
        _rigidbody2D.rotation = aimAngle;
  
    }
    public void OnReload(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if (_weaponManager == null)
            {
                return;
            }

            if (_weaponManager.CurrentWeapon == null)
            {
                return;
            }
            _weaponManager.CurrentWeapon.Reload();
        }
    }

    public void OnShoot(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if (_weaponManager == null)
            {
                return;
            }

            if (_weaponManager.CurrentWeapon == null)
            {
                return;
            }
            _weaponManager.CurrentWeapon.Shoot();
        }
    }
    public void OnSwitchWeapon(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            float scrollValue = context.ReadValue<float>();
            if (scrollValue != 0 && _weaponManager != null)
            {
                int direction = scrollValue > 0 ? 1 : -1;
                _weaponManager.SwitchWeapon(direction);
            }
        }
    }

    #endregion
}
