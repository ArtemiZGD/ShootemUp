using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Fighter : MonoBehaviour
{
    [SerializeField] private GameObject explosion;
    [SerializeField] private int scoreForKill = 1;
    [SerializeField] private int hp = 3;
    [SerializeField] private float shotDelay = 1;
    [SerializeField] private float shotDistance;

    [SerializeField] private float rotationSpeed = 1;

    [SerializeField] private GameObject bullet;

    private PlayerHealth playerHealth;
    private NavMeshAgent agent;
    private GameObject player;

    private bool canShoot;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<PlayerHealth>();

        StartCoroutine("Shooting");
    }

    private void Update()
    {
        if (Physics.Raycast(transform.position, new Vector3(player.transform.position.x - transform.position.x, 0,
            player.transform.position.z - transform.position.z), out RaycastHit hit, shotDistance))
        {
            if (hit.collider.gameObject.tag == "Player")
            {
                transform.rotation = Quaternion.Lerp(transform.rotation,
                    Quaternion.LookRotation(new Vector3(
                    player.transform.position.x - transform.position.x, 0,
                    player.transform.position.z - transform.position.z)), 
                    Time.deltaTime * rotationSpeed);
            }
        }

        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit2, shotDistance))
        {
            if (hit2.collider.gameObject.tag == "Player")
            {
                Shot();
                agent.destination = transform.position;
            }
            else
            {
                agent.destination = player.transform.position;
                canShoot = false;
            }
        }
        else
        {
            agent.destination = player.transform.position;
            canShoot = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Bullet")
        {
            Destroy(other.gameObject);
            HealthDown();
        }
        else if (other.tag == "Player")
        {
            playerHealth.HealthDown();
            Death();
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

    private void Shot()
    {
        canShoot = true;
    }

    IEnumerator Shooting()
    {
        while (true)
        {
            yield return new WaitForSeconds(shotDelay);

            if (canShoot)
            {
                Instantiate(bullet, transform.position, transform.rotation);
            }
        }
    }

    private void DeathWithScore()
    {
        SettingsScript.score += scoreForKill;
        Death();
    }

    private void Death()
    {
        Instantiate(explosion, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
