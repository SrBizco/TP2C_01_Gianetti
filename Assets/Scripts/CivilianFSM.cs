using UnityEngine;
using UnityEngine.AI;
using System;

public class CivilianFSM : Person
{
    public CivilianType civilianType;
    public float idleTime = 2f;
    public float patrolRadius = 10f;
    public Action OnDeath;

    private enum State { Idle, Patrolling }
    private State currentState = State.Idle;

    private NavMeshAgent agent;
    private float idleTimer;
    private Animator animator;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        idleTimer = idleTime;
        animator.SetBool("isWalking", false);
    }

    void Update()
    {
        switch (currentState)
        {
            case State.Idle:
                HandleIdle();
                break;
            case State.Patrolling:
                HandlePatrol();
                break;
        }
    }

    void HandleIdle()
    {
        animator.SetBool("isWalking", false);

        idleTimer -= Time.deltaTime;
        if (idleTimer <= 0f)
        {
            idleTimer = idleTime;
            Vector3 randomDirection = UnityEngine.Random.insideUnitSphere * patrolRadius;
            randomDirection += transform.position;

            if (NavMesh.SamplePosition(randomDirection, out NavMeshHit hit, patrolRadius, NavMesh.AllAreas))
            {
                agent.SetDestination(hit.position);
                currentState = State.Patrolling;
                animator.SetBool("isWalking", true);
            }
        }
    }

    void HandlePatrol()
    {
        animator.SetBool("isWalking", true);

        if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            currentState = State.Idle;
            animator.SetBool("isWalking", false);
        }
    }

    public override void Die()
    {
        if (civilianType == CivilianType.Good)
        {
            Debug.Log("¡Penalización por matar un civil inocente!");
            FindObjectOfType<GameManager>().DecreaseScore();
        }
        else
        {
            Debug.Log("¡Puntos por eliminar a un criminal!");
            FindObjectOfType<GameManager>().EliminateBadCivilian();
        }

        Collider col = GetComponent<Collider>();
        if (col != null) col.enabled = false;

        
        OnDeath?.Invoke();

        
        if (civilianType == CivilianType.Good && CivilianPool.Instance != null)
            CivilianPool.Instance.ReturnCivilianToPool(gameObject);
        else if (civilianType == CivilianType.Bad && BadCivilianPool.Instance != null)
            BadCivilianPool.Instance.ReturnCivilianToPool(gameObject);
        else
            Destroy(gameObject);
    }
}
