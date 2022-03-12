using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPos : MonoBehaviour
{
    private Transform player;

    public static PlayerPos instance;

    void Start()
    {
        instance = this;

        player = GameObject.FindGameObjectWithTag("Player").transform;
        if(player != null)
        {
            CheckPoint();
        }
    }

    public void CheckPoint()
    {
        Vector3 playerPos = transform.position;
        playerPos.z = 0f;
        player.position = playerPos;
    }
}
