using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ZombieSpawn : ObjectSpawn
{


    void Awake() {
        objectTag = "Zombie";
        spawnNumber = 5;
    }

    private new void Start() {
        base.Start();
        Spawn();
    }

    void Update()
    {
        // distance player has moved
        float distanceMoved = Vector3.Distance(lastPosition, transform.position);

        // player has move more than spawn distance
        if (distanceMoved >= spawnDistance)
        {
            spawnNumber = 5 * PlayerLevel.Instance.GetLevel(); 
            Spawn();
            lastPosition = transform.position;
        }
    }
}
