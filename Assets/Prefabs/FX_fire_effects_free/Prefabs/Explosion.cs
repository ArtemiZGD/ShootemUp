using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    private AudioSource audioSource;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = SettingsScript.sounds;
        StartCoroutine("Death");
    }

    IEnumerator Death()
    {
        audioSource.Play();
        yield return new WaitForSeconds(3);
        Destroy(gameObject);
    }
}
