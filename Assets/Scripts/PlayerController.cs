using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region components
    public Rigidbody2D thisRBPlatformer, thisRBTopDown;
    public Rigidbody thisRBFPS;

    public Camera sceneCamera;
    [SerializeField] bool isFPS, isPlatformer, isTopdown;
    //public WeaponContainer weapon;
    #endregion

    #region input parameters
    private Vector2 _movementInput;
    private Vector2 _moveDirection;
    private Vector2 _mousePosition;
    #endregion

    private float _defaultSpeedTopdown = 5f;

    public float speedBuff;

    #region platformer parameters
    private float _defaultSpeedPlatformer = 20f;
    public float jumpForce = 70f;
    public Transform groundCheck;
    public float checkRadius = 0.5f;
    public LayerMask groundLayer;

    private bool isGrounded;
    #endregion

    #region fps parameters
    public float speed = 5.0f;
    public float mouseSensitivity = 2.0f;
    public Transform playerCamera;
    public GameObject bulletPrefab;
    public Transform bulletSpawnPoint;
    public float bulletSpeed = 10.0f;
    public float fireRate = 1.0f;
    public float mouseX, mouseY;

    private float verticalRotation = 0;
    private float nextFireTime;
    #endregion

    private void Awake()
    {

        thisRBPlatformer = GetComponent<Rigidbody2D>();
        thisRBTopDown = GetComponent<Rigidbody2D>();
        thisRBFPS = GetComponent<Rigidbody>();


    }

    void Update()
    {
        #region input

        if (isTopdown)
        {
            _movementInput.x = Input.GetAxisRaw("Horizontal");
            _movementInput.y = Input.GetAxisRaw("Vertical");
            _moveDirection = new Vector2(_movementInput.x, _movementInput.y).normalized;//normalized so diagonal goes same speed
            _mousePosition = sceneCamera.ScreenToWorldPoint(Input.mousePosition);
        }
        else if (isPlatformer)
        {
            _movementInput.x = Input.GetAxis("Horizontal");

            //rework this into fixed update? 
            thisRBPlatformer.velocity = new Vector2(_movementInput.x * _defaultSpeedPlatformer, thisRBPlatformer.velocity.y);

            isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, groundLayer);

            Debug.Log("is grounded" + isGrounded);


            if (isGrounded && Input.GetKeyDown(KeyCode.Space))
            {
                Debug.Log("is jumping");
                thisRBPlatformer.velocity = Vector2.up * jumpForce;
            }
        }
        else if (isFPS)
        {
            _movementInput.x = Input.GetAxis("Horizontal");
            _movementInput.y = Input.GetAxis("Vertical");
            mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
            mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;
        }
        #endregion
    }

    private void FixedUpdate()
    {
        #region movement
        if (isTopdown)
        {
            thisRBTopDown.velocity = new Vector2(_moveDirection.x * (_defaultSpeedTopdown + speedBuff), _moveDirection.y * (_defaultSpeedTopdown + speedBuff));
            // thisRB.MovePosition(thisRB.position + _movementInput * topSpeed * Time.fixedDeltaTime);
            // thisRBTopDown.gravityScale = 0f;
        }
        else if (isPlatformer)
        {

        }
        else if (isFPS)
        {
            Vector3 moveDirection = new Vector3(_movementInput.x, 0, _movementInput.y);
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= speed;
            thisRBFPS.MovePosition(thisRBFPS.position + moveDirection * Time.fixedDeltaTime);

        }
        #endregion

        #region Shooting

        if (isTopdown)
        {
            //player follows mouse and rotates
            Vector2 aimDirection = _mousePosition - thisRBTopDown.position;
            float aimAngle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg - 90f;
            thisRBTopDown.rotation = aimAngle;
        }
        else if (isPlatformer)
        {

        }
        else if (isFPS)
        {
            transform.Rotate(0, mouseX, 0);
            verticalRotation -= mouseY;
            verticalRotation = Mathf.Clamp(verticalRotation, -80, 80);
            playerCamera.localRotation = Quaternion.Euler(verticalRotation, 0, 0);

            if (Input.GetButtonDown("Fire1") && Time.time > nextFireTime)
            {
                nextFireTime = Time.time + 1f / fireRate;
                Shoot();
            }
        }
        //doshooting();
        #endregion
    }

    private void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
        bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * bulletSpeed;
        Destroy(bullet, 2.0f);
    }
}
