using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerkSpawn : MonoBehaviour
{
    // Prefabs for the perks
    public GameObject energyRefill;
    public GameObject healthRefill;
    public GameObject bulletRefill;


    private float xMin=-630, xMax=-65;
    private float zMin=-269, zMax = 12;

    const float y = 14f;


    public int numberOfPerksToSpawn = 25;

    // Start is called before the first frame update
    void Start()
    {
        SpawnPerks();
    }

    // spawn
    void SpawnPerks()
    {
        // get random pos
        Vector3 GetRandomPosition()
        {
            float randomX = Random.Range(xMin, xMax);
            float randomZ = Random.Range(zMin, zMax);
            return new Vector3(randomX, y, randomZ);
        }

        for (int i = 0; i < numberOfPerksToSpawn; i++)
        {
            Vector3 randomPosition = GetRandomPosition();
            GameObject perkToSpawn = GetRandomPerk();
            Instantiate(perkToSpawn, randomPosition, Quaternion.identity);
        }
    }



    GameObject GetRandomPerk()
    {
        int randomIndex = Random.Range(0, 3); // 0 = energy, 1 = health, 2 = bullets

        switch (randomIndex)
        {
            case 0:
                return energyRefill;
            case 1:
                return healthRefill;
            case 2:
                return bulletRefill;
            default:
                return null;
        }
    }
}
