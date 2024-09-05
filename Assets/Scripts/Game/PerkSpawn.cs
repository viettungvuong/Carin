using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerkSpawn : MonoBehaviour
{
    // Prefabs for the perks
    public GameObject energyRefill;
    public GameObject healthRefill;
    public GameObject bulletRefill;


    public Vector2 spawnAreaMin;
    public Vector2 spawnAreaMax;


    public int numberOfPerksToSpawn = 5;

    // Start is called before the first frame update
    void Start()
    {
        SpawnPerks();
    }

    // spawn
    void SpawnPerks()
    {
        for (int i = 0; i < numberOfPerksToSpawn; i++)
        {
            Vector2 randomPosition = GetRandomPosition();
            GameObject perkToSpawn = GetRandomPerk();
            Instantiate(perkToSpawn, randomPosition, Quaternion.identity);
        }
    }

    // get random pos
    Vector2 GetRandomPosition()
    {
        float randomX = Random.Range(spawnAreaMin.x, spawnAreaMax.x);
        float randomY = Random.Range(spawnAreaMin.y, spawnAreaMax.y);
        return new Vector2(randomX, randomY);
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
