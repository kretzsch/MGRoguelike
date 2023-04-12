using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public int currentHealth;
    public int maxHealth = 100;
    public HealthBar healthBar;

    private void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    private void Update()
    {
        //show health to healthbar.

        if (Input.GetKeyDown(KeyCode.Space))
        {
            currentHealth -= 20;
        }
        healthBar.SetHealth(currentHealth);
    }
}
