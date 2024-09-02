using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawn : ObjectSpawn
{


    void Awake() {
        objectTag = "Zombie";
        spawnNumber = 10;
    }

    void Update()
    {
        // distance player has moved
        float distanceMoved = Vector3.Distance(lastPosition, transform.position);
        Debug.Log(distanceMoved);

        // player has move more than spawn distance
        if (distanceMoved >= spawnDistance)
        {
            Spawn();
            lastPosition = transform.position;
        }
    }
}
