using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private void Update()
    {
        if (!SettingsScript.Pause)
        {
            transform.position += transform.forward * SettingsScript.BulletSpeed * Time.deltaTime;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Barrier")
        {
            Destroy(this.gameObject);
        }
    }

}
