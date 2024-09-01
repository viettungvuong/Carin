using UnityEngine;
using UnityEngine.AI;

public class ZombieAI : MonoBehaviour
{
    public Transform player;
    public float detectionRange = 10f;
    public float attackRange = 2f;
    public float wanderRadius = 5f;
    public float wanderTime = 3f;

    private NavMeshAgent agent;
    private float timer;

    private enum ZombieState { Idle, Wandering, Chasing, Attacking }
    private ZombieState currentState = ZombieState.Idle;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        timer = wanderTime;
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        switch (currentState)
        {
            case ZombieState.Idle:
                if (distanceToPlayer <= detectionRange)
                {
                    currentState = ZombieState.Chasing;
                }
                else
                {
                    currentState = ZombieState.Wandering;
                }
                break;

            case ZombieState.Wandering:
                timer += Time.deltaTime;

                if (timer >= wanderTime)
                {
                    Vector3 newPos = RandomNavSphere(transform.position, wanderRadius, -1);
                    agent.SetDestination(newPos);
                    timer = 0;
                }

                if (distanceToPlayer <= detectionRange)
                {
                    currentState = ZombieState.Chasing;
                }
                break;

            case ZombieState.Chasing:
                agent.SetDestination(player.position);

                if (distanceToPlayer <= attackRange)
                {
                    currentState = ZombieState.Attacking;
                }
                else if (distanceToPlayer > detectionRange)
                {
                    currentState = ZombieState.Wandering;
                }
                break;

            case ZombieState.Attacking:
                Debug.Log("Zombie is attacking!");

                if (distanceToPlayer > attackRange)
                {
                    currentState = ZombieState.Chasing;
                }
                break;
        }
    }

    public static Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask)
    {
        Vector3 randDirection = Random.insideUnitSphere * dist;
        randDirection += origin;
        NavMeshHit navHit;
        NavMesh.SamplePosition(randDirection, out navHit, dist, layermask);
        return navHit.position;
    }
}