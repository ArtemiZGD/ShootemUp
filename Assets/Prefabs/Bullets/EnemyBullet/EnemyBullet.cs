using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    private PlayerHealth playerHealth;
    private GameObject player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<PlayerHealth>();
    }

    void Update()
    {
        if (!SettingsScript.pause)
        {
            transform.position += transform.forward * SettingsScript.bulletSpeed * Time.deltaTime;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if ( other.tag == "Barrier")
        {
            Destroy(this.gameObject);
        }
        else if (other.tag == "Player")
        {
            playerHealth.HealthDown();
            Destroy(this.gameObject);
        }
    }
}
