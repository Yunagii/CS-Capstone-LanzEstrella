using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackAreaScript : MonoBehaviour
{
    [SerializeField] private int attackDMG= 10;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.GetComponent<HealthScript>() != null)
        {
            HealthScript health = collider.GetComponent<HealthScript>();
            health.Damage(attackDMG);
            Debug.Log("HIT");
        }
    }
}
