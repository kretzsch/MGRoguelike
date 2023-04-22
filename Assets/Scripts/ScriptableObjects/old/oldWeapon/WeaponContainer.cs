using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WeaponContainer : MonoBehaviour
{

  public GameObject bullet;
  public WeaponSO weapon;
  public Transform firePoint;
  public float fireForce;
  public TMP_Text text;
  //remove hardcoded  parts.
  public int currentClip = 10, maxClipSize = 10, currentAmmo, maxAmmoSize = 100;

  void Start()
  {
    #region scriptableobj start
    weapon.fireForce = fireForce;
    weapon.bulletType = bullet;
    text.text = $"{currentClip}/{maxClipSize}{currentAmmo}";
    #endregion
  }
  private void Update()
  {
    // do fire weapon and reload here instead of player controller
    // so it doesnt matter which weapon is active
    if (Input.GetMouseButtonDown(0)) { FireWeapon(); }
    if (Input.GetKeyDown(KeyCode.R)) { ReloadWeapon(); }
    text.text = $"{currentClip}/{maxClipSize}\n{currentAmmo}";
  }

  public void FireWeapon()
  {
    //TODO: exchange this for object pooling here
    if (gameObject == isActiveAndEnabled)
    {
      if (currentClip > 0)
      {
        GameObject projectile = Instantiate(bullet, firePoint.position, firePoint.rotation);
        projectile.GetComponent<Rigidbody2D>().AddForce(firePoint.up * fireForce, ForceMode2D.Impulse);
        currentClip--;
      }
    }
  }

  public void ReloadWeapon()
  {
    int reloadAmount = maxClipSize - currentClip; //how many bullets to refill clip
    reloadAmount = (currentAmmo - reloadAmount) >= 0 ? reloadAmount : currentAmmo;
    currentClip += reloadAmount;
    currentAmmo -= reloadAmount;
  }

  public void AddAmmo(int ammoAmount)
  {
    currentAmmo += ammoAmount;
    if (currentAmmo > maxAmmoSize)
    {
      currentAmmo = maxAmmoSize;
    }
  }
}
