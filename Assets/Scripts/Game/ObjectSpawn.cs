using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ObjectSpawn : MonoBehaviour
{
    protected int spawnNumber;
    protected string objectTag;

    public float spawnRadius = 5f;
    public float spawnDistance=20f;
    public LayerMask zombieLayer;

    protected Vector3 lastPosition;

    protected void Start() {
        lastPosition = transform.position;
    }



    protected void Spawn()
    {
        for (int i = 0; i < spawnNumber; i++)
        {
            Vector3 spawnPosition;
            int attempts = 0;

            do
            {
                spawnPosition = lastPosition + Random.insideUnitSphere * spawnRadius;
                spawnPosition.y = ObjectPool.ObjectPosition(objectTag).y - 2;
                attempts++;

            } while (Physics.OverlapSphere(spawnPosition, 1.5f, zombieLayer).Length > 0 && attempts < 200);

            if (attempts >= 200)
            {
                continue;
            }



            GameObject spawned = ObjectPool.SpawnFromPool(objectTag, spawnPosition);
            spawned.SetActive(true);
        }
    }
}
