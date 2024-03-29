using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Tilemaps;
using UnityEditor.U2D.Aseprite;
using UnityEngine;

public class AIChaseScript : MonoBehaviour
{
    [Header ("Variables")]
    public GameObject Player;
    public float speed;
    public float distance;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector2.Distance(transform.position, Player.transform.position);
        Vector2 direction = Player.transform.position - transform.position;
        direction.Normalize();

        transform.position = Vector2.MoveTowards(this.transform.position, Player.transform.position, speed * Time.deltaTime);

        if (direction.x > 0)
        {
            this.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        }

        else
        {
            this.transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
        }
    }
}
