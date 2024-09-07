using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerkSpawn : MonoBehaviour
{
    // Prefabs for the perks
    public GameObject energyRefill;
    public GameObject healthRefill;
    public GameObject bulletRefill;
    public GameObject moneyRefill;


    private float xMin=-630, xMax=330;
    private float zMin=-269, zMax = 12;

    const float y = 5.2f;


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
            float randomYAngle = Random.Range(0f, 360f);
            Quaternion rotation = Quaternion.Euler(0, randomYAngle, 0);
            Instantiate(perkToSpawn, randomPosition, rotation);
        }
    }



    GameObject GetRandomPerk()
    {
        int randomIndex = Random.Range(0, 4); // 0 = energy, 1 = health, 2 = bullets, 3 = money

        switch (randomIndex)
        {
            case 0:
                return energyRefill;
            case 1:
                return healthRefill;
            case 2:
                return bulletRefill;
            case 3:
                return moneyRefill;
            default:
                return null;
        }
    }
}
