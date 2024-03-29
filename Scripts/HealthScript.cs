using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HealthScript : MonoBehaviour
{
    [Header ("Health Variables")]
    [SerializeField] private float currentHealth;
    [SerializeField] private float maxHealth = 100.0f;

    [Header ("Other Scripts")]
    public HealthBarScript HealthBar;

    void Start()
    {
        currentHealth = maxHealth;
        HealthBar.SetMaxHealth(maxHealth);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            Damage(10);
        }
    }

    public void Damage(int amount)
    {
        if (amount < 0)
        {
            throw new System.ArgumentOutOfRangeException("Negative DMG");
        }

        currentHealth -= amount;
        StartCoroutine(VisualIndicatorCoroutine(Color.red));
        HealthBar.SetHealth(currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        //GetComponent<LootBagScript>().InstantiateLoot(transform.position);
        Destroy(gameObject);
        Debug.Log("You Died");
    }

    private IEnumerator VisualIndicatorCoroutine(Color color)
    {
        GetComponent<SpriteRenderer>().color = color;
        yield return new WaitForSeconds(0.1f);
        GetComponent<SpriteRenderer>().color = Color.white;
        yield return new WaitForSeconds(0.1f);
        GetComponent<SpriteRenderer>().color = color;
        yield return new WaitForSeconds(0.1f);
        GetComponent<SpriteRenderer>().color = Color.white;
    }
}
