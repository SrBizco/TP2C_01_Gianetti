using UnityEngine;
using UnityEngine.AI;

public class CivilianFSM : MonoBehaviour
{
    public CivilianType civilianType;
    public float idleTime = 2f;
    public float patrolRadius = 10f;

    private enum State { Idle, Patrolling }
    private State currentState = State.Idle;

    private NavMeshAgent agent;
    private float idleTimer;

   
    private Animator animator;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        idleTimer = idleTime;

        
        animator = GetComponent<Animator>();

        
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
            Vector3 randomDirection = Random.insideUnitSphere * patrolRadius;
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

    public void Die()
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

        Destroy(gameObject);
    }
}