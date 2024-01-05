using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float health = 100;
    [SerializeField] private float maxHealth = 100;

    // Update is called once per frame
    private void Update()
    {
        /*
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            Damage(10);
        }

        if (Input.GetKeyDown(KeyCode.Equals))
        {
            Heal(10);
        }
        */
    }

    public void Damage(int amount)
    {
        if (amount < 0)
        {
            throw new System.ArgumentOutOfRangeException("Cannot hvae negative Damage");
        }

        this.health -= amount;

        if (health <= 0)
        {
            Die();
        }
    }

    public void Heal(int amount)
    {
        if (amount < 0)
        {
            throw new System.ArgumentOutOfRangeException("Cannot hvae negative Healing");
        }

        if (health + amount > maxHealth)
        {
            this.health = maxHealth;
        } 
        else
        {
            this.health += amount;
        }
    }

    private void Die()
    {
        Debug.Log("You are dead");
        Destroy(gameObject);
    }
}
