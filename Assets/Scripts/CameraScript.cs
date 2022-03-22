using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    [SerializeField] private float moveSpeed;

    private GameObject player;
    private Vector3 startPosition;

    private void Start()
    {
        startPosition = transform.position;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        transform.position = Vector3.Lerp(transform.position, player.transform.position + startPosition, Time.deltaTime * moveSpeed);
    }
}
