using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class HealthScript : MonoBehaviour
{
    [Header ("Health Variables")]
    [SerializeField] private float currentHealth;
    [SerializeField] private float maxHealth = 100.0f;

    [Header ("Other Scripts")]
    public HealthBarScript HealthBar;

    void Awake()
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
        else if (Input.GetKeyDown(KeyCode.K))
        {
            if (gameObject.tag == "Hostile")
            {
                Destroy(gameObject);
            }
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

        if (gameObject.tag == "Player")
        {
            SceneManager.LoadScene("Death");
        }   
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
