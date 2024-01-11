using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HealthScript : MonoBehaviour
{
    [Header ("Health Variables")]
    [SerializeField] private float currentHealth = 100.0f;
    [SerializeField] private float maxHealth = 100.0f;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            Damage(10);
        }
    }

    private void Damage(int amount)
    {
        if (amount < 0)
        {
            throw new System.ArgumentOutOfRangeException("Negative DMG");
        }

        currentHealth -= amount;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
        Debug.Log("You Died");
    }
}
