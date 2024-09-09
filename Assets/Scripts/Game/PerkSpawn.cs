using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerkSpawn : MonoBehaviour
{
    public GameObject energyRefill;
    public GameObject healthRefill;
    public GameObject bulletRefill;
    public GameObject moneyRefill;

    private float xMin = -630, xMax = 330;
    private float zMin = -269, zMax = 12;

    public int numberOfPerksToSpawn = 25;

    [SerializeField] LayerMask cityGroundLayer; 

    void Start()
    {
        SpawnPerks();
    }

    void SpawnPerks()
    {
        Vector3 GetRandomPosition()
        {
            float randomX = Random.Range(xMin, xMax);
            float randomZ = Random.Range(zMin, zMax);

            // raycast downward to find CityGround layer position => y position
            Ray ray = new Ray(new Vector3(randomX, 10f, randomZ), Vector3.down);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, cityGroundLayer))
            {
                return new Vector3(randomX, hit.point.y + 0.5f, randomZ);
            }

            // if raycast fail
            return new Vector3(randomX, 5.5f, randomZ);
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
        int randomIndex = Random.Range(0, 4); 

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
