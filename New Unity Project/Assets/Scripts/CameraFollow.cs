using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    public Vector3 offset;

    private void Awake()
    {
        player = FindObjectOfType<Player>().transform;
    }

    void Update()
    {
        transform.position = player.position + offset;
    }
}
