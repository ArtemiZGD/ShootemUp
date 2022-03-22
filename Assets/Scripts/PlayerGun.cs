using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class PlayerGun : MonoBehaviour
{
    [SerializeField] private float volume = 0.2f;
    [SerializeField] private Text score;
    [SerializeField] private Text ammo;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject bullet;

    private AudioSource audioSource;
    private int bullets = 50;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = SettingsScript.sounds * volume;
        SettingsScript.bullets = bullets;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && !SettingsScript.pause && SettingsScript.bullets > 0)
        {
            Shot();
        }
        ammo.text = "Ammo: " + SettingsScript.bullets;
        score.text = "Score: " + SettingsScript.score;
    }

    private void Shot()
    {
        Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit);
        Instantiate(bullet, transform.position, 
            Quaternion.LookRotation(new Vector3(hit.point.x - transform.position.x, 0, hit.point.z - transform.position.z)));
        SettingsScript.bullets--;

        audioSource.Play();
    }
}
