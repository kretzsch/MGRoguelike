using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class FirstPersonController : MonoBehaviour
{
    [Header("Player")]
    [Tooltip("Move speed of the character in m/s")]
    public float MoveSpeed = 4.0f;
    [Tooltip("Sprint speed of the character in m/s")]
    public float SprintSpeed = 6.0f;
    [Tooltip("Rotation speed of the character")]
    public float RotationSpeed = 1.0f;
    [Tooltip("Acceleration and deceleration")]
    public float SpeedChangeRate = 10.0f;

    [Header("Cinemachine")]
    [Tooltip("The follow target set in the Cinemachine Virtual Camera that the camera will follow")]
    public GameObject CinemachineCameraTarget;
    [Tooltip("How far in degrees can you move the camera up")]
    public float TopClamp = 90.0f;
    [Tooltip("How far in degrees can you move the camera down")]
    public float BottomClamp = -90.0f;

    // cinemachine
    private float _cinemachineTargetPitch;

    // player
    private float _speed;
    private float _rotationVelocity;
    private float _verticalVelocity;
    private float _terminalVelocity = 53.0f;

    private GameObject _mainCamera;
    private WeaponManager _weaponManager;
    private CharacterController _characterController;

    //input variables
    private Vector2 _moveInput;
    private Vector2 _lookInput;

    private void Awake()
    {
        // get a reference to our main camera
        if (_mainCamera == null)
        {
            _mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        }
        _weaponManager = FindObjectOfType<WeaponManager>();
        Debug.Log($"WeaponManager reference: {_weaponManager}");

    }

    private void Start()
    {
        _characterController = GetComponent<CharacterController>();
    }
    private void FixedUpdate()
    {
        Look();
        Move();
    }
    #region input methods
    public void OnMove(InputAction.CallbackContext context)
    {
        _moveInput = context.ReadValue<Vector2>();
    }
    //the input system uses Delta pointer for onlook
    //todo: research if this is the best way to do this
    public void OnLook(InputAction.CallbackContext context)
    {
        _lookInput = context.ReadValue<Vector2>();
        Debug.Log($"OnLookInput: {_lookInput}");
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
    private void Look()
    {
        float deltaTimeMultiplier = Time.deltaTime;

        _cinemachineTargetPitch += _lookInput.y * RotationSpeed * deltaTimeMultiplier;
        _rotationVelocity = _lookInput.x * RotationSpeed * deltaTimeMultiplier;
        Debug.Log($"_cinemachineTargetPitch: {_cinemachineTargetPitch}");
        Debug.Log($"_rotationVelocity: {_rotationVelocity}");
        _cinemachineTargetPitch = ClampAngle(_cinemachineTargetPitch, BottomClamp, TopClamp);

        CinemachineCameraTarget.transform.localRotation = Quaternion.Euler(_cinemachineTargetPitch, 0.0f, 0.0f);
        transform.Rotate(Vector3.up * _rotationVelocity);
    }
    public void Move()
    {
        Vector3 moveDirection = transform.right * _moveInput.x + transform.forward * _moveInput.y;
        _characterController.Move(moveDirection * MoveSpeed * Time.deltaTime);
    }
    private static float ClampAngle(float lfAngle, float lfMin, float lfMax)
    {
        if (lfAngle < -360f) lfAngle += 360f;
        if (lfAngle > 360f) lfAngle -= 360f;
        return Mathf.Clamp(lfAngle, lfMin, lfMax);
    }
}


