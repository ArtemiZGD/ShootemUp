using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnEnemy : MonoBehaviour
{
    [SerializeField] private float chanceToSpawnAmmo = 0.5f;
    [SerializeField] private float chanceToSpawnMed = 0.1f;
    private bool ammoSpawned;
    private bool medSpawned;

    [Min(22)]
    [SerializeField] private float spawnDistance = 22f;

    [SerializeField] private List<GameObject> spawnPoint;
    [SerializeField] private GameObject enemy1;
    [SerializeField] private GameObject enemy2;
    [SerializeField] private GameObject enemy3;
    [SerializeField] private GameObject turret;

    [SerializeField] private GameObject ammo;
    [SerializeField] private GameObject med;

    [SerializeField] private GameObject boxesPrefab;
    private GameObject boxes;

    [SerializeField] private AnimationCurve enemyCount;
    [SerializeField] private AnimationCurve chanceToSpawnEnemy1;
    [SerializeField] private AnimationCurve chanceToSpawnEnemy2;
    [SerializeField] private AnimationCurve chanceToSpawnEnemy3;
    [SerializeField] private AnimationCurve chanceToSpawnTurret;

    [SerializeField] private Text enemies;
    [SerializeField] private Text wave;

    private GameObject player;
    private List<GameObject> spawnPointToUse = new List<GameObject>();
    private int waveNum;

    private void Start()
    {
        boxes = Instantiate(boxesPrefab);
        player = GameObject.FindGameObjectWithTag("Player");
        waveNum = 0;
        NewWave();
    }

    private void Update()
    {
        if (transform.childCount == 0)
        {
            NewWave();
        }
        enemies.text = "Enemies left: " + transform.childCount;
        wave.text = "Wave: " + waveNum;
    }

    private void NewWave()
    {
        Destroy(boxes);
        boxes = Instantiate(boxesPrefab);

        ammoSpawned = false;
        medSpawned = false;

        for (int i = 0; i < spawnPoint.Count; i++)
        {
            spawnPointToUse.Add(spawnPoint[i]);
        }
        waveNum++;

        for (int i = 0; i < enemyCount.Evaluate(waveNum); i++)
        {
            SpawnNewEnemy();
        }
        for (int i = 0; i < spawnPointToUse.Count; i++)
        {
            int randomSpawnPoint = Random.Range(0, spawnPointToUse.Count);

            if ((player.transform.position - spawnPointToUse[randomSpawnPoint].transform.position).magnitude < spawnDistance)
            {
                if (!ammoSpawned)
                {
                    Instantiate(ammo, spawnPointToUse[randomSpawnPoint].transform.position, Quaternion.identity, boxes.transform);
                    ammoSpawned = true;
                }
                else if (!medSpawned)
                {
                    Instantiate(med, spawnPointToUse[randomSpawnPoint].transform.position, Quaternion.identity, boxes.transform);
                    medSpawned = true;
                }
                spawnPointToUse.RemoveAt(randomSpawnPoint);
            }
        }
    }

    private void SpawnNewEnemy()
    {
        float randomNum = Random.Range(0, 
            chanceToSpawnEnemy1.Evaluate(waveNum) + 
            chanceToSpawnEnemy2.Evaluate(waveNum) + 
            chanceToSpawnEnemy3.Evaluate(waveNum) +
            chanceToSpawnTurret.Evaluate(waveNum));

        int randomSpawnPoint = 0;

        if (Random.Range(0, 1.0f) > chanceToSpawnAmmo)
        {
            ammoSpawned = true;
        }
        if (Random.Range(0, 1.0f) > chanceToSpawnMed)
        {
            medSpawned = true;
        }

        while (spawnPointToUse.Count > 0)
        {
            randomSpawnPoint = Random.Range(0, spawnPointToUse.Count);

            if ((player.transform.position - spawnPointToUse[randomSpawnPoint].transform.position).magnitude < spawnDistance)
            {
                if (!ammoSpawned)
                {
                    Instantiate(ammo, spawnPointToUse[randomSpawnPoint].transform.position, Quaternion.identity, boxes.transform);
                    ammoSpawned = true;
                }
                else if (!medSpawned)
                {
                    Instantiate(med, spawnPointToUse[randomSpawnPoint].transform.position, Quaternion.identity, boxes.transform);
                    medSpawned = true;
                }
                spawnPointToUse.RemoveAt(randomSpawnPoint);
            }
            else
            {
                break;
            }
        } 


        if (spawnPointToUse.Count > 0)
        {
            if (randomNum < chanceToSpawnEnemy1.Evaluate(waveNum))
            {
                Instantiate(enemy1, spawnPointToUse[randomSpawnPoint].transform.position, Quaternion.identity, transform);
            }
            else if (randomNum < chanceToSpawnEnemy2.Evaluate(waveNum) + chanceToSpawnEnemy1.Evaluate(waveNum))
            {
                Instantiate(enemy2, spawnPointToUse[randomSpawnPoint].transform.position, Quaternion.identity, transform);
            }
            else if (randomNum < chanceToSpawnEnemy3.Evaluate(waveNum) + chanceToSpawnEnemy2.Evaluate(waveNum) + chanceToSpawnEnemy1.Evaluate(waveNum))
            {
                Instantiate(enemy3, spawnPointToUse[randomSpawnPoint].transform.position, Quaternion.identity, transform);
            }
            else
            {
                Instantiate(turret, spawnPointToUse[randomSpawnPoint].transform.position, Quaternion.identity, transform);
            }

            spawnPointToUse.RemoveAt(randomSpawnPoint);
        }
    }
}
