using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PBR : MonoBehaviour
{
    [SerializeField] private int scoreForKill = 3;
    [SerializeField] private int hp = 3;
    [SerializeField] private float shotDelay = 0.3f;
    [SerializeField] private float shotAnimDelay = 0.2f;
    [SerializeField] private float shotDistance;


    [SerializeField] private float verticOffset = 1.5f;
    [SerializeField] private float horizintOffset = 1f;

    [SerializeField] private GameObject bullet;
    [SerializeField] private GameObject gun;

    private Animator animator;
    private PlayerHealth playerHealth;
    private NavMeshAgent agent;
    private GameObject player;
    private bool canShoot;
    private bool death;

    private bool run;
    private bool idle;

    void Start()
    {
        ResetAnim();

        death = false;
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<PlayerHealth>();

        StartCoroutine("Shooting");
    }

    private void Update()
    {
        if (!death)
        {
            if (Physics.Raycast(transform.position + Vector3.up, player.transform.position - transform.position, out RaycastHit hit, shotDistance))
            {
                if (hit.collider.gameObject.tag == "Player")
                {
                    Shot();
                    agent.destination = transform.position;
                    if (!idle)
                    {
                        ResetAnim();
                        idle = true;
                        animator.SetTrigger("Idle");
                    }
                }
                else
                {
                    agent.destination = player.transform.position;
                    canShoot = false;
                    if (!run)
                    {
                        ResetAnim();
                        run = true;
                        animator.SetTrigger("Run");
                    }
                }
            }
            else
            {
                agent.destination = player.transform.position;
                canShoot = false;
                if (!run)
                {
                    ResetAnim();
                    run = true;
                    animator.SetTrigger("Run");
                }
            }

            if (canShoot)
            {
                transform.LookAt(player.transform, Vector3.up);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!death)
        {
            if (other.tag == "Bullet")
            {
                Destroy(other.gameObject);
                HealthDown();
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
            StartCoroutine("Death");
        }
    }

    private void Shot()
    {
        canShoot = true;
    }

    IEnumerator Shooting()
    {
        while (!death)
        {
            yield return new WaitForSeconds(shotDelay);

            if (canShoot && !death)
            {
                animator.SetTrigger("Shot");
                yield return new WaitForSeconds(shotAnimDelay);
                Instantiate(bullet, transform.position + Vector3.up * verticOffset + transform.right * horizintOffset, transform.rotation);
            }
        }
    }

    IEnumerator Death()
    {
        animator.ResetTrigger("Run");
        animator.ResetTrigger("Idle");
        animator.ResetTrigger("Shot");

        SettingsScript.Score += scoreForKill;
        Destroy(agent);
        death = true;
        animator.SetTrigger("Death");

        yield return new WaitForSeconds(2);

        Destroy(gameObject);

        yield return null;
    }

    private void ResetAnim()
    {
        run = false;
        idle = false;
    }
}
