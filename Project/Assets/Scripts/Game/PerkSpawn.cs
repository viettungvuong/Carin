using System.Collections;
using UnityEngine;

public class PerkSpawn : MonoBehaviour
{
    public GameObject energyRefill;
    public GameObject healthRefill;
    public GameObject bulletRefill;
    public GameObject moneyRefill;


    private float spawnDistance = 100f; 
    private Vector3 lastPlayerPosition;
    public float spawnRadius = 50f; 

    public int numberOfPerksToSpawn = 25;

    [SerializeField] LayerMask cityGroundLayer;

    private Transform playerTransform;

    private void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        SpawnPerks();
        lastPlayerPosition = playerTransform.position;
    }

    private void Update()
    {
        float distanceMoved = Vector3.Distance(lastPlayerPosition, playerTransform.position);

        if (distanceMoved >= spawnDistance)
        {
            SpawnPerks();
            lastPlayerPosition = playerTransform.position;
        }
    }

    void SpawnPerks()
    {
        for (int i = 0; i < numberOfPerksToSpawn; i++)
        {
            Vector3 randomPosition = GetRandomPositionAroundPlayer();
            GameObject perkToSpawn = GetRandomPerk();
            float randomYAngle = Random.Range(0f, 360f);
            Quaternion rotation = Quaternion.Euler(0, randomYAngle, 0);
            Instantiate(perkToSpawn, randomPosition, rotation);
        }
    }

    Vector3 GetRandomPositionAroundPlayer()
    {
        Vector2 randomCircle = Random.insideUnitCircle * spawnRadius;
        Vector3 randomPosition = new Vector3(randomCircle.x, 10f, randomCircle.y) + playerTransform.position;

        // Raycast downward to find CityGround layer position => y position
        Ray ray = new Ray(randomPosition, Vector3.down);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, cityGroundLayer))
        {
            return new Vector3(randomPosition.x, hit.point.y + 0.5f, randomPosition.z);
        }

        // If raycast fails
        return new Vector3(randomPosition.x, 6f, randomPosition.z);
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
