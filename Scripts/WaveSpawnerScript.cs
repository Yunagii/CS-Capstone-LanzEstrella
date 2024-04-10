using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
[System.Serializable]

public class Wave
{
    public string waveName;
    public int enemyTotal;
    public GameObject[] enemyType;
    public float spawnInterval;
}

public class WaveSpawnerScript : MonoBehaviour
{
    [Header ("Unity Components")]
    [SerializeField] Animator animator;
    [SerializeField] TextMeshProUGUI WaveName;
    [SerializeField] Wave[] waves;
    [SerializeField] Transform[] spawnPoints;

    [Header ("Variables")]
    private Wave currentWave;
    private int currentWaveNumber;
    private float nextSpawnTime;
    private bool canSpawn = true;
    private bool canAnimate = false;

    private void Update()
    {
        currentWave = waves[currentWaveNumber];
        SpawnWave();
        GameObject[] totalEnemies = GameObject.FindGameObjectsWithTag("Hostile");

        if (totalEnemies.Length == 0)
        {
            if (currentWaveNumber+1 != waves.Length)
            {
                if(canAnimate)
                {
                    WaveName.text = waves[currentWaveNumber + 1].waveName;
                    animator.SetTrigger("Wave Complete");
                    canAnimate = false;
                }   
            }
            else
            {
            // add restart function
            SceneManager.LoadScene("Victory");
            Debug.Log("Game Finish");
            }
        }   
    }

    private void SpawnNextWave()
    {
        currentWaveNumber++;
        canSpawn = true;
    }

    private void SpawnWave()
    {
        if (canSpawn && nextSpawnTime < Time.time)
        {
            GameObject RandomEnemy = currentWave.enemyType[Random.Range(0, currentWave.enemyType.Length)];
            Transform randomPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
            Instantiate(RandomEnemy, randomPoint.position, Quaternion.identity);
            currentWave.enemyTotal--;
            nextSpawnTime = Time.time + currentWave.spawnInterval;
            if (currentWave.enemyTotal == 0)
            {
                canSpawn = false;
                canAnimate = true;
            }
        }
    }
}
