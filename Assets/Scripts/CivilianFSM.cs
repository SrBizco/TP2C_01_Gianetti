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

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        idleTimer = idleTime;
    }

    void Update()
    {
        switch (currentState)
        {
            case State.Idle:
                Idle();
                break;
            case State.Patrolling:
                Patrol();
                break;
        }
    }

    void Idle()
    {
        idleTimer -= Time.deltaTime;
        if (idleTimer <= 0)
        {
            idleTimer = idleTime;
            Vector3 randomDirection = Random.insideUnitSphere * patrolRadius;
            randomDirection += transform.position;

            if (NavMesh.SamplePosition(randomDirection, out NavMeshHit hit, patrolRadius, 1))
            {
                agent.SetDestination(hit.position);
                currentState = State.Patrolling;
            }
        }
    }

    void Patrol()
    {
        if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            currentState = State.Idle;
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

        
        Collider civilianCollider = GetComponent<Collider>();
        if (civilianCollider != null)
        {
            civilianCollider.enabled = false;
        }

        Destroy(gameObject);
    }
}