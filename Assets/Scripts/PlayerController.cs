using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
  #region components
  public Rigidbody2D thisRB;
  public Camera sceneCamera;
  //public WeaponContainer weapon;
  #endregion

  #region input parameters
  private Vector2 _movementInput;
  private Vector2 _moveDirection;
  private Vector2 _mousePosition;
  #endregion

  private float _defaultSpeed = 5f;
  public float speedBuff;

  private void Awake()
  {
    thisRB = GetComponent<Rigidbody2D>();
  }

  void Update()
  {
    #region input
    _movementInput.x = Input.GetAxisRaw("Horizontal");
    _movementInput.y = Input.GetAxisRaw("Vertical");
    _moveDirection = new Vector2(_movementInput.x, _movementInput.y).normalized;//normalized so diagonal goes same speed
    _mousePosition = sceneCamera.ScreenToWorldPoint(Input.mousePosition);
    #endregion
  }

  private void FixedUpdate()
  {
    #region movement
    thisRB.velocity = new Vector2(_moveDirection.x * (_defaultSpeed + speedBuff), _moveDirection.y * (_defaultSpeed + speedBuff));
    // thisRB.MovePosition(thisRB.position + _movementInput * topSpeed * Time.fixedDeltaTime);
    #endregion

    #region Shooting

    //player follows mouse and rotates
    Vector2 aimDirection = _mousePosition - thisRB.position;
    float aimAngle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg - 90f;
    thisRB.rotation = aimAngle;
    //doshooting();
    #endregion
  }
}
