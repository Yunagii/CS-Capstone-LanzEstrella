using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Tilemaps;
using UnityEditor.U2D.Aseprite;
using UnityEngine;

public class AIChaseScript : MonoBehaviour
{
    [Header ("Variables")]
    [SerializeField] private float speed = 1.5f;
    private GameObject Target;

    void Awake()
    {
        Target = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, Target.transform.position, speed * Time.deltaTime);
    }
}
