using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponContainer : MonoBehaviour
{

  public GameObject bullet;
  public WeaponSO weapon;
  public Transform firePoint;
  public float fireForce;

  void Start()
  {
    #region scriptableobj start
    weapon.fireForce = fireForce;
    weapon.bulletType = bullet;
    #endregion
  }
  private void Update()
  {
    // do fire weapon here instead of player controller
    // so it doesnt matter which weapon is active
    if (Input.GetMouseButtonDown(0))
    {
      FireWeapon();
    }
  }

  public void FireWeapon()
  {
    if (gameObject == isActiveAndEnabled)
    {
      GameObject projectile = Instantiate(bullet, firePoint.position, firePoint.rotation);
      projectile.GetComponent<Rigidbody2D>().AddForce(firePoint.up * fireForce, ForceMode2D.Impulse);
    }
  }
}
