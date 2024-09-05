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
    private Animator animator;

    private enum ZombieState { Idle, Wandering, Chasing, Attacking }
    private ZombieState currentState = ZombieState.Idle;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        timer = wanderTime;

        animator = GetComponent<Animator>();
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        switch (currentState)
        {
            case ZombieState.Idle:
                animator.SetTrigger("idle");
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
                animator.SetTrigger("walk");
                animator.ResetTrigger("idle");
                animator.ResetTrigger("attack1");
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
                animator.SetTrigger("walk");
                animator.ResetTrigger("idle");
                animator.ResetTrigger("attack1");
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
                animator.ResetTrigger("idle");
                animator.ResetTrigger("walk");
                animator.SetTrigger("attack1");

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