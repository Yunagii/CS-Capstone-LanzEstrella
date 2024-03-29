using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackAreaScript : MonoBehaviour
{
    [SerializeField] private int attackDMG= 10;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.GetComponent<HealthScript>() != null)
        {
            if (collider.gameObject.CompareTag("Player"))
            {
                HealthScript health = collider.GetComponent<HealthScript>();
                health.Damage(attackDMG);
                Debug.Log("HIT");
            }          
        }
    }
}
