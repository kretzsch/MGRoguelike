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


    [Space(10)]
    [Tooltip("The height the player can jump")]
    public float JumpHeight = 1.2f;
    [Tooltip("The character uses its own gravity value. The engine default is -9.81f")]
    public float Gravity = -15.0f;

    [Space(10)]
    [Tooltip("Time required to pass before being able to jump again. Set to 0f to instantly jump again")]
    public float JumpTimeout = 0.1f;
    [Tooltip("Time required to pass before entering the fall state. Useful for walking down stairs")]
    public float FallTimeout = 0.15f;

    [Header("Player Grounded")]
    [Tooltip("If the character is grounded or not. Not part of the CharacterController built in grounded check")]
    public bool Grounded = true;
    [Tooltip("Useful for rough ground")]
    public float GroundedOffset = -0.14f;
    [Tooltip("The radius of the grounded check. Should match the radius of the CharacterController")]
    public float GroundedRadius = 0.5f;
    [Tooltip("What layers the character uses as ground")]
    public LayerMask GroundLayers;



    private bool _grounded;
    private float _jumpTimeoutDelta;
    private float _fallTimeoutDelta;
   


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
    private bool _jumpInput;


    //used for the spawn animation of a new weapon
    [SerializeField] private float reloadTime = 1f;
    [SerializeField] private float throwForce = 20f;


    private bool IsCurrentDeviceMouse
    {
        get
        {
            return Mouse.current != null && Mouse.current == InputSystem.GetDevice<Mouse>();
        }
    }

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
        _cinemachineTargetPitch = CinemachineCameraTarget.transform.localEulerAngles.x;
        if (_cinemachineTargetPitch > 180f)
        {
            _cinemachineTargetPitch -= 420f; // the weed number
        }
    }
    private void FixedUpdate()
    {
        JumpAndGravity();
        GroundedCheck();
        Move();
    }

    private void LateUpdate()
    {
        Look();
    }

    private void JumpAndGravity()
    {
        if (_grounded)
        {
            _fallTimeoutDelta = FallTimeout;

            if (_verticalVelocity < 0.0f)
            {
                _verticalVelocity = -2f;
            }

            if (_jumpInput && _jumpTimeoutDelta <= 0.0f)
            {
                _verticalVelocity = Mathf.Sqrt(JumpHeight * -2f * Gravity);
                _jumpInput = false;
            }

            if (_jumpTimeoutDelta >= 0.0f)
            {
                _jumpTimeoutDelta -= Time.deltaTime;
            }
        }
        else
        {
            _jumpTimeoutDelta = JumpTimeout;

            if (_fallTimeoutDelta >= 0.0f)
            {
                _fallTimeoutDelta -= Time.deltaTime;
            }
        }

        if (_verticalVelocity < _terminalVelocity)
        {
            _verticalVelocity += Gravity * Time.deltaTime;
        }

        _characterController.Move(new Vector3(0, _verticalVelocity, 0) * Time.deltaTime);
    }


    private void GroundedCheck()
    {
        // set sphere position, with offset
        Vector3 spherePosition = new Vector3(transform.position.x, transform.position.y - GroundedOffset, transform.position.z);
        _grounded = Physics.CheckSphere(spherePosition, GroundedRadius, GroundLayers, QueryTriggerInteraction.Ignore);
    }

    #region input methods
    public void OnMove(InputAction.CallbackContext context)
    {
        _moveInput = context.ReadValue<Vector2>();
    }
    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.started && _grounded)
        {
            _jumpInput = true;
            _jumpTimeoutDelta = 0.0f;
        }
    }

   


    //the input system uses Delta pointer for onlook
    //todo: research if this is the best way to do this
    public void OnLook(InputAction.CallbackContext context)
    {
        _lookInput = context.ReadValue<Vector2>();
    }

    public void OnReload(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            StartCoroutine(ReloadSequence());
        }
    }

    public void OnShoot(InputAction.CallbackContext context)
    {

        Debug.Log("onshoot");
        if (context.performed)
        {
            Debug.Log("context started");
            if (_weaponManager == null)
            {
                return;
            }

            if (_weaponManager.CurrentWeapon == null)
            {
                return;
            }
            Debug.Log($"Current weapon: {_weaponManager.CurrentWeapon.name}");

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
        // Don't multiply mouse input by Time.deltaTime
        float deltaTimeMultiplier = IsCurrentDeviceMouse ? 1.0f : Time.deltaTime;

        _cinemachineTargetPitch += _lookInput.y * RotationSpeed * deltaTimeMultiplier;
        _rotationVelocity = _lookInput.x * RotationSpeed * deltaTimeMultiplier;
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
    private IEnumerator ReloadSequence()
    {
        // Store references
        MeshRenderer weaponMeshRenderer = _weaponManager.CurrentWeapon.GetComponent<MeshRenderer>();
        MeshRenderer[] weaponMeshRenderers = _weaponManager.CurrentWeapon.GetComponentsInChildren<MeshRenderer>();
        ProjectileWeapon currentWeaponScript = _weaponManager.CurrentWeapon;
        ParticleSystem weaponParticleSystem = _weaponManager.CurrentWeapon.transform.Find("WeaponSpawn").GetComponent<ParticleSystem>();



        // Disable weapon visuals and functionality
        foreach (MeshRenderer renderer in weaponMeshRenderers)
        {
            renderer.enabled = false;
        }
        weaponMeshRenderer.enabled = false;
        currentWeaponScript.canShoot = false;

        // Spawn and throw the fake weapon
        GameObject fakeWeapon = Instantiate(currentWeaponScript.gameObject, currentWeaponScript.transform.position, currentWeaponScript.transform.rotation);
        fakeWeapon.GetComponent<MeshRenderer>().enabled = true;
        fakeWeapon.GetComponent<ProjectileWeapon>().canShoot = false;
        Rigidbody rb = fakeWeapon.AddComponent<Rigidbody>();
        fakeWeapon.AddComponent<BoxCollider>();
        rb.AddForce(_weaponManager.transform.forward * throwForce, ForceMode.VelocityChange);

        // Wait for some time
        yield return new WaitForSeconds(1.5f);

        // Enable weapon visuals and functionality
        foreach (MeshRenderer renderer in weaponMeshRenderers)
        {
            renderer.enabled = true;
        }
        weaponMeshRenderer.enabled = true;
        currentWeaponScript.canShoot = true;
        weaponParticleSystem.Play();

        // Perform the actual reload
        currentWeaponScript.Reload();

        // Destroy the fake weapon
        Destroy(fakeWeapon, 2f);
    }

}


