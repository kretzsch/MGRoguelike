using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStats : MonoBehaviour, IDamageable
{
    public int currentHealth;
    public int maxHealth = 100;
    public HealthBar healthBar;
    private bool _isDead = false;

    private void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    private void Update()
    {
        //show health to healthbar.
        healthBar.SetHealth(currentHealth);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0 && !_isDead)
        {
            _isDead = true;
            Die();
        }
    }

    public bool IsDead()
    {
        return _isDead;
    }

    private void Die()
    {
        //do death things like animation / sound whatever. 

        //quick hack to go to death screen 
        SceneManager.LoadScene("YouLost!");
    }

}