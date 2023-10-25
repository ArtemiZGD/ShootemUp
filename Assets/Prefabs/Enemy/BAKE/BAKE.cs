using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BAKE : MonoBehaviour
{
    [SerializeField] private GameObject explosion;
    [SerializeField] private int scoreForKill = 1;
    [SerializeField] private int hp = 3;
    private NavMeshAgent agent;
    private GameObject player;

    private PlayerHealth playerHealth;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<PlayerHealth>();
    }

    private void FixedUpdate()
    {
        agent.destination = player.transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            playerHealth.HealthDown();
            Death();
        }
        else if(other.tag == "Bullet")
        {
            Destroy(other.gameObject);
            HealthDown();
        }
    }

    private void HealthDown()
    {
        if (hp > 1)
        {
            hp--;
        }
        else
        {
            DeathWithScore();
        }
    }

    private void DeathWithScore()
    {
        SettingsScript.Score += scoreForKill;
        Death();
    }

    private void Death()
    {
        Instantiate(explosion, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
