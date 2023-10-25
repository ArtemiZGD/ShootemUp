using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultTurret : MonoBehaviour
{
    [SerializeField] private GameObject explosion;
    [SerializeField] private int scoreForKill = 3;
    [SerializeField] int hp = 5;
    [SerializeField] float shootingDelay = 1f;
    [SerializeField] float shootingDistance = 10f;
    [SerializeField] GameObject head;
    [SerializeField] GameObject bullet;

    [SerializeField] GameObject gun1;
    [SerializeField] GameObject gun2;

    private GameObject player;
    private bool canShoot;

    void Start()
    {
        canShoot = false;
        player = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine("Shot");
    }

    void Update()
    {
        head.transform.rotation = Quaternion.LookRotation(new Vector3(
            player.transform.position.x - head.transform.position.x, 0, 
            player.transform.position.z - head.transform.position.z));

        if (Physics.Raycast(head.transform.position, 
            new Vector3(
            player.transform.position.x - head.transform.position.x, 0, 
            player.transform.position.z - head.transform.position.z), 
            out RaycastHit hit, shootingDistance))
        {
            if (hit.collider.gameObject.tag == "Player")
            {
                canShoot = true; 
            }
            else
            {
                canShoot = false;
            }
        }
        else
        {
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
    }

    IEnumerator Shot()
    {
        while(true)
        {
            yield return new WaitForSeconds(shootingDelay);

            if (canShoot)
            {
                Instantiate(bullet, gun1.transform.position, head.transform.rotation);
            }

            yield return new WaitForSeconds(shootingDelay);

            if (canShoot)
            {
                Instantiate(bullet, gun2.transform.position, head.transform.rotation);
            }
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
